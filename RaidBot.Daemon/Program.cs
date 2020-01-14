using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.Daemon
{
    static class Program
    {

        const int SW_HIDE = 0;
        [DllImport("User32")]
        static extern int ShowWindow(int hwnd, int nCmdShow);

        const UInt32 WM_KEYDOWN = 0x0100;
        const UInt32 WM_KEYUP = 0x0101;
        const int VK_F5 = 0x74;
        const int VK_RETURN = 0x0D;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        static Process process;

        static Engine.Daemon.Daemon daemon = new Engine.Daemon.Daemon();

        static void CleanOldProcess()
        {
            try
            {
                process.Kill();
            }
            catch { }
            try
            {
                File.Delete(Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf.old"));
            }
            catch { }
        }

        static void ReplaceInvocker()
        {
            File.Move(Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf"), Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf.old"));
            File.Copy(Path.Combine("./DofusInvoker.swf"), Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf"));
        }

        static void ResetInvocker()
        {
            try
            {
                File.Delete(Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf"));
            }
            catch { }
            File.Move(Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf.old"), Path.Combine(Properties.Settings.Default.DofusPath, "DofusInvoker.swf"));
        }

        static void LaunchClient()
        {
            process = new Process();
            process.StartInfo.FileName = Path.Combine(Properties.Settings.Default.DofusPath, "Dofus.exe");
            process.Start();
            process.WaitForInputIdle();
        }

        static void LoadClient()
        {
            CleanOldProcess();
            ReplaceInvocker();
            LaunchClient();
            System.Threading.Thread.Sleep(5000);//Todo find a way to be sure that the invocker is fully loaded, maybe a iwatch on it or something like this
            ResetInvocker();
            WinsockHook h = new WinsockHook(process);
            h.SourceIPs = new List<System.Net.IPAddress>
            {
                System.Net.IPAddress.Parse("34.252.21.81"),
                System.Net.IPAddress.Parse("52.17.231.202"),
            };
            h.RemoteIP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5555);
            h.AllowReplace = true;
            h.Hook();
        }

        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            LoadClient();
            Console.WriteLine("Launching routine");
            while (true)
            {
                PostMessage(process.MainWindowHandle, WM_KEYDOWN, VK_RETURN, 0);
                System.Threading.Thread.Sleep(30);
                PostMessage(process.MainWindowHandle, WM_KEYUP, VK_RETURN, 0);
                System.Threading.Thread.Sleep(30);
                PostMessage(process.MainWindowHandle, WM_KEYDOWN, 0x53, 0);
                System.Threading.Thread.Sleep(30);
                PostMessage(process.MainWindowHandle, WM_KEYUP, 0x53, 0);
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            process.Kill();
        }
    }
}
