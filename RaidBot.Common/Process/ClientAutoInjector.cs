
using RaidBot.Common.Default.Loging;
using RaidBot.Common.Processe.Injector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.Processe
{
    public class ClientAutoInjector
    {
        private bool runing = true;
        public static ClientAutoInjector Default = new ClientAutoInjector();
        private List<int> alreadyPatchedClient;
        private int patchedClientCount;
        private Task ListenClientTask;

        public ClientAutoInjector()
        {
            patchedClientCount = 0;
            alreadyPatchedClient = new List<int>();
        }

        public void Start()
        {
            ListenClientTask = new Task(ListenClient);
            ListenClientTask.Start();
        }

        public void Stop()
        {
            runing = false;
        }

        private void ListenClient()
        {
            while (runing)
            {
                System.Threading.Thread.Sleep(600);
                while (Process.GetProcessesByName("Dofus").ToList<Process>().Count  < patchedClientCount) { }
                List<Process> processLst = Process.GetProcessesByName("Dofus").ToList<Process>();
                foreach (Process prc in processLst)
                {
                    if (!alreadyPatchedClient.Contains(prc.Id))
                    {
                        Console.WriteLine("Hello biatch");
                        patchedClientCount += 1;
                        alreadyPatchedClient.Add(prc.Id);
                        DllInjector Injector = new DllInjector();
                        string dllDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\","") + @"\No.Ankama.dll";
                        Logger.Default.Log("Configurations du client : " + Injector.Inject(Convert.ToUInt32(prc.Id), dllDir).ToString());
                   
                        break;
                    }
                }
            }
        }
    }
}