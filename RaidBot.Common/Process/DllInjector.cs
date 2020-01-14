using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RaidBot.Common.Processe.Injector
{

    public sealed class DllInjector
    {
        #region Declarations

        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress,
            IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        #endregion

        #region Methode public

        public InjectionResultEnum Inject(uint sProcId, string sDllPath)
        {
            if (!File.Exists(sDllPath))
                return InjectionResultEnum.FILENOFOUND;
            if (sProcId == 0)
                return InjectionResultEnum.PROCESSNOFOUND;
            if (!bInject(sProcId, sDllPath))
                return InjectionResultEnum.INJECTIONFAILED;

            return InjectionResultEnum.SUCESS;
        }

        #endregion

        #region Methode priver

        bool bInject(uint pToBeInjected, string sDllPath)
        {
            IntPtr hndProc = OpenProcess((0x2 | 0x8 | 0x10 | 0x20 | 0x400), 1, pToBeInjected);//on prent le handle du processus
            if (hndProc == INTPTR_ZERO)
                return false;
            IntPtr lpLLAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");//on prent l'aadresse du processus
            if (lpLLAddress == INTPTR_ZERO)
                return false;
            IntPtr lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)sDllPath.Length, (0x1000 | 0x2000), 0X40);//on prepart la mémoire pour ecrire la dll
            if (lpAddress == INTPTR_ZERO)
                return false;
            byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0)//on ecrit la dll dans la mémoire du processus
                return false;
            if (CreateRemoteThread(hndProc, (IntPtr)null, INTPTR_ZERO, lpLLAddress, lpAddress, 0, (IntPtr)null) == INTPTR_ZERO)//on lence la dll dans une nouvelle thread
                return false;
            CloseHandle(hndProc);
            return true;
        }

        #endregion
    }
}
