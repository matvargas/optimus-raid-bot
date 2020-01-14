using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using System;
using RaidBot.Engine.Utility.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.Enums;
using System.Security.Cryptography;
using System.IO;
using Raidbot.Protocol.Messages;

namespace RaidBot.Engine.Bot.Frames
{
    public class AuthFrame: Frame
    {
        public static UInt32 PREDICATE = HelloConnectMessage.Id;

        public AuthFrame(Brain brain): base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(ProtocolRequired))]
        private void HandleProtocolRequired(ProtocolRequired msg)
        {
            Brain.State.CurrentContext.Set(Dispatcher.GameContext.LOGIN);
            Log("Required version {0}", msg.RequiredVersion);
        }

        [MessageHandlerAttribut(typeof(RawDataMessage))]
        private void HandleRawDataMessage(RawDataMessage msg)
        {
            Log("Request antibot bypass to daemon...");
            Brain.AuthManager.ProcessRDM(msg);
        }

        [MessageHandlerAttribut(typeof(HelloConnectMessage))]
        private void HandleHelloConnect(HelloConnectMessage msg)
        {
            Log("Identification ...");
            Brain.AuthManager.Salt = msg.Salt;
            Brain.AuthManager.RsaKey = msg.Key;
            Brain.AuthManager.LoadAesKey();
            Brain.SendMessage(Brain.AuthManager.GetIdentificationMessage(Brain.Config.Username, Brain.Config.Password));
            Brain.SendMessage(Brain.AuthManager.GetClientKeyMessage());
        }

        [MessageHandlerAttribut(typeof(IdentificationFailedMessage))]
        private void HandleIdentificationFailedMessage(IdentificationFailedMessage msg)
        {
            Error("Identification failed, reason : " + (IdentificationFailureReasonEnum)msg.Reason);
        }

        [MessageHandlerAttribut(typeof(IdentificationFailedBannedMessage))]
        private void HandleIdentificationBannedMessage(IdentificationFailedBannedMessage msg)
        {
            Error("Banned !");
        }

        [MessageHandlerAttribut(typeof(IdentificationSuccessMessage))]
        private void HandleIdentificationSuccessMessage(IdentificationSuccessMessage msg)
        {
            Log("Identification success !");
            Brain.State.IdentificationResult.Set(msg);
        }

        [MessageHandlerAttribut(typeof(SelectedServerDataExtendedMessage))]
        private void HandleSelectedServerDataExtendedMessage(SelectedServerDataExtendedMessage msg)
        {
            Log("Server selected automaticaly !");
            Brain.SelectedServer(msg);
            Brain.State.Ticket = Brain.AuthManager.DecodeWithAes(msg.Ticket);
        }

        [MessageHandlerAttribut(typeof(SelectedServerDataMessage))]
        private void HandleSelectedServerDataMessage(SelectedServerDataMessage msg)
        {
            Log("Server selected manualy !");
            Brain.SelectedServer(msg);
            Brain.State.Ticket = Brain.AuthManager.DecodeWithAes(msg.Ticket);
            Brain.ServerManager.OnServerSelectionResult(true);
        }

        [MessageHandlerAttribut(typeof(SelectedServerRefusedMessage))]
        private void HandleSelectedServerRefusedMessage(SelectedServerRefusedMessage msg)
        {
            Error("Can't select this server !");
            Brain.ServerManager.OnServerSelectionResult(false);
        }

        [MessageHandlerAttribut(typeof(HelloGameMessage))]
        private void HandleHelloGameMessage(HelloGameMessage message)
        {
            Brain.State.CurrentContext.Set(Dispatcher.GameContext.GAME);
            Log("Connected to game server !");
            Brain.SendMessage(new AuthenticationTicketMessage().InitAuthenticationTicketMessage("fr", Brain.State.Ticket));
        }

        [MessageHandlerAttribut(typeof(IdentificationFailedForBadVersionMessage))]
        private void HandleIdentificationFailedForBadVersion(IdentificationFailedForBadVersionMessage msg)
        {
            Log("Bad version !");
        }

        [MessageHandlerAttribut(typeof(NicknameRegistrationMessage))]
        private void HandleNicknameRegistrationMessage(NicknameRegistrationMessage msg)
        {
            Brain.AuthManager.OnNickNameRegistrationRequired();
        }

        [MessageHandlerAttribut(typeof(NicknameRefusedMessage))]
        private void HandleNicknameRefusedMessage(NicknameRefusedMessage msg)
        {
            Brain.AuthManager.OnNickNameRegistrationResult(false);
        }

        [MessageHandlerAttribut(typeof(NicknameAcceptedMessage))]
        private void HandleNicknameAcceptedMessage(NicknameAcceptedMessage msg)
        {
            Brain.AuthManager.OnNickNameRegistrationResult(true);
        }
    }
}
