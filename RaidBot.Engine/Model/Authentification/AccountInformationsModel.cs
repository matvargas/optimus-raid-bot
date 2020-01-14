using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
namespace RaidBot.Engine.Model.Authentification
{
   public class AccountInformationsModel : ModelBase
    {
        public AccountInformationsModel(ConnectedHost hot)
        {
            hot.Dispatcher.Register(this);
        }
        private string m_NDC;

        public virtual string NDC
        {
            get
            {
                return m_NDC;
            }
            set
            {
                m_NDC = value;
            }
        }
       private string m_MDP;

        public virtual string MDP
        {
            get
            {
                return m_MDP;
            }
            set
            {
                m_MDP = value;
            }
        }
        private bool m_hasRights;

        public virtual bool HasRights
        {
            get
            {
                return m_hasRights;
            }
            set
            {
                m_hasRights = value;
            }
        }

        private bool m_wasAlreadyConnected;

        public virtual bool WasAlreadyConnected
        {
            get
            {
                return m_wasAlreadyConnected;
            }
            set
            {
                m_wasAlreadyConnected = value;
            }
        }

        private string m_login;

        public virtual string Login
        {
            get
            {
                return m_login;
            }
            set
            {
                m_login = value;
            }
        }

        private string m_nickname;

        public virtual string Nickname
        {
            get
            {
                return m_nickname;
            }
            set
            {
                m_nickname = value;
            }
        }

        private int m_accountId;

        public virtual int AccountId
        {
            get
            {
                return m_accountId;
            }
            set
            {
                m_accountId = value;
            }
        }

        private sbyte m_communityId;

        public virtual sbyte CommunityId
        {
            get
            {
                return m_communityId;
            }
            set
            {
                m_communityId = value;
            }
        }

        private string m_secretQuestion;

        public virtual string SecretQuestion
        {
            get
            {
                return m_secretQuestion;
            }
            set
            {
                m_secretQuestion = value;
            }
        }

        private double m_accountCreation;

        public virtual double AccountCreation
        {
            get
            {
                return m_accountCreation;
            }
            set
            {
                m_accountCreation = value;
            }
        }

        private double m_subscriptionElapsedDuration;

        public virtual double SubscriptionElapsedDuration
        {
            get
            {
                return m_subscriptionElapsedDuration;
            }
            set
            {
                m_subscriptionElapsedDuration = value;
            }
        }

        private double m_subscriptionEndDate;

        public virtual double SubscriptionEndDate
        {
            get
            {
                return m_subscriptionEndDate;
            }
            set
            {
                m_subscriptionEndDate = value;
            }
        }
        [MessageHandlerAttribut(typeof(IdentificationSuccessMessage))]
        private void HandleMapComplementaryInformationsDataMessage(IdentificationSuccessMessage message, ConnectedHost source)
        {
            m_hasRights = message.hasRights;
            m_wasAlreadyConnected = message.wasAlreadyConnected;
            m_login = message.login;
            m_nickname = message.nickname;
            m_accountId = message.accountId;
            m_communityId = message.communityId;
            m_secretQuestion = message.secretQuestion;
            m_accountCreation = message.accountCreation;
            m_subscriptionElapsedDuration = message.subscriptionElapsedDuration;
            m_subscriptionEndDate = message.subscriptionEndDate;
            source.logger.Log("Identification réussie !");
            OnUpdated();

        }
     
    }
}
