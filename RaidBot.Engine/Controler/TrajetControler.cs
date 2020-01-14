using RaidBot.Engine.Functionality.Script.Machine;
using RaidBot.Engine.Functionality.Script.Machine.Link;
using RaidBot.Engine.Functionality.Script.Parsing;
using RaidBot.Engine.Functionality.Script.Template;
using RaidBot.Engine.Functionality.Script.Template.Expressions;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Setting;
using RaidBot.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Engine.Controler
{
    public class TrajetControler
    {
        #region Properity

        public bool Running { get; private set; }
        public bool Loaded { get; private set; }
        private ClassInterpretor mClassInterpretor;
        private Dictionary<uint,string> Mouvements { get;  set; }
        private ConnectedHost mHost;

        #endregion

        #region Public method

        public TrajetControler(ConnectedHost host)
        {
            mHost = host;
            Loaded = false;
            Running = false;
            Mouvements = new Dictionary<uint, string>();
            mClassInterpretor = new ClassInterpretor();
            host.Bot.Game.World.Map.Updated += Map_Updated;
            InitializeLink();
        }
        public void SetTrajet(ClassTemplate trajet)
        {
            mClassInterpretor.SetClass(trajet);
            Loaded = true;
        }
        public void Start()
        {
            Running = true;
            mClassInterpretor.Execute();
            Map_Updated(this, new EventArgs());
        }
        public void Pause()
        {
        }
        public void Stop()
        {
            Running = false;
        }
        public void Restart()
        {

        }

        #endregion

        #region Private method
        private void InitializeLink()
        {
            mClassInterpretor.Links.Add(new MethodLink("Move", ChangMapLink));
            MouvementsLink mouvement = new MouvementsLink();
            mouvement.MouvementAdded += MouvementAdded;
            mClassInterpretor.Links.Add(mouvement);
        }
        private void MouvementAdded(object sender, MouvementAddedEventArgs e)
        {
            foreach (var item in e.NewMouvements)
            {
                uint cible = Convert.ToUInt32(mClassInterpretor.GetValue(item.Value.LeftExpression));
                string destinationStr = (string)mClassInterpretor.GetValue(item.Value.RightExpression);
                Mouvements.Add(cible,destinationStr.Trim());
            }
        }
        void Map_Updated(object sender, EventArgs e)
        {
            if (Mouvements.ContainsKey(mHost.Bot.Game.World.Map.Data.Id))
            {
                new Thread(() => ChangMap(Mouvements[mHost.Bot.Game.World.Map.Data.Id])).Start();
            }
        }

        private void ChangMap(string dest)
        {
            Thread.Sleep(TrajetSetting.Default.ChangMapTimeOut);
            switch (dest)
            {
                case "up":
                    mHost.Bot.Game.Player.Move(DirectionsEnum.DIRECTION_NORTH);
                    break;
                case "down":
                    mHost.Bot.Game.Player.Move(DirectionsEnum.DIRECTION_SOUTH);
                    break;
                case "left":
                    mHost.Bot.Game.Player.Move(DirectionsEnum.DIRECTION_WEST);
                    break;
                case "right":
                    mHost.Bot.Game.Player.Move(DirectionsEnum.DIRECTION_EAST);
                    break;
            }
        }
        private void ChangMapLink(object[] args)
        {

        }

        #endregion

    }
}
