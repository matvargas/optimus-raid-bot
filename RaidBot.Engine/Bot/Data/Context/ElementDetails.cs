using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Data.IO.D2P.Map.Elements;
using RaidBot.Data.IO.ELE;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Data.Context
{
    public class ElementDetails
    {
        public StatedElement Stated { get; set; }
        public InteractiveElement Interactive { get; set; }
        public Interactive Static { get; set; }
        public GraphicalElement Graphical { get; set; }
        public Cell Cell { get; set; }
        public List<SkillDetails> DisabelSkills { get; set; }
        public List<SkillDetails> EnabelSkills { get; set; }

        public bool IsActive
        {
            get
            {
                return EnabelSkills.Count() > 0;
            }
        }

        public short CellId
        {
            get
            {
                if (IsStated)
                    return (short)Stated.ElementCellId;
                else
                    return (short)Cell.cellId;
            }
        }

        public int Id
        {
            get
            {
                return Interactive.ElementId;
            }
        }

        public bool IsStated
        {
            get { return Stated != null; }
        }

        public bool IsSolid { get; private set; }

        public string Name { get; set; }

        public ElementDetails(StatedElement stated, InteractiveElement interactive)
        {
            Stated = stated;
            Interactive = interactive;
            LoadStatic();
        }

        public ElementDetails(InteractiveElement interactive, Brain bot)
        {
            Interactive = interactive;
            LoadStatic();
            foreach (Layer layer in bot.State.CurrentMap.Get().Layers)
            {
                foreach (Cell cell in layer.cells)
                {
                    foreach (BasicElement e in cell.elements)
                    {
                        if (e is GraphicalElement)
                        {
                            if (((GraphicalElement)e).identifier == interactive.ElementId)
                            {
                                this.Graphical = (GraphicalElement)e;
                                this.Cell = cell;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void LoadStatic()
        {
            try
            {
                Static = GameDataManager.SafeGetObject<Interactive>(Interactive.ElementTypeId);
                Name = I18nFileAccessor.SafeGetText((int)Static.NameId);
            }
            catch { }
            LoadSkills();
        }

        public void LoadSkills()
        {
            EnabelSkills = new List<SkillDetails>();
            DisabelSkills = new List<SkillDetails>();
            foreach (InteractiveElementSkill item in Interactive.EnabledSkills)
                EnabelSkills.Add(new SkillDetails(item));
            foreach (InteractiveElementSkill item in Interactive.DisabledSkills)
                DisabelSkills.Add(new SkillDetails(item));
            EnabelSkills.ForEach(e => { if (e.IsSolid) IsSolid = true; });
            DisabelSkills.ForEach(e => { if (e.IsSolid) IsSolid = true; });
        }
    }

    public class SkillDetails
    {
        static int[] SolidSKill = new int[] { 6 };

        public Skill Skill { get; set; }
        public InteractiveElementSkill ElementSkill { get; set; }
        public string Name { get; set; }
        public bool IsSolid
        {
            get
            {
                return SolidSKill.Contains(Skill.Id);
            }
        }

        public SkillDetails(InteractiveElementSkill skill)
        {
            ElementSkill = skill;
            Skill = GameDataManager.SafeGetObject<Skill>(skill.SkillId);
            Name = I18nFileAccessor.SafeGetText((int)Skill.NameId);
        }
    }
}
