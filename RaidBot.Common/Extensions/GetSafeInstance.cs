using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Common.Extensions
{
    public static class GetSafeInstance
    {
        public static void SafeRun(this ISafeSingelton singelton, Action fnc)
        {
            new SingeltonSafeHandle(singelton.GetMutex()).Run(fnc);
        }
    }

    public class SingeltonSafeHandle
    {
        private Mutex mut;

        public SingeltonSafeHandle(Mutex mut)
        {
            this.mut = mut;
        }

        public SingeltonSafeHandle Run(Action fnc)
        {
            mut.WaitOne();
            try
            {
                fnc();
            }
            finally
            {
                mut.ReleaseMutex();
            }
            return this;
        }
    }

    public abstract class SafeSingelton : ISafeSingelton
    {
        Mutex myMutex;

        public SafeSingelton()
        {
            myMutex = new Mutex();
        }

        public Mutex GetMutex()
        {
            return myMutex;
        }
    }

    public interface ISafeSingelton
    {
        /// <summary>
        /// Nedd to return a singel static mutex for each instance !!
        /// </summary>
        /// <returns></returns>
        Mutex GetMutex();
    }
}
