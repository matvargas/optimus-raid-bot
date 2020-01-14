using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.DataCenter
{
    [Serializable()]
    public class TransformData : IData
    {
        public string OverrideClip { get; set; }

        public string OriginalClip { get; set; }

        public long X { get; set; }

        public long Y { get; set; }

        public long ScaleX { get; set; }

        public long ScaleY { get; set; }

        public long Rotation { get; set; }

        public TransformData()
        {

        }
    }
}
