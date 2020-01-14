using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Model.Game.Player.Job
{
    public class PlayerJobsModel : ModelBase
    {
        public PlayerJobsModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
           
        }
        private List<Job> mJobs = new List<Job>();
        public List<Job> Jobs
        {
            get
            {
                return mJobs;
            }
            set
            {
                mJobs = value;
            }
        }
        private Job FindJob(SByte id)
        {
            foreach (Job job in this.Jobs)
            {
                if (job.JobId == id)
                    return job;
            }
            return (Job)null;
        }
		/*
        [MessageHandlerAttribut(typeof(JobDescriptionMessage))]
        private void HandleCharacterCharacteristicsInformations(JobDescriptionMessage message, ConnectedHost source)
        {
            foreach (JobDescription jobDescription in message.jobsDescription)
            {
                if (this.FindJob(jobDescription.jobId) == null)
                    this.Jobs.Add(new Job(jobDescription));
                else
                    this.FindJob(jobDescription.jobId).Skills = jobDescription.skills;
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(JobExperienceMultiUpdateMessage))]
        private void HandleCharacterCharacteristicsInformations(JobExperienceMultiUpdateMessage message, ConnectedHost source)
        {
            foreach (JobExperience jobExperience in message.experiencesUpdate)
            {
                if (this.FindJob(jobExperience.jobId) == null)
                    this.Jobs.Add(new Job(jobExperience));
                else
                {
                    this.FindJob(jobExperience.jobId).JobLevel = jobExperience.jobLevel;
                    this.FindJob(jobExperience.jobId).JobXP = jobExperience.jobXP;
                    this.FindJob(jobExperience.jobId).JobXpLevelFloor = jobExperience.jobXpLevelFloor;
                    this.FindJob(jobExperience.jobId).JobXpNextLevelFloor = jobExperience.jobXpNextLevelFloor;
                }
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(JobLevelUpMessage))]
        private void HandleCharacterCharacteristicsInformations(JobLevelUpMessage message, ConnectedHost source)
        {
            foreach (Job job in this.Jobs)
            {
                if (job.JobId == message.jobsDescription.jobId)
                {
                    job.JobLevel = message.newLevel;    
                }
                OnUpdated();
            }
        }
        [MessageHandlerAttribut(typeof(JobExperienceUpdateMessage))]
        private void HandleCharacterCharacteristicsInformations(JobExperienceUpdateMessage message, ConnectedHost source)
        {
            foreach (Job job in this.Jobs)
            {
                if (job.JobId == message.experiencesUpdate.jobId)
                {
                    job.JobLevel = message.experiencesUpdate.jobLevel;
                    job.JobXP = message.experiencesUpdate.jobXP;
                    job.JobXpLevelFloor = message.experiencesUpdate.jobXpLevelFloor;
                    job.JobXpNextLevelFloor = message.experiencesUpdate.jobXpNextLevelFloor;
                }
                OnUpdated();
            }
        }
        */
    }
}
