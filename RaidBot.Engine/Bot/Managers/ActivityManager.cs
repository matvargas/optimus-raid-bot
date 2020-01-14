using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers
{
    public class ActivityManager : Manager
    {
        public ActivityManager(Brain brain) : base(brain)
        {
            lastActivity = DateTime.Now.Millisecond;
            brain.CurrentState.changed += CurrentState_changed;
            ActivityLoop();
        }

        private int lastActivity;
        private async void ActivityLoop()
        {
            while (true)
            {
                await Task.Delay(60000 * 5);
                if (DateTime.Now.Millisecond - lastActivity > 60000 * 5)
                {
                    if (Brain.CurrentState.Get() == Brain.BrainState.Fight)
                        continue;
                    Error("No activity from a wile, launching recovery mode ...");
                    Brain.Connection.Stop();
                }
            }
        }

        bool ReconnectPending = false;
        private async void CurrentState_changed(Brain.BrainState data)
        {
            lastActivity = DateTime.Now.Millisecond;
            switch (data)
            {
                case Brain.BrainState.Disconnected:
                    if (ReconnectPending)
                        break;
                    ReconnectPending = true;
                    await Task.Delay(11000);
                    if (Brain.CurrentState.Get() != Brain.BrainState.Disconnected)
                        break;
                    Error("Disconnected, trying to reconnect !");
                    Brain.Group.Login(Brain.Config.Username);
                    ReconnectPending = false;
                    break;
            }
        }
    }
}
