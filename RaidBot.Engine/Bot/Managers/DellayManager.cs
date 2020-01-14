using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers
{
    public class DellayManager
    {
        private static Random rand = new Random();

        public struct RandomDellay
        {
            private int Base;
            private int Volatility;

            public RandomDellay(int @base, int volatility)
            {
                this.Base = @base;
                this.Volatility = volatility;
            }

            public int Next()
            {
                return Base + rand.Next(0, Volatility);
            }

            public static implicit operator int(RandomDellay b)
            {
                return b.Next();
            }
        }

        public enum DellayType
        {
            FightPlacement,
            FightSayReady,
            FightTurnBegin,
            FightTurnFinish,
            FightMovement,
            FightTurnStepDellay,
            FightSpellCasting,
            PartyInvitAccept,
            PartyLoadDellay,
            DeleteObjectInterval,
        }

        static DellayManager instance = new DellayManager();
        public static DellayManager GetInstance()
        {
            return instance;
        }

        public Dictionary<DellayType, RandomDellay> table { get; }

        public int Get(DellayType tp)
        {
            return table[tp];
        }

        public DellayManager()
        {
            table = new Dictionary<DellayType, RandomDellay>();
            table[DellayType.FightPlacement] = new RandomDellay(100, 500);
            table[DellayType.FightSayReady] = new RandomDellay(500, 500);
            table[DellayType.FightTurnBegin] = new RandomDellay(300, 500);
            table[DellayType.FightTurnFinish] = new RandomDellay(100, 200);
            table[DellayType.FightMovement] = new RandomDellay(100, 200);
            table[DellayType.FightSpellCasting] = new RandomDellay(100, 200);
            table[DellayType.FightTurnStepDellay] = new RandomDellay(100, 200);
            table[DellayType.PartyInvitAccept] = new RandomDellay(1000, 500);
            table[DellayType.PartyLoadDellay] = new RandomDellay(2000, 1000);
            table[DellayType.DeleteObjectInterval] = new RandomDellay(100, 150);
        }
    }
}
