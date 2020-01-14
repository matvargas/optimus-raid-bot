using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using System;
using System.Collections.Generic;
using RaidBot.Protocol.Messages;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers
{
    public class ServerSelectionManager : Manager
    {
        public event Action ServerSelectionRequired;
        public void OnServerSelectionRequired() { if (ServerSelectionRequired != null) ServerSelectionRequired(); }
        public event Action<bool> ServerSelectionResult;
        public void OnServerSelectionResult(bool e) { if (ServerSelectionResult != null) ServerSelectionResult(e); }
        public event Action NoCharacterAvaiable;
        public void OnNoCharacterAvaiable() { if (NoCharacterAvaiable != null) NoCharacterAvaiable(); }
        public event Action<CharacterCreationResultEnum> CaracterCreationResult;
        public void OnCaracterCreationResult(CharacterCreationResultEnum e) { if (CaracterCreationResult != null) CaracterCreationResult(e); }

        private Dictionary<int, Breed> breeds = null;

        public ServerSelectionManager(Brain bot) : base(bot)
        {
        }

        public void RequestServerSelection(int serverId)
        {
            Brain.SendMessage(new ServerSelectionMessage().InitServerSelectionMessage((short)serverId));
        }

        public void RequestCharacterCreation(int classId, String characterName, bool sex)
        {
            CharacterCreationRequestMessage ccrm = new CharacterCreationRequestMessage();
            Breed br = GameDataManager.SafeGetObject<Breed>(classId);
            foreach (Head h in GameDataManager.SafeGetAllObject<Head>())
            {
                if (h.Breed == classId)
                {
                    ccrm.InitCharacterCreationRequestMessage(characterName, (byte)classId, sex, (short)h.Id, sex ? br.FemaleColors.ToArray() : br.MaleColors.ToArray());
                    Brain.SendMessage(ccrm);
                    return;
                }
            }
            throw new Exception("Can't find head for breed " + classId);
        }

        public List<Breed> GetAvaiableClassList()
        {
            if (breeds == null)
            {
                breeds = new Dictionary<int, Breed>();
                foreach (Breed b in GameDataManager.SafeGetAllObject<Breed>())
                {
                    breeds[b.Id] = b;
                }
            }
            return breeds.Values.ToList();
        }

        public enum CharacterCreationResultEnum
        {
            Undefined,
            Success,
            InvalidServer,
            InvalidName,
            TooManyCharacter
        }
    }
}
