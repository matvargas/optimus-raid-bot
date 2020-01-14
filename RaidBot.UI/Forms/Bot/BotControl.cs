using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaidBot.Engine.Bot;
using RaidBot.Common.Default.Loging;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Protocol.Types;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Engine.Bot.Data.Context;
using RaidBot.Data.IO.D2P.Map.Elements;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Utility;
using RaidBot.Protocol.Enums;
using RaidBot.Engine.Bot.Frames.GameContext;
using RaidBot.Engine.Bot.Managers.GameContext;
using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using RaidBot.Data.IO.D2I;
using static RaidBot.Engine.Bot.Brain;
using RaidBot.Engine.Bot.Managers;
using static RaidBot.Engine.Bot.Data.Store;
using RaidBot.Common.IO;
using static RaidBot.Engine.Bot.Managers.FightContext.FightManager;

namespace RaidBot.UI.Forms.Bot
{
    public partial class BotControl : UserControl
    {

        #region Declarations

        Brain bot;
        MapControl map;

        #endregion

        #region Ctor/Dtor

        public BotControl()
        {
            InitializeComponent();
        }

        ~BotControl()
        {
            if (AccountManager.GetInstance().Accounts.ContainsKey(bot.Config.Username))
                AccountManager.GetInstance().Accounts[bot.Config.Username].Used = false;
            bot.Dispose();
        }

        public BotControl(Brain bot)
        {
            InitializeComponent();
            this.bot = bot;
            this.bot.NetworkLogger.OnLogObj += NetworkLogger_OnLogObj; ;
            this.bot.NetworkLogger.OnLog += Logger_OnLog;
            this.bot.Logger.OnLog += Logger_OnLog;

            InitMap();
            // Popup
            this.bot.ServerManager.NoCharacterAvaiable += ServerManager_NoCharacterAvaiable;
            this.bot.ServerManager.ServerSelectionRequired += ServerManager_ServerSelectionRequired;
            this.bot.AuthManager.NickNameRegistrationRequired += AuthManager_NickNameRegistrationRequired;

            // Settings
            this.bot.State.UserConfig.changed += UserConfig_changed;
            InitHeaderBar();
            InitInvCharac();
        }

        #endregion

        #region Header bar

        private void InitHeaderBar()
        {
            bot.State.Player.BaseInformation.changed += BaseInformation_changed;
            bot.State.Player.Characteristics.changed += Characteristics_changed;
            bot.State.Player.InventoryWeight.changed += InventoryWeight_changed;
            bot.State.CurrentMap.changed += CurrentMap_changed;
            bot.CurrentState.changed += CurrentState_changed;
        }

        private void CurrentState_changed(Brain.BrainState data)
        {
            this.BeginInvoke(new Action(() =>
           {
               lblState.Text = data.ToString();
           }));
        }

        private void CurrentMap_changed(Map data)
        {
            SubArea sub = GameDataManager.SafeGetObject<SubArea>(data.SubareaId);
            String subareaText = I18nFileAccessor.SafeGetText((int)sub.NameId);
            String areaText = I18nFileAccessor.SafeGetText((int)GameDataManager.SafeGetObject<Area>(sub.AreaId).NameId);
            this.BeginInvoke(new Action(() =>
            {
                lblMap.Text = String.Format("[ {0}, {1} ] {3} - {2}", data.Position.x, data.Position.y, subareaText, areaText);
            }));
        }

