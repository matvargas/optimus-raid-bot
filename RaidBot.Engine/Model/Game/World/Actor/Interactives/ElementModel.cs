using RaidBot.Data;
using  RaidBot.Data.GameData;
using RaidBot.Data.IO.D2I;
using DataCenter= RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Interactives
{
    public class ElementModel
    {
        public ElementModel(InteractiveElement element)
        {
            Id = element.elementId;
            TypeId = element.elementTypeId;
            EnabledSkills = element.enabledSkills;
            DisabledSkills =element.disabledSkills;
            if (EnabledSkills.Length != 0)
                Enabled = true;
            else
                Enabled = false;
        }
        public ElementModel(StatedElement element)
        {
            CellId = element.elementCellId;
            Id = element.elementId;
            State = element.elementState;
            if (EnabledSkills.Length != 0)
                Enabled = true;
            else
                Enabled = false;
        }
        public void Update(StatedElement element)
        {
            CellId = element.elementCellId;
            State = element.elementState;
            if (EnabledSkills.Length != 0)
                Enabled = true;
            else
                Enabled = false;
        }
        public void Update(InteractiveElement element)
        {
            TypeId = element.elementTypeId;
            EnabledSkills = element.enabledSkills;
            DisabledSkills = element.disabledSkills;
            if (EnabledSkills.Length != 0)
                Enabled = true;
            else
                Enabled = false;
        }
        public string Name
        {
            get
            {
                List<DataCenter.Skill> ld = GameDataAdapter.GetAllClass<DataCenter.Skill>().ToList();

                foreach (DataCenter.Skill d in ld)
                {
                    if ((int)d.InteractiveId == TypeId)
                        return TextDataAdapter.GetText((int)GameDataAdapter.GetClass<DataCenter.Item>(d.GatheredRessourceItem).NameId);
                }
                return "Unknown";
            }
        }
        public bool IsUsable
        {
            get { return EnabledSkills.Length > 0; }
        }
        public bool Enabled { get;  set; }
        public uint CellId { get; private set; }
        public uint State { get; set; }
        public int TypeId { get; private set; }
        public int Id { get; private set; }
        public InteractiveElementSkill[] DisabledSkills { get; private set; }
        public InteractiveElementSkill[] EnabledSkills { get; private set; }
        
    }
}
