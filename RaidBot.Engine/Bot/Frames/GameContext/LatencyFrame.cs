using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Frames.GameContext
{
    public class LatencyFrame : Frame
    {
        public LatencyFrame(Brain brain) : base(brain)
        {
        }

        // TODO clean things, we also send a sequenceNumber from authFrame
        [MessageHandlerAttribut(typeof(SequenceNumberRequestMessage))]
        private void HandleSequenceNumberRequestMessage(SequenceNumberRequestMessage msg)
        {
            short seqNumber = Brain.Connection.NextSequence();
            Brain.SendMessage(new SequenceNumberMessage().InitSequenceNumberMessage(seqNumber));
            Log("Sending SequenceNumberMessage({0})", seqNumber);
        }

        // TODO maybe decrasse bot latency to avoid antibot detection
        [MessageHandlerAttribut(typeof(BasicLatencyStatsRequestMessage))]
        private void HandleBasicLatencyStatsRequestMessage(BasicLatencyStatsRequestMessage msg)
        {
            BasicLatencyStatsMessage blsmg = new BasicLatencyStatsMessage();
            Brain.SendMessage(blsmg.InitBasicLatencyStatsMessage((short)Math.Min(32767, Brain.Connection.Latency.Avg), (short)Brain.Connection.Latency.SampleCount, (short)Brain.Connection.Latency.Max));
            Log("Sending latency to server (latency {0}, count  {1}, max {2})", (short)Math.Min(32767, Brain.Connection.Latency.Avg), (short)Brain.Connection.Latency.SampleCount, (short)Brain.Connection.Latency.Max);
        }
    }
}
