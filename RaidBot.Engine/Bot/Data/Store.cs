using Raidbot.Protocol.Messages;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Data
{
    public class Store
    {
        public String Ticket { get; set; }
        public ObservableProperty<GameContext> CurrentContext { get; }
        public ObservableProperty<IdentificationSuccessMessage> IdentificationResult { get; }
        public ObservableProperty<SelectedServerDataMessage> SelectedServer { get; }
        public ObservableProperty<List<GameServerDetail>> AvaiableServers { get; }
        public ObservableProperty<UserConfig> UserConfig { get; }

        public ObservableProperty<bool> Connected { get;  }
        public ObservableProperty<List<DebugCellHandler>> DebugCells { get; }
        public ObservableProperty<Map> CurrentMap;
        public ObservableProperty<MapComplementaryInformationsDataMessage> MapComplementaryInformations;

        public Player Player { get; set; }

        public event Action MapReady;
        public void OnMapReady()
        {
            if (MapReady != null) MapReady();
        }

        public Store()
        {
            UserConfig = new ObservableProperty<UserConfig>(null);
            Connected = new ObservableProperty<bool>(false);
            CurrentContext = new ObservableProperty<GameContext>(GameContext.NONE);
            CurrentMap = new ObservableProperty<Map>(null);
            IdentificationResult = new ObservableProperty<IdentificationSuccessMessage>(null);
            SelectedServer = new ObservableProperty<SelectedServerDataMessage>(null);
            MapComplementaryInformations = new ObservableProperty<MapComplementaryInformationsDataMessage>(null);
            AvaiableServers = new ObservableProperty<List<GameServerDetail>>(null);
            DebugCells = new ObservableProperty<List<DebugCellHandler>>(new List<DebugCellHandler>());
            Player = new Player();
        }

        public void DebugCellRequest(short[] cells, int timeout, int state)
        {
            DebugCellHandler handler = new DebugCellHandler(cells, timeout, state);
            DebugCells.Get().Add(handler);
            DebugCells.OnChanged();
            handler.WaitAnd(() =>
            {
                DebugCells.Get().Remove(handler);
                DebugCells.OnChanged();
            });
        }

        public class DebugCellHandler
        {
            public short[] Cells { get; }
            public int Timeout { get; }
            public int State { get; }

            public DebugCellHandler(short[] cells, int timeout, int state)
            {
                this.Cells = cells;
                this.Timeout = timeout;
                this.State = state;
            }

            public async Task WaitAnd(Action fnc)
            {
                await Task.Delay(Timeout);
                fnc();
            }
        }
    }
}
