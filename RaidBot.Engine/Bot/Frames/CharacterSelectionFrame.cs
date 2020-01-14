using Raidbot.Protocol.Messages;
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
    public class CharacterSelectionFrame : Frame
    {
        public static UInt32 PREDICATE = SelectedServerDataMessage.Id;
        private bool WaitingNoOp = false;

        public CharacterSelectionFrame(Brain brain) : base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(CharacterSelectedForceMessage))]
        private void HandleCharacterSelectedForceMessage(CharacterSelectedForceMessage msg)
        {
            WaitingNoOp = true;
        }

        [MessageHandlerAttribut(typeof(BasicNoOperationMessage))]
        private void HandleBasicNoOperationMessage(BasicNoOperationMessage msg)
        {
            if (WaitingNoOp)
            {
                WaitingNoOp = false;
                Brain.SendMessage(new CharacterSelectedForceReadyMessage());
            }
        }


        [MessageHandlerAttribut(typeof(TrustStatusMessage))]
        private void TrustStatusMessage(TrustStatusMessage message)
        {
            Brain.SendMessage(new CharactersListRequestMessage());
        }

        [MessageHandlerAttribut(typeof(ServersListMessage))]
        private void HandleServersListMessage(ServersListMessage message)
        {
            List<GameServerDetail> details = new List<GameServerDetail>();
            foreach (GameServerInformations info in message.Servers)
                details.Add(new GameServerDetail(info));
            Brain.State.AvaiableServers.Set(details);
            Brain.ServerManager.OnServerSelectionRequired();
        }

        [MessageHandlerAttribut(typeof(CharactersListMessage))]
        private void HandleCharactersListMessage(CharactersListMessage message)
        {
            if (message.Characters.Length == 0)
            {
                Log("No character found !");
                Brain.ServerManager.OnNoCharacterAvaiable();
                return;
            }
            Log("Selecting " + message.Characters[0].Id_);
            Brain.SendMessage(new CharacterSelectionMessage().InitCharacterSelectionMessage(message.Characters[0].Id_));
        }

        [MessageHandlerAttribut(typeof(CharacterCreationResultMessage))]
        private void HandleCharacterCreationResultMessage(CharacterCreationResultMessage msg)
        {
            Log("Character creation reasult " + msg.Result);
            if (msg.Result == 0)
                Brain.ServerManager.OnCaracterCreationResult(Managers.ServerSelectionManager.CharacterCreationResultEnum.Success);
            else
                Brain.ServerManager.OnCaracterCreationResult(Managers.ServerSelectionManager.CharacterCreationResultEnum.InvalidName);
        }

        [MessageHandlerAttribut(typeof(CharacterStatsListMessage))]
        private void HandleCharacterStatsListMessage(CharacterStatsListMessage msg)
        {
            Brain.State.Player.Characteristics.Set(msg.Stats);
        }

        [MessageHandlerAttribut(typeof(SpellListMessage))]
        private void HandleSpellListMessage(SpellListMessage msg)
        {
            Dictionary<int, SpellDetail> spells = new Dictionary<int, SpellDetail>();
            foreach (SpellItem spell in msg.Spells)
                spells.Add(spell.SpellId, new SpellDetail(spell));
            Brain.State.Player.Spells.Set(spells);
        }
    }
}
