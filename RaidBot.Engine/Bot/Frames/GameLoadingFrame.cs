using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.Engine.Bot.Managers.FightContext.FightManager;

namespace RaidBot.Engine.Bot.Frames
{
    public class GameLoadingFrame : Frame
    {
        public static UInt32 PREDICATE = HaapiApiKeyMessage.Id;
        private int NoOp = 0;
        private bool Loading = false;

        public GameLoadingFrame(Brain br) : base(br)
        {
        }

        [MessageHandlerAttribut(typeof(BasicNoOperationMessage))]
        private void HandleBasicNoOperationMessage(BasicNoOperationMessage msg)
        {
            if (Loading)
                NoOp++;
            if (NoOp == 3)
            {
                Brain.SendMessage(new ClientKeyMessage().InitClientKeyMessage(Brain.AuthManager.FlashKey));
            }
            else if (NoOp == 5 || (Brain.FightManager.State == FightState.Reconnecting && NoOp == 4))
            {
                Brain.SendMessage(new GameContextCreateRequestMessage());
            }
        }

        [MessageHandlerAttribut(typeof(CharacterLoadingCompleteMessage))]
        private void HandleCharacterLoadingCompleteMessage(CharacterLoadingCompleteMessage msg)
        {
            Loading = false;
            Brain.State.UserConfig.Set(UserConfig.Load(Brain.Config.Username, Brain.State.Player.BaseInformation.Get().Name));
            Log("Config loaded !");
        }

        [MessageHandlerAttribut(typeof(StartupActionsListMessage))]
        private void HandleCharacterLoadingCompleteMessage(StartupActionsListMessage msg)
        {
            Loading = true;
            Brain.SendMessage(new FriendsGetListMessage());
            Brain.SendMessage(new IgnoredGetListMessage());
            Brain.SendMessage(new SpouseGetInformationsMessage());
        }

        [MessageHandlerAttribut(typeof(CharacterStatsListMessage))]
        private void HandleCharacterStatsListMessage(CharacterStatsListMessage msg)
        {
            if (!Loading)
                return;
            Brain.SendMessage(new ChannelEnablingMessage().InitChannelEnablingMessage(7, false));
            Brain.SendMessage(new ChannelEnablingMessage().InitChannelEnablingMessage(14, false));
            Brain.SendMessage(new ChannelEnablingMessage().InitChannelEnablingMessage(10, false));
            Brain.SendMessage(new QuestListRequestMessage());
            Loading = false;
            NoOp = 0;
        }

    }
}