        private void InventoryWeight_changed(Protocol.Messages.InventoryWeightMessage data)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (data.Weight > data.WeightMax)
                {
                    pbPods.Maximum = data.WeightMax;
                    pbPods.Value = data.WeightMax;
                    ModifyProgressBarColor.SetState(pbPods, 1);
                }
                else
                {
                    ModifyProgressBarColor.SetState(pbPods, 0);
                    pbPods.Maximum = data.WeightMax;
                    pbPods.Value = data.Weight;
                }
                lblPods.Text = String.Format("Pods {0}/{1}", HumanNumber(data.Weight), HumanNumber(data.WeightMax));
            }));
        }

        private String HumanNumber(int num)
        {
            if (num > 10000)
                return (String.Format("{0}K", (double)num / 1000));
            return num.ToString();
        }

        private void Characteristics_changed(CharacterCharacteristicsInformations data)
        {
            this.BeginInvoke(new Action(() =>
            {
                pbLife.Maximum = data.MaxLifePoints;
                pbLife.Value = data.LifePoints;
                pbEnergy.Maximum = data.MaxEnergyPoints;
                pbEnergy.Value = data.EnergyPoints;
                lblVie.Text = String.Format("Vitalité {0}/{1}", HumanNumber(data.LifePoints), HumanNumber(data.MaxLifePoints));
                lblEnergy.Text = String.Format("Énergie {0}/{1}", HumanNumber(data.EnergyPoints), HumanNumber(data.MaxEnergyPoints));
                RefreshCharac(data);
            }));
        }

        private void BaseInformation_changed(CharacterBaseInformations data)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (pictureBox1.Tag == null)
                {
                    string headName = String.Format("{0}{1}_{2}", data.Breed, data.Sex ? "1" : "0", data.EntityLook.BonesId);
                    pictureBox1.Image = new Bitmap(String.Format("data/cosmetics/{0}.png", headName));
                    pictureBox1.Tag = true;
                    lblName.Text = data.Name;
                }
                Lblxp.Text = String.Format("Niveau {0}", data.Level);
            }));
        }

        #endregion

        #region Settings

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().Save();
        }

        private void UserConfig_changed(Engine.Bot.Data.UserConfig data)
        {
            this.BeginInvoke(new Action(RefreshSettings));
        }

        private void RefreshSettings()
        {
            UserConfig cfg = bot.State.UserConfig.Get();
            if (cfg != null)
            {
                settingGrp1.Enabled = true;
                settingsGrp2.Enabled = true;
                settingsGrp3.Enabled = true;
                settingsGrp4.Enabled = true;
                settingsGrp5.Enabled = true;
                btnCfgExport.Enabled = true;
                btnCfgImport.Enabled = true;
                btnCfgSave.Enabled = true;
            }
            else
                return;
            placeCmbb.SelectedIndex = (int)cfg.FightPlacement;
            iaCmbb.SelectedIndex = (int)cfg.Ia;
            chkEmoteRegen.Checked = cfg.RegenEmote;
            chkLimitGrp.Checked = cfg.OnlyGroup;
            chkObjectRegen.Checked = cfg.RegenObject;
            chkEmoteRegen.Checked = cfg.RegenEmote;
            nudGroupMax.Value = cfg.GroupLevelMax;
            nudGroupMin.Value = cfg.GroupLevelMin;
            nudMonsterMax.Value = cfg.MonsterLevelMax;
            nudMonsterMin.Value = cfg.MonsterLevelMin;

            lstSpellStack.Items.Clear();
            lstSpellStack.Items.AddRange(cfg.SpellStack.ToArray<object>());
        }

        private void chkEmoteRegen_CheckedChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().RegenEmote = chkEmoteRegen.Checked;
        }

        private void chkObjectRegen_CheckedChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().RegenObject = chkObjectRegen.Checked;
        }

        private void chkSpecta_CheckedChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().AllowWatcher = chkSpecta.Checked;
        }

        private void placeCmbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().FightPlacement = (FightPlacementType)placeCmbb.SelectedIndex;
        }

        private void iaCmbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().Ia = (FightIaType)iaCmbb.SelectedIndex;
        }

        private void chkLimitGrp_CheckedChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().OnlyGroup = chkLimitGrp.Checked;
        }

        private void chkSkipPopulate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void nudGroupMax_ValueChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().GroupLevelMax = (int)nudGroupMax.Value;
        }

        private void nudGroupMin_ValueChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().GroupLevelMin = (int)nudGroupMin.Value;
        }

        private void nudMonsterMax_ValueChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().MonsterLevelMax = (int)nudMonsterMax.Value;
        }

        private void nudMonsterMin_ValueChanged(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().MonsterLevelMin = (int)nudMonsterMin.Value;
        }

        #region SpellStack

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstSpellStack.SelectedItem != null)
            {
                bot.State.UserConfig.Get().SpellStack.Remove((SpellStackItem)lstSpellStack.SelectedItem);
                bot.State.UserConfig.OnChanged();
            }
        }

        private void viderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bot.State.UserConfig.Get().SpellStack.Clear();
            bot.State.UserConfig.OnChanged();
        }

        private void ajouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSpell frm = new AddSpell(bot);
            frm.ShowDialog();
        }

        private void ReOrderSpellStack()
        {
            bot.State.UserConfig.Get().SpellStack = bot.State.UserConfig.Get().SpellStack.OrderBy((elem) => elem.Priority).ToList();
            bot.State.UserConfig.OnChanged();
        }

        private void monterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstSpellStack.SelectedItem == null || lstSpellStack.SelectedIndex == 0 || lstSpellStack.Items.Count == 1)
                return;
            SpellStackItem prev = (SpellStackItem)lstSpellStack.Items[lstSpellStack.SelectedIndex - 1];
            SpellStackItem current = (SpellStackItem)lstSpellStack.Items[lstSpellStack.SelectedIndex];
            int tmp = prev.Priority;
            prev.Priority = current.Priority;
            current.Priority = tmp;
            ReOrderSpellStack();
        }

        private void descendreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstSpellStack.SelectedItem == null || lstSpellStack.SelectedIndex == lstSpellStack.Items.Count - 1 || lstSpellStack.Items.Count == 1)
                return;
            SpellStackItem next = (SpellStackItem)lstSpellStack.Items[lstSpellStack.SelectedIndex + 1];
            SpellStackItem current = (SpellStackItem)lstSpellStack.Items[lstSpellStack.SelectedIndex];
            int tmp = next.Priority;
            next.Priority = current.Priority;
            current.Priority = tmp;
            ReOrderSpellStack();
        }

        #endregion

        #endregion

        #region Popup

        private void ServerManager_ServerSelectionRequired()
        {
            this.BeginInvoke(new Action(() => { new ServerSelectionForm(bot).Show(); }));
        }

        private void ServerManager_NoCharacterAvaiable()
        {
            this.BeginInvoke(new Action(() =>
            {
                new CharacterCreationForm(bot).Show();
            }));
        }

        private void AuthManager_NickNameRegistrationRequired(Engine.Bot.Managers.AuthManager obj)
        {
            NickNameRegistration dial;
            this.BeginInvoke(new Action(() =>
            {
                dial = new NickNameRegistration() { AccountName = bot.Config.Username };
                dial.NickNameSelected += (sender, nick) => { bot.AuthManager.SelectNickName(nick); };
                bot.AuthManager.NickNameRegistrationResult += (sender, result) => { if (result) dial.Done(); else dial.Error(); };
                dial.Show();
            }));
        }

        #endregion

        #region Map

        private void InitMap()
        {
            // Map
            map = new MapControl();
            map.Dock = DockStyle.Fill;
            map.CellClicked += Map_CellClicked;
            kryptonPanel3.Controls.Add(map);
            // Map refresh handler
            this.bot.ElementsManager.Actors.changed += (e) => { RefreshInvoke(); };
            this.bot.ElementsManager.Elements.changed += (e) => { RefreshInvoke(); };
            this.bot.State.Player.RolePlayInformations.changed += (e) => { RefreshInvoke(); };
            this.bot.FightManager.State.changed += (e) => { RefreshInvoke(); }; ;
            this.bot.FightManager.Fighters.changed += (e) => { RefreshInvoke(); }; ;
            this.bot.FightManager.EntitiesDispositons.changed += (e) => { RefreshInvoke(); };
            this.bot.State.DebugCells.changed += (e) => { RefreshInvoke(); };
            this.bot.State.Connected.changed += Connected_changed;
            this.bot.FightManager.CastSpellUpdate += FightManager_CastSpellUpdate;
            this.bot.PlayerManager.Movement.MovementPerformed += Movement_MovementPerformed;
        }

        List<CellWithOrientation> Path = new List<CellWithOrientation>();
        CellState PathState;
        private void Movement_MovementPerformed(List<CellWithOrientation> arg1, int arg2, bool arg3)
        {
            this.BeginInvoke(new Action(async () =>
            {
                Path = arg1;
                PathState = arg3 ? CellState.Road : CellState.RedPlacement;
                RefreshMap(bot.State.CurrentMap.Get());
                await Task.Delay(arg2);
                Path = null;
                RefreshMap(bot.State.CurrentMap.Get());
            }));
        }

        #region Mapactions

        private async void Map_CellClicked(MapControl control, MapCell cell, MouseButtons buttons, bool hold)
        {
            try
            {
                if (buttons == MouseButtons.Left)
                {
                    if (bot.FightManager.IsFighting)
                        return;
                    foreach (GroupMonsterDetails group in bot.ElementsManager.GroupMonster.Get().Values)
                    {
                        if (group.Informatons.Disposition.CellId == cell.Id)
                        {
                            bot.PlayerManager.AttackGroupMonster(group);
                            return;
                        }
                    }
                    foreach (ElementDetails element in bot.ElementsManager.Elements.Get().Values)
                    {
                        if (element.CellId == cell.Id)
                        {
                            if (element.EnabelSkills.Count > 0)
                                bot.PlayerManager.UseInteractive(element, element.EnabelSkills[0]);
                            return;
                        }
                    }
                    if (!await bot.PlayerManager.Movement.Move(cell.Id, true))
                    {
                        this.map.Cells[cell.Id].State = CellState.RedPlacement;
                        return;
                    }
                    RefreshMap(this.bot.State.CurrentMap);
                }
                else if (bot.ElementsManager.ElementByCellId((short)cell.Id) != null)
                {
                    mapMenu.Items.Clear();
                    ElementDetails elem = bot.ElementsManager.ElementByCellId((short)cell.Id);
                    foreach (SkillDetails skill in elem.EnabelSkills)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = skill.Name;
                        item.Click += (object sender, EventArgs e) =>
                        {
                            bot.PlayerManager.UseInteractive(elem, skill);
                        };
                        mapMenu.Items.Add(item);
                    }
                    ToolStripMenuItem copy = new ToolStripMenuItem();
                    copy.Text = "Copy id (" + elem.Interactive.ElementId + ")";
                    copy.Click += (e, s) =>
                    {
                        Clipboard.SetText(elem.Interactive.ElementId.ToString());
                    };
                    mapMenu.Items.Add(copy);
                    mapMenu.Show(Cursor.Position);
                }
                else if (bot.ElementsManager.NpcByCellId((short)cell.Id) != null)
                {
                    mapMenu.Items.Clear();
                    NpcDetails elem = bot.ElementsManager.NpcByCellId((short)cell.Id);
                    foreach (string resp in elem.Actions.Keys)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = resp;
                        item.Click += (object sender, EventArgs e) =>
                        {
                        };
                        mapMenu.Items.Add(item);
                    }
                    ToolStripMenuItem copy = new ToolStripMenuItem();
                    copy.Text = "Copy id (" + elem.Informations.ContextualId + ")";
                    copy.Click += (e, s) =>
                    {
                        Clipboard.SetText(elem.Informations.ContextualId.ToString());
                    };
                    mapMenu.Items.Add(copy);
                    mapMenu.Show(Cursor.Position);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion

        #region MapRefresh

        private void FightManager_CastSpellUpdate(short cell, bool finish, bool success)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.map.Cells[cell].State = finish ? (success ? CellState.BluePlacement : CellState.RedPlacement) : CellState.Road;
                this.map.Refresh();
            }));
        }


        private void RefreshInvoke()
        {
            this.BeginInvoke(new Action(() =>
            {
                RefreshMap(bot.State.CurrentMap);
            }));
        }

        private void RefreshMap(Map map)
        {
            try
            {
                if (map == null)
                    return;
                RefreshMapCommon(map);
                if (bot.FightManager.IsFighting)
                    RefreshMapFight(map);
                else
                    RefreshMapRp(map);
                if (Path != null)
                    foreach (CellWithOrientation c in Path)
                        this.map.Cells[c.Id].State = PathState;
                this.map.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Failed to refresh map !");
            }
        }

        private void RefreshMapCommon(Map map)
        {
            for (int i = 0; i < map.Cells.Count; i++)
            {
                CellData cell = map.Cells[i];
                this.map.Cells[i].Shape = Shape.None;
                if (bot.FightManager.IsFighting)
                    this.map.Cells[i].State = (cell.allowWalkFight && cell.mov) ? CellState.Walkable : (cell.los ? CellState.Los : CellState.NonWalkable);
                else
                    this.map.Cells[i].State = (cell.allowWalkRP && cell.mov) ? CellState.Walkable : (cell.los ? CellState.Los : CellState.NonWalkable);
                this.map.Cells[i].Text = chkCellid.Checked ? PathingUtils.CellIdToCoord((short)i).ToString() : "";
            }
            foreach (DebugCellHandler handle in bot.State.DebugCells.Get())
            {
                foreach (short cell in handle.Cells)
                {
                    this.map.Cells[cell].State = handle.State == 0 ? CellState.BluePlacement : CellState.RedPlacement;
                }
            }
        }

        private void RefreshMapFight(Map map)
        {
            int[] used = bot.ElementsManager.UsedCells;

            if (bot.FightManager.Fighters.Get() != null)
            {
                foreach (FighterDetails detail in bot.FightManager.Fighters.Get().Values)
                {
                    if (!bot.FightManager.EntitiesDispositons.Get().ContainsKey(detail.Fighter.ContextualId))
                        continue;
                    short cid = bot.FightManager.EntitiesDispositons.Get()[detail.Fighter.ContextualId];
                    this.map.Cells[cid].Shape = detail.Fighter.TeamId == bot.FightManager.Player.Fighter.TeamId ? Shape.CircleGreen : Shape.CircleRed;
                }
                if (chkActorId.Checked)
                    foreach (FighterDetails detail in bot.FightManager.Fighters.Get().Values)
                        if (bot.FightManager.EntitiesDispositons.Get().ContainsKey(detail.Fighter.ContextualId))
                            this.map.Cells[bot.FightManager.EntitiesDispositons.Get()[detail.Fighter.ContextualId]].Text += String.Format(" - {0}", detail.Fighter.ContextualId.ToString());
            }
            if (bot.FightManager.Player != null && bot.FightManager.EntitiesDispositons.Get().ContainsKey(bot.FightManager.Player.Fighter.ContextualId))
                this.map.Cells[bot.FightManager.EntitiesDispositons.Get()[bot.FightManager.Player.Fighter.ContextualId]].Shape = Shape.CircleBlue;
            if (bot.FightManager.PlacementForDefender != null)
                foreach (short cell in bot.FightManager.PlacementForDefender)
                    this.map.Cells[cell].State = CellState.BluePlacement;

        }

        private void RefreshMapRp(Map map)
        {
            if (map == null)
                return;
            int[] used = bot.ElementsManager.UsedCells;
            for (int i = 0; i < map.Cells.Count; i++)
            {
                CellData cell = map.Cells[i];
                if (!(cell.allowWalkRP && cell.mov) || cell.mapChangeData == 0)
                    continue;
                DirectionsEnum dir = MovementManager.GetChangeMapDirection((short)i, cell.mapChangeData);
                if (dir == DirectionsEnum.DIRECTION_NORTH)
                    this.map.Cells[i].Text += "N";
                else if (dir == DirectionsEnum.DIRECTION_SOUTH)
                    this.map.Cells[i].Text += "S";
                else if (dir == DirectionsEnum.DIRECTION_WEST)
                    this.map.Cells[i].Text += "W";
                else if (dir == DirectionsEnum.DIRECTION_EAST)
                    this.map.Cells[i].Text += "E";
            }
            if (bot.ElementsManager.Actors.Get() != null)
            {
                foreach (GameContextActorInformations actor in bot.ElementsManager.Actors.Get().Values)
                {
                    if (actor is GameRolePlayNamedActorInformations)
                    {
                        this.map.Cells[actor.Disposition.CellId].Shape = Shape.CircleBlue;
                        this.map.Cells[actor.Disposition.CellId].Text = ((GameRolePlayNamedActorInformations)actor).Name;
                    }
                    else if (actor is GameRolePlayGroupMonsterInformations)
                    {
                        this.map.Cells[actor.Disposition.CellId].Shape = Shape.CircleRed;
                        this.map.Cells[actor.Disposition.CellId].Text += bot.ElementsManager.GroupMonster.Get()[actor.ContextualId].LeaderName;
                    }
                }
                foreach (NpcDetails npc in bot.ElementsManager.Npc.Get().Values)
                {
                    this.map.Cells[npc.Informations.Disposition.CellId].Shape = Shape.CircleGreen;
                    this.map.Cells[npc.Informations.Disposition.CellId].Text += npc.Name;
                }
            }
            if (bot.ElementsManager.Elements.Get() != null)
                foreach (ElementDetails elem in bot.ElementsManager.Elements.Get().Values)
                {
                    this.map.Cells[elem.CellId].Shape = elem.IsActive ? Shape.Door : Shape.DoorInactive;
                    this.map.Cells[elem.CellId].Text += elem.Name;
                }
        }

        #endregion

        #endregion

        #region ToolBarButtons

        private void BotControl_Load(object sender, EventArgs e)
        {
            bot.Group.Login(bot.Config.Username);
        }

        private void Connected_changed(bool data)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (data)
                    btnConnect.Text = "Logout";
                else
                    btnConnect.Text = "Login";
            }));
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Login")
            {
                bot.Group.Login(bot.Config.Username);
            }
            else
            {
                bot.Group.Logout(bot.Config.Username);
            }
        }

        #endregion

        #region Logging

        /// <summary>
        /// Logging
        /// </summary>
        private void Logger_OnLog(string log, LogLevelEnum logLevel)
        {
            this.BeginInvoke(new Action(() =>
            {
                txtConsole.AppendText(String.Format("{0}\n", log), logLevel == LogLevelEnum.Error ? Color.Red : (logLevel == LogLevelEnum.Warning) ? Color.Orange : Color.Green);
                txtConsole.ScrollToCaret();
            }));
        }

        private void NetworkLogger_OnLogObj(object log, LogLevelEnum logLevel)
        {

            if (log is PacketLogContainer && chkSniffActivate.Checked)
            {
                PacketLogContainer recv = (PacketLogContainer)log;
                this.BeginInvoke(new Action(() =>
                {
                    if (lstPackets.Items.Count > 1000)
                        lstPackets.Items.RemoveAt(0);
                    lstPackets.Items.Add(recv);
                    lstPackets.TopIndex = lstPackets.Items.Count - 1;
                }));
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
        }

        private void lstPackets_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                PacketLogContainer packet = (PacketLogContainer)lstPackets.SelectedItem;
                pgPacket.SelectedObject = packet.Message;
            }
            catch {
            }
        }


        string[] filters = new string[0];
        private void kryptonTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filters = (from item in txtFilter.Text.Split(',') select item.Trim().ToLower()).ToArray();
            }
        }

        private void lstPackets_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index >= 0 && e.Index < lstPackets.Items.Count))
                return;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            var item = lstPackets.Items[e.Index];
            if (filters.Contains(item.ToString().Substring(1).Trim().ToLower()))
                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), e.Bounds);
            else if (item.ToString().Contains(">"))
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#7aadff")), e.Bounds);
            else
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#146eff")), e.Bounds);

            e.Graphics.DrawString(item.ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }



        #endregion

        #region Inventory and characteristic

        private void RefreshCharac(CharacterCharacteristicsInformations info)
        {
            // Vitality
            lstCharac.Items[0].SubItems[1].Text = info.Vitality.Base.ToString();
            lstCharac.Items[0].SubItems[2].Text = info.Vitality.Additionnal.ToString();
            lstCharac.Items[0].SubItems[3].Text = (info.Vitality.Base + info.Vitality.Additionnal).ToString();
            // Wisodm
            lstCharac.Items[1].SubItems[1].Text = info.Wisdom.Base.ToString();
            lstCharac.Items[1].SubItems[2].Text = info.Wisdom.Additionnal.ToString();
            lstCharac.Items[1].SubItems[3].Text = (info.Wisdom.Base + info.Wisdom.Additionnal).ToString();
            // Strength
            lstCharac.Items[2].SubItems[1].Text = info.Strength.Base.ToString();
            lstCharac.Items[2].SubItems[2].Text = info.Strength.Additionnal.ToString();
            lstCharac.Items[2].SubItems[3].Text = (info.Strength.Base + info.Strength.Additionnal).ToString();
            // Intelligence
            lstCharac.Items[3].SubItems[1].Text = info.Intelligence.Base.ToString();
            lstCharac.Items[3].SubItems[2].Text = info.Intelligence.Additionnal.ToString();
            lstCharac.Items[3].SubItems[3].Text = (info.Intelligence.Base + info.Intelligence.Additionnal).ToString();
            // Chance
            lstCharac.Items[4].SubItems[1].Text = info.Chance.Base.ToString();
            lstCharac.Items[4].SubItems[2].Text = info.Chance.Additionnal.ToString();
            lstCharac.Items[4].SubItems[3].Text = (info.Chance.Base + info.Chance.Additionnal).ToString();
            // Agility
            lstCharac.Items[5].SubItems[1].Text = info.Agility.Base.ToString();
            lstCharac.Items[5].SubItems[2].Text = info.Agility.Additionnal.ToString();
            lstCharac.Items[5].SubItems[3].Text = (info.Agility.Base + info.Agility.Additionnal).ToString();
            // Prospec
            lstCharac.Items[6].SubItems[1].Text = info.Prospecting.Base.ToString();
            lstCharac.Items[6].SubItems[2].Text = info.Prospecting.Additionnal.ToString();
            lstCharac.Items[6].SubItems[3].Text = (info.Prospecting.Base + info.Prospecting.Additionnal).ToString();
            // PA esquive
            lstCharac.Items[7].SubItems[3].Text = ((info.Wisdom.Base + info.Wisdom.Additionnal) / 10).ToString();
            // PM esquive
            lstCharac.Items[8].SubItems[3].Text = ((info.Agility.Base + info.Agility.Additionnal) / 10).ToString();

            lstCharac.Items[9].SubItems[3].Text = info.StatsPoints.ToString();
        }

        private void InitInvCharac()
        {
            bot.State.Player.InventoryContent.changed += InventoryContent_changed;
            bot.State.Player.Kamas.changed += Kamas_changed;
        }

        private void Kamas_changed(long data)
        {
            this.BeginInvoke(new Action(() =>
            {
                lblKamas.Text = String.Format("Kamas : {0}", data.ToString());
            }));
        }

        private void InventoryContent_changed(List<InventoryItemDetail> data)
        {
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    lstItems.Items.Clear();
                    imgLstItems.Images.Clear();
                    foreach (InventoryItemDetail item in data)
                    {
                        ListViewItem x = new ListViewItem(new[] { item.Name });
                        imgLstItems.Images.Add(item.Image);
                        x.ImageIndex = imgLstItems.Images.Count - 1;
                        x.Text = String.Format("{0} ({1})", item.Name, item.Item.Quantity);
                        x.ToolTipText = item.Name;
                        x.Tag = item;
                        lstItems.Items.Add(x);
                    }
                    lstItems.SmallImageList = imgLstItems;
                    lstItems.LargeImageList = imgLstItems;
                }
                catch
                {
                    Console.WriteLine("Can''t refresh inventory");
                }
            }));
        }

        private void supprimerToutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstItems.SelectedItems)
            {
                InventoryItemDetail detail = (InventoryItemDetail)item.Tag;
                bot.InventoryManager.DeleteObject(detail, detail.Item.Quantity);
            }
        }

        private void copierLidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count > 0)
                Clipboard.SetText(((InventoryItemDetail)lstItems.SelectedItems[0].Tag).Item.ObjectGID.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            new ContextScriptEditor(bot).Show();
        }

        #endregion

    }
}
