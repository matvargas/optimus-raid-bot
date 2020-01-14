using RaidBot.Data.IO.D2P;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Frames
{
    public class ContextFrame : Frame
    {
        public ContextFrame(Brain b) : base(b)
        {
            Regen = new ObservableProperty<bool>(false);
        }

        [MessageHandlerAttribut(typeof(CurrentMapMessage))]
        private void HandleCurrentMapMessage(CurrentMapMessage msg)
        {
            Log("Current mapid is " + msg.MapId);
            Brain.State.CurrentMap.Set(MapManager.SafeGetMap((uint)msg.MapId, Encoding.ASCII.GetBytes("649ae451ca33ec53bbcbcc33becf15f4")));
            Brain.SendMessage(new MapInformationsRequestMessage().InitMapInformationsRequestMessage(msg.MapId));
        }

        [MessageHandlerAttribut(typeof(CharacterSelectedSuccessMessage))]
        private void HandleCharacterSelectedSuccessMessage(CharacterSelectedSuccessMessage msg)
        {
            Brain.State.Player.BaseInformation.Set(msg.Infos);
            Brain.State.CurrentContext.Set(Dispatcher.GameContext.ROLEPLAY);
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataInHouseMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsDataInHouseMessage message)
        {
            HandleMapComplementaryInformationsDataMessage(message);
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataInHavenBagMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsDataInHavenBagMessage message)
        {
            HandleMapComplementaryInformationsDataMessage(message);
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsWithCoordsMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsWithCoordsMessage message)
        {
            HandleMapComplementaryInformationsDataMessage(message);
        }

        bool BeforeNoOpMapIsReady = false;
        [MessageHandlerAttribut(typeof(BasicNoOperationMessage))]
        private void HandleBasicNoOp(BasicNoOperationMessage msg)
        {
            if (BeforeNoOpMapIsReady)
            {
                BeforeNoOpMapIsReady = false;
                Brain.State.OnMapReady();
            }
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsDataMessage message)
        {
            foreach (GameRolePlayActorInformations actor in message.Actors)
            {
                if (actor.ContextualId == Brain.State.Player.BaseInformation.Get().Id_)
                {
                    Brain.State.Player.RolePlayInformations.Set((GameRolePlayCharacterInformations)actor);
                }
            }
            Brain.State.MapComplementaryInformations.Set(message);
            BeforeNoOpMapIsReady = true;
        }

        [MessageHandlerAttribut(typeof(GameRolePlayShowActorMessage))]
        private void HandleGameRolePlayShowActorMessage(GameRolePlayShowActorMessage msg)
        {
            Brain.ElementsManager.AddActor(msg.Informations);
        }

        [MessageHandlerAttribut(typeof(InteractiveElementUpdatedMessage))]
        private void HandleInteractiveElementUpdatedMessage(InteractiveElementUpdatedMessage msg)
        {
            Brain.ElementsManager.UpdateInteractive(msg);
        }

        [MessageHandlerAttribut(typeof(StatedElementUpdatedMessage))]
        private void HandleStatedElementUpdatedMessage(StatedElementUpdatedMessage msg)
        {
            Brain.ElementsManager.UpdateStated(msg);
        }

        [MessageHandlerAttribut(typeof(GameContextRemoveElementMessage))]
        private void HandleGameContextRemoveElementMessage(GameContextRemoveElementMessage msg)
        {
            try
            {
                Brain.ElementsManager.RemoveActor(msg.Id_);
            }
            catch { }
        }

        [MessageHandlerAttribut(typeof(CharacterLevelUpMessage))]
        private void HandleCharacterLevelUpInformationMessage(CharacterLevelUpMessage msg)
        {
            Log("Level up ({0})", msg.NewLevel);
            Brain.State.Player.BaseInformation.Get().Level = msg.NewLevel;
            Brain.State.Player.BaseInformation.OnChanged();
        }


        public ObservableProperty<bool> Regen { get; private set; }

        [MessageHandlerAttribut(typeof(LifePointsRegenBeginMessage))]
        private async void HandleLifePointsRegenBeginMessage(LifePointsRegenBeginMessage msg)
        {
            StartRegen(msg.RegenRate);
        }

        private async void StartRegen(int rate)
        {
            Regen.Set(true);
            ObservableProperty<CharacterCharacteristicsInformations> info = Brain.State.Player.Characteristics;
            try
            {
                while (Regen.Get())
                {
                    if (info.Get() != null && info.Get().LifePoints < info.Get().MaxLifePoints)
                    {
                        info.Get().LifePoints++;
                        info.OnChanged();
                    }
                    else if (info.Get() != null)
                        break;
                    await Task.Delay(rate * 100);
                }
            }
            finally
            {
                Regen.Set(false);
            }
        }

        [MessageHandlerAttribut(typeof(LifePointsRegenEndMessage))]
        private void HandleLifePointsRegenEndMessage(LifePointsRegenEndMessage msg)
        {
            Regen.Set(false);
        }

        [MessageHandlerAttribut(typeof(UpdateLifePointsMessage))]
        private void HandleUpdateLifePointsMessage(UpdateLifePointsMessage msg)
        {
            Brain.State.Player.Characteristics.Get().LifePoints = msg.LifePoints;
            Brain.State.Player.Characteristics.Get().MaxLifePoints = msg.MaxLifePoints;
            Brain.State.Player.Characteristics.OnChanged();
        }

        [MessageHandlerAttribut(typeof(StatsUpgradeResultMessage))]
        private void HandleStatsUpgradeResultMessage(StatsUpgradeResultMessage msg)
        {
            if (msg.Result == 0)
                Log("Stats upgraded !");
            else
                Log("Failed to upgrade stats !");
        }
    }
}
