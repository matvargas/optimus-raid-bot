using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Data.IO.D2I;
using RaidBot.Data.GameData;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Model.Game.Player.Job
{
   public class Job
    {
        public Job()
        {

        }
		/*
        public Job(JobDescription job)
        {
          this.JobId = job;
          this.Skills = job;
        }

         public Job(JobExperience job)
        {
          this.JobId = job.jobId;
          this.JobLevel = job.jobLevel;
          this.JobXP = job.jobXP;
          this.JobXpLevelFloor = job.jobXpLevelFloor;
          this.JobXpNextLevelFloor = job.jobXpNextLevelFloor;
        }
        */
        
         public virtual string Name
         {
             get
             {
               
                 return TextDataAdapter.GetText((int)GameDataAdapter.GetClass<RaidBot.Protocol.DataCenter.Job>(JobId).NameId);
             }
            
          }

        private sbyte m_jobId;

        public virtual sbyte JobId
        {
            get
            {
                return m_jobId;
            }
            set
            {
                m_jobId = value;
            }
        }

        private SkillActionDescription[] m_skills;

        public virtual SkillActionDescription[] Skills
        {
            get
            {
                return m_skills;
            }
            set
            {
                m_skills = value;
            }
        }

       SkillActionDescriptionCollect[] _collectSkills;
        public SkillActionDescriptionCollect[] CollectSkills { get { if (_collectSkills == null) { _collectSkills = Skills.OfType<SkillActionDescriptionCollect>().ToArray(); }; return _collectSkills; } }

        SkillActionDescriptionCraft[] _craftSkills;
        public SkillActionDescriptionCraft[] CraftSkills { get { if (_craftSkills == null) { _craftSkills = Skills.OfType<SkillActionDescriptionCraft>().ToArray(); }; return _craftSkills; } }

        private sbyte m_jobLevel;

        public virtual sbyte JobLevel
        {
            get
            {
                return m_jobLevel;
            }
            set
            {
                m_jobLevel = value;
            }
        }

        private ulong m_jobXP;

        public virtual ulong JobXP
        {
            get
            {
                return m_jobXP;
            }
            set
            {
                m_jobXP = value;
            }
        }

        private ulong m_jobXpLevelFloor;

        public virtual ulong JobXpLevelFloor
        {
            get
            {
                return m_jobXpLevelFloor;
            }
            set
            {
                m_jobXpLevelFloor = value;
            }
        }

        private ulong m_jobXpNextLevelFloor;

        public virtual ulong JobXpNextLevelFloor
        {
            get
            {
                return m_jobXpNextLevelFloor;
            }
            set
            {
                m_jobXpNextLevelFloor = value;
            }
        }
        
    }
}
