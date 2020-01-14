using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Daemon
{
    public class WinsockHook
    {


        public static class NativeMethods
        {

            [Flags]
            public enum AllocationType
            {
                Commit = 0x1000,
                Reserve = 0x2000,
                Decommit = 0x4000,
                Release = 0x8000,
                Reset = 0x80000,
                Physical = 0x400000,
                TopDown = 0x100000,
                WriteWatch = 0x200000,
                LargePages = 0x20000000
            }

            [Flags]
            public enum MemoryProtection
            {
                Execute = 0x10,
                ExecuteRead = 0x20,
                ExecuteReadWrite = 0x40,
                ExecuteWriteCopy = 0x80,
                NoAccess = 0x01,
                ReadOnly = 0x02,
                ReadWrite = 0x04,
                WriteCopy = 0x08,
                GuardModifierflag = 0x100,
                NoCacheModifierflag = 0x200,
                WriteCombineModifierflag = 0x400
            }

            [Flags]
            public enum LoadLibraryFlags : uint
            {
                None = 0,
                DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
                LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
                LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
                LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
                LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
                LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
                LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
                LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
                LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
                LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
                LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
            }


            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      byte[] lpBuffer,
      Int32 nSize,
      out IntPtr lpNumberOfBytesWritten);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [Out] byte[] lpBuffer,
        int dwSize,
        out IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(
                IntPtr hProcess,
                IntPtr lpBaseAddress,
                [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
                int dwSize,
                out IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(
                IntPtr hProcess,
                IntPtr lpBaseAddress,
                IntPtr lpBuffer,
                int dwSize,
                out IntPtr lpNumberOfBytesRead);

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
       uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

            [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
            public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
       int dwSize, AllocationType dwFreeType);

            [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

            public static IntPtr GetAPIAddress(string lpFileName, string procName)
            {
                IntPtr moduleHandle = LoadLibraryEx(lpFileName, IntPtr.Zero, LoadLibraryFlags.None);
                if (moduleHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                IntPtr procHandle = GetProcAddress(moduleHandle, procName);
                if (moduleHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                return procHandle;
            }

            [DllImport("kernel32.dll")]
            public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,
       UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        }

        public class WinsockHookEventArgs : EventArgs
        {

            public WinsockHookEventArgs(Process process, IntPtr apiAdd, IntPtr hookAdd, IntPtr continueAdd)
            {
                Process = process;
                ApiAdd = apiAdd;
                HookAdd = hookAdd;
                ContinueAdd = continueAdd;
            }

            public Process Process { get; private set; }
            public IntPtr ApiAdd { get; private set; }
            public IntPtr HookAdd { get; private set; }
            public IntPtr ContinueAdd { get; private set; }

        }

        public static class Helper
        {
            public static int CalculateJmp(int source, int dest)
            {
                if (source > dest)
                    return dest - source - 5;
                return source - dest - 5;
            }

            public static ushort FormatPort(int port)
            {
                /* 5555 = 0x15B3, but it has to be 0xB315
                 * 0x15B3 = 00010101 10110011 and 0xB315 = 10110011 00010101
                 * We have to rotate right (or left since it's short and it's only 2 bytes).
                 * (00010101 10110011) << 8 = 10110011 00000000
                 * (00010101 10110011) >> 8 = 00000000 00010101
                 * In this case, "add" and "or" does the same, so we have just the or the 2 nums to rotate bytes.
                 */

                return (ushort)((port << 8) | (port >> 8));
            }

            public static bool DetectHook(IntPtr hProcess, IntPtr lpAddress, bool replace)
            {
                byte[] bytes = new byte[5];
                if (!NativeMethods.ReadProcessMemory(hProcess, lpAddress, bytes, 5, out IntPtr lpNumberOfBytesRead))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (bytes[0] != 0xE9)
                {
                    return false;
                }

                if (!replace)
                {
                    return true;
                }

                byte[] originalBytes = new byte[5]
                {
                0x8b,0xff, // mov edi, edi
                0x55,      // push ebp
                0x8b,0xec  // mov ebp, esp
                };

                if (!NativeMethods.VirtualProtectEx(hProcess, lpAddress, new UIntPtr((uint)originalBytes.Length), (uint)NativeMethods.MemoryProtection.ExecuteReadWrite, out uint oldProtection))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (!NativeMethods.WriteProcessMemory(hProcess, lpAddress, originalBytes, originalBytes.Length, out lpNumberOfBytesRead))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (!NativeMethods.VirtualProtectEx(hProcess, lpAddress, new UIntPtr((uint)originalBytes.Length), oldProtection, out oldProtection))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                return true;
            }

        }

        private IntPtr _handle;
        private Process _process;
        private bool _prepared;
        private IntPtr _apiAdd;
        private IntPtr _hookAdd;
        private IntPtr _continueAdd;
        private byte[] _originalBytes;
        private byte[] _trampoline;
        private byte[] _continue;
        private byte[] _patchBytes;



        /// <summary>
        /// A list of IP address you want to blacklist.
        /// </summary>
        public IList<IPAddress> SourceIPs { get; set; }

        /// <summary>
        /// The IP address on which you want to redirect the connection.
        /// </summary>
        public IPEndPoint RemoteIP { get; set; }

        /// <summary>
        /// Is "connect" hooked ?
        /// </summary>
        public bool IsHook { get; private set; }

        /// <summary>
        /// Do we replace the function with the original bytes if the function is already hooked ?
        /// </summary>
        public bool AllowReplace { get; set; }

        public delegate void WinsockHookEventHandler(object sender, WinsockHookEventArgs e);
        public event WinsockHookEventHandler OnHook;
        public event WinsockHookEventHandler OnUnhook;
        public event WinsockHookEventHandler OnHookDetected;


        /// <summary>
        /// Initialize the WinsockHook class.
        /// </summary>
        /// <param name="process">The process to hook.</param>
        public WinsockHook(Process process)
        {
            _handle = process.Handle;
            _process = process;
        }

        /// <summary>
        /// Initialize the WinsockHook class.
        /// </summary>
        /// <param name="pid">Process ID of the process you want to hook.</param>
        public WinsockHook(int pid)
        {
            _process = Process.GetProcessById(pid);
            _handle = _process.Handle;
        }


        /// <summary>
        /// Initialize the WinsockHook class.
        /// </summary>
        /// <param name="processName">The name of the process you want to hook.</param>
        /// <param name="index">Several processes can have the same name, so you have to choose which.</param>
        public WinsockHook(string processName, int index = 0)
        {
            _process = Process.GetProcessesByName(processName)[index];
            _handle = _process.Handle;
        }

        /// <summary>
        /// Prepare the variables for hooking the function.
        /// </summary>
        public void Prepare()
        {
            if (_prepared)
            {
                throw new NotSupportedException();
            }

            // get address of connect
            if (_apiAdd == IntPtr.Zero)
            {
                GetApiAddress();
            }

            if (Helper.DetectHook(_handle, _apiAdd, AllowReplace))
            {
                HandleEvent(OnHookDetected);

                // if we don't replace, then our original bytes are the jump, and it causes crash.
                if (!AllowReplace)
                {
                    return;
                }
            }

            // get the original opcodes of connect
            if (_originalBytes == null)
            {
                GetOriginalBytes();
            }

            // redirect to original function with modified, or not, parameters
            if (_continue == null && _continueAdd == IntPtr.Zero)
            {
                CreateContinue();
            }

            // the DETOUR !!!
            if (_patchBytes == null && _hookAdd == IntPtr.Zero)
            {
                CreateDetour();
            }

            // replace the original bytes with a jump to our detour
            if (_trampoline == null)
            {
                CreateTrampoline();
            }

            _prepared = true;
        }

        /// <summary>
        /// Hook the function.
        /// </summary>
        public void Hook()
        {
            if (IsHook)
                throw new NotSupportedException();

            if (!_prepared)
                Prepare();

            uint oldProtection;
            if (!NativeMethods.VirtualProtectEx(_handle, _apiAdd, new UIntPtr((uint)_trampoline.Length), (uint)NativeMethods.MemoryProtection.ExecuteReadWrite, out oldProtection))
            {
                HandleError();
            }

            IntPtr lpNumberOfBytesRead;
            if (!NativeMethods.WriteProcessMemory(_handle, _apiAdd, _trampoline, _trampoline.Length, out lpNumberOfBytesRead))
            {
                HandleError();
            }

            if (!NativeMethods.VirtualProtectEx(_handle, _apiAdd, new UIntPtr((uint)_trampoline.Length), oldProtection, out oldProtection))
            {
                HandleError();
            }

            IsHook = true;
            HandleEvent(OnHook);
        }

        /// <summary>
        /// Remove the hook of the function.
        /// </summary>
        public void Unhook()
        {
            if (!IsHook)
                throw new NotSupportedException();

            if (!NativeMethods.VirtualProtectEx(_handle, _apiAdd, new UIntPtr((uint)_originalBytes.Length), (uint)NativeMethods.MemoryProtection.ExecuteReadWrite, out uint oldProtection))
            {
                HandleError();
            }

            if (!NativeMethods.WriteProcessMemory(_handle, _apiAdd, _originalBytes, _originalBytes.Length, out IntPtr lpNumberOfBytesRead))
            {
                HandleError();
            }

            if (!NativeMethods.VirtualProtectEx(_handle, _apiAdd, new UIntPtr((uint)_originalBytes.Length), oldProtection, out oldProtection))
            {
                HandleError();
            }

            IsHook = false;
            HandleEvent(OnUnhook);
        }

        #region Private

        /// <summary>
        /// Return the address of ws2_32.dll!connect.
        /// </summary>
        private void GetApiAddress()
        {
            _apiAdd = NativeMethods.GetAPIAddress("ws2_32.dll", "connect");
            if (_apiAdd == IntPtr.Zero)
            {
                HandleError();
            }
        }

        /// <summary>
        /// Get the original bytes at the address of the function.
        /// </summary>
        private void GetOriginalBytes()
        {
            _originalBytes = new byte[5];
            if (!NativeMethods.ReadProcessMemory(_handle, _apiAdd, _originalBytes, 5, out IntPtr lpNumberOfBytesRead))
            {
                HandleError();
            }
        }

        /// <summary>
        /// Alloc 10 bytes to write the original bytes of the function and the jump to continue.
        /// </summary>
        private void CreateContinue()
        {
            _continue = new byte[5 + 5];
            Array.Copy(_originalBytes, _continue, 5);
            _continue[5] = 0xE9; // jmp

            if ((_continueAdd = NativeMethods.VirtualAllocEx(_handle, IntPtr.Zero, (uint)_continue.Length, NativeMethods.AllocationType.Commit | NativeMethods.AllocationType.Reserve, NativeMethods.MemoryProtection.ExecuteReadWrite)) == IntPtr.Zero)
            {
                HandleError();
            }

            //int continueToApi = CalculateJmp(_apiAdd.ToInt32() + 5, _continueAdd.ToInt32() + 5);
            int continueToApi = _apiAdd.ToInt32() - _continueAdd.ToInt32() - 5;
            Array.Copy(BitConverter.GetBytes(continueToApi), 0, _continue, 6, 4);

            if (!NativeMethods.WriteProcessMemory(_handle, _continueAdd, _continue, _continue.Length, out IntPtr lpNumberOfBytesRead))
            {
                HandleError();
            }
        }

        /// <summary>
        /// Alloc bytes to detour the function, it checks if the current IP is blacklisted, then it patches if it is there, otherwise it returns to original call function.
        /// </summary>
        private void CreateDetour()
        {
            // I use List of bytes because it is easier to manipulate than byte array.
            List<byte> tmp = new List<byte>();
            tmp.AddRange(new byte[]
            {
                    0x55,           // push ebp
                    0x8b, 0xec,     // mov ebp, esp
                    0x8b,0x45,0x0c, // mov eax, DWORD PTR [ebp+0xc]
                    0x83,0xec,0x10  // sub esp, 0x10
            });

            // cmp whith blacklisted ips
            int max = 5 + 9 * (SourceIPs.Count - 1);
            if (max > byte.MaxValue)
            {
                throw new NotSupportedException();
            }

            foreach (IPAddress ip in SourceIPs)
            {
                tmp.AddRange(new byte[]
                {
                        0x81,0x78,0x04 // cmp DWORD PTR [eax+0x4], ...
                });
                tmp.AddRange(BitConverter.GetBytes((uint)ip.Address));
                tmp.AddRange(new byte[]
                {
                        0x74, (byte)max // je ...
                });
                max -= 9;
            }

            tmp.AddRange(new byte[]
            {
                    0xff,0x75,0x10,               // push [ebp+0x10]
                    0xeb,0x1c,                    // jmp +0x1f
                    0x6a,0x02,/*0x00,0x00,0x00,*/ // push 0x00000002
                    0x58,                         // pop eax
                    0x66,0x89,0x45,0xf0,          // mov [ebp-0x10], ax
                    0xb8                          // mov eax, port...
            });
            tmp.AddRange(BitConverter.GetBytes((uint)Helper.FormatPort(RemoteIP.Port)));
            tmp.AddRange(new byte[]
            {
                    0x66,0x89,0x45,0xf2,      // mov [ebp-0xE], ax
                    0x8d,0x45,0xf0,           // lea eax, [ebp-0x10]
                    0xc7,0x45,0xf4            // mov [ebp-0xC], ip...
            });
            tmp.AddRange(BitConverter.GetBytes((uint)RemoteIP.Address.Address));
            tmp.AddRange(new byte[]
            {
                    //0x68,0x10,0x00,0x00,0x00,  // push 0x00000010
                    0x6a,0x10,                 // push 0x10
                    0x50,                      // push eax
                    0xff,0x75,0x08,            // push [ebp+0x08]
                    0xe8,0xde,0xad,0xbe,0xef   // call 0xdeadbeef
            });
            int index = tmp.Count;
            tmp.AddRange(new byte[]
            {
                    0xc9,                      // leave
                    0xc2,0x0c,0x00             // ret 0x0c
            });

            _patchBytes = tmp.ToArray();

            // Clean
            tmp.Clear();
            tmp = null;

            if ((_hookAdd = NativeMethods.VirtualAllocEx(_handle, IntPtr.Zero, (uint)_patchBytes.Length, NativeMethods.AllocationType.Commit | NativeMethods.AllocationType.Reserve, NativeMethods.MemoryProtection.ExecuteReadWrite)) == IntPtr.Zero)
            {
                HandleError();
            }

            // find for the call
            int callAdd = Helper.CalculateJmp(_hookAdd.ToInt32() + index, _continueAdd.ToInt32()) + 5;
            Array.Copy(BitConverter.GetBytes(callAdd), 0, _patchBytes, index - 4, 4);


            if (!NativeMethods.WriteProcessMemory(_handle, _hookAdd, _patchBytes, _patchBytes.Length, out IntPtr lpNumberOfBytesRead))
            {
                HandleError();
            }
        }

        /// <summary>
        /// We replace the original bytes to jump over our hook memory region.
        /// </summary>
        private void CreateTrampoline()
        {
            _trampoline = new byte[5];
            _trampoline[0] = 0xE9; // jmp
            int jmp = Helper.CalculateJmp(_apiAdd.ToInt32(), _hookAdd.ToInt32());
            Array.Copy(BitConverter.GetBytes(jmp), 0, _trampoline, 1, 4);
        }

        /// <summary>
        /// It handles the error than WinAPI occured.
        /// </summary>
        private void HandleError()
        {
            int error = Marshal.GetLastWin32Error();
            if (_hookAdd != IntPtr.Zero)
            {
                NativeMethods.VirtualFreeEx(_handle, _hookAdd, 0, NativeMethods.AllocationType.Release);
                _hookAdd = IntPtr.Zero;
                _patchBytes = null;
            }
            if (_continueAdd != IntPtr.Zero)
            {
                NativeMethods.VirtualFreeEx(_handle, _continueAdd, 0, NativeMethods.AllocationType.Release);
                _continueAdd = IntPtr.Zero;
                _continue = null;
            }
            throw new Win32Exception(error);
        }

        /// <summary>
        /// It checks if the process has ws2_32.dll and if the variables are set.
        /// </summary>
        private void Check()
        {
            if (!_process.Modules.Any(module => module.ModuleName.ToLower() == "ws2_32.dll"))
            {
                throw new DllNotFoundException();
            }

            if (SourceIPs.Count == 0)
            {
                throw new NotSupportedException();
            }

            if (RemoteIP == null)
            {
                throw new NotSupportedException();
            }
        }

        private void HandleEvent(WinsockHookEventHandler winsockHookEventHandler)
        {
            winsockHookEventHandler?.Invoke(this, new WinsockHookEventArgs(_process, _apiAdd, _hookAdd, _continueAdd));
        }

        #endregion
    }
}
