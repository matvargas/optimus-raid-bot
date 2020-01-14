using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.Network.Client
{
    public class Latency
    {
        List<int> latencyBuffer;
        int lastSent;
   
        public uint Max { get { return 50; } }

        public int SampleCount
        {
            get { return latencyBuffer.Count; }
        }

        public int Avg
        {
            get
            {
                if (latencyBuffer.Count == 0)
                    return 0;
                int total = 0;
                foreach (int latency in latencyBuffer)
                    total = total + latency;
                return (total / this.latencyBuffer.Count);
            }
        }

        public Latency()
        {
            latencyBuffer = new List<int>();
            lastSent = 0;
        }

        public void Send()
        {
            lastSent = DateTime.Now.Millisecond;
        }

        public void UpdateLatency()
        {
            if (lastSent == 0)
                return;
            var packetReceived = DateTime.Now.Millisecond;
            int latency = packetReceived - lastSent;
            lastSent = 0;
            latencyBuffer.Add(latency);
            if (latencyBuffer.Count > Max)
                latencyBuffer.RemoveAt(0);
        }
    }
}
