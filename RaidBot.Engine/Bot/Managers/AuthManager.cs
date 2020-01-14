using Raidbot.Protocol.Messages;
using RaidBot.Common.IO;
using RaidBot.Engine.Daemon;
using RaidBot.Engine.Utility.Security;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers
{
    public class AuthManager:Manager
    {
        public byte[] RsaKey { get; set; }
        public byte[] AesKey { get; set; }
        public String Salt { get; set; }
        public String FlashKey { get; set; }

        private static char[] HEX_CHARS = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private const int KEY_SIZE = 21;
        private const int AES_KEY_LENGTH = 32;

        public event Action<AuthManager> NickNameRegistrationRequired;
        public void OnNickNameRegistrationRequired() { if (NickNameRegistrationRequired != null) NickNameRegistrationRequired(this); }
        public event Action<AuthManager, bool> NickNameRegistrationResult;
        public void OnNickNameRegistrationResult(bool res) { if (NickNameRegistrationResult != null) NickNameRegistrationResult(this, res);  }

        public AuthManager(Brain brain): base(brain)
        {
            LoadFlashKey();
        }

        public IdentificationMessage GetIdentificationMessage(String username, String password)
        {
            VersionExtended version = (VersionExtended)(new VersionExtended().InitVersionExtended(1, 1).InitVersion(2, 48, 17, 96720011, 1, 0));
            IdentificationMessage iMsg = new IdentificationMessage().InitIdentificationMessage(true, false, false, version, "fr", Cryptography.Encrypt(RsaKey, Salt, AesKey, username, password), 0, 0, new short[0]);
            return iMsg;
        }

        public ClientKeyMessage GetClientKeyMessage()
        {
            return new ClientKeyMessage().InitClientKeyMessage(this.FlashKey);
        }

        public void SelectNickName(String nickname)
        {
            Brain.SendMessage(new NicknameChoiceRequestMessage().InitNicknameChoiceRequestMessage(nickname));
        }

        public void ProcessRDM(RawDataMessage msg)
        {
            DaemonClient.GetInstance().Request(msg);
            DaemonClient.GetInstance().MessageReceived += Bypass_MessageReceived;
        }

        private void Bypass_MessageReceived(object sender, Daemon.Daemon.MessageReceivedEventArgs e)
        {
            Log("Receive message from daemon");
            if (e.Msg is CheckIntegrityMessage)
            {
                Log("Slave has generated CheckIntegrityMessage");
                e.Msg.Deserialize(new CustomDataReader(e.Msg.Data));
                Brain.SendMessage(e.Msg);
                DaemonClient.GetInstance().MessageReceived -= Bypass_MessageReceived;
            }
        }

        public void LoadFlashKey()
        {
            this.FlashKey = "ZrGM8jyXjaaIISzKG0#01";//GenerateRandomFlashKey();
        }

        public void LoadAesKey()
        {
            this.AesKey = GenerateRandomAESKey();
        }

        public String DecodeWithAes(byte[] cipherText)
        {
            string plaintext = null;
            Aes aes = AesCryptoServiceProvider.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = AesKey;
            aes.IV = AesKey.Take(16).ToArray();
            aes.Padding = PaddingMode.None;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        private byte[] GenerateRandomAESKey()
        {
            Random rnd = new Random();
            byte[] ba = new byte[AES_KEY_LENGTH];
            for (int i = 0; i < AES_KEY_LENGTH; i++)
                ba[i] = (byte)Math.Floor((double)(rnd.NextDouble() * 256));
            return ba;
        }

        private String GenerateRandomFlashKey()
        {
            String sSentence = String.Empty;
            int nLen = KEY_SIZE - (1 + 3);
            for (int i = 0; i < nLen; i++)
            {
                sSentence = sSentence + GetRandomChar();
            }
            return sSentence + Checksum(sSentence);
        }

        private char Checksum(String str)
        {
            int r = 0;
            char[] s = str.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                r = r + s[i] % 16;
            }
            return HEX_CHARS[r % 16];
        }

        private char GetRandomChar()
        {
            Random rnd = new Random();
            int n = (int)Math.Ceiling((double)(rnd.NextDouble() * 100));
            if (n <= 40)
            {
                return (char)Math.Floor((double)(rnd.NextDouble() * 26) + 65);
            }
            if (n <= 80)
            {
                return (char)Math.Floor((double)(rnd.NextDouble() * 26) + 97);
            }
            return (char)Math.Floor((double)(rnd.NextDouble() * 10) + 48);
        }
    }
}
