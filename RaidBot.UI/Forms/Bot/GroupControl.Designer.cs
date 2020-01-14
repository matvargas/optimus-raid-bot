namespace RaidBot.UI
{
    partial class GroupControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabBot = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbbChef = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmbbTrajets = new System.Windows.Forms.ToolStripComboBox();
            this.btnSet = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabBot)).BeginInit();
            this.tabBot.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBot
            // 
            this.tabBot.Bar.BarOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.tabBot.Bar.ItemOrientation = ComponentFactory.Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.tabBot.Bar.ItemSizing = ComponentFactory.Krypton.Navigator.BarItemSizing.Individual;
            this.tabBot.Bar.TabStyle = ComponentFactory.Krypton.Toolkit.TabStyle.LowProfile;
            this.tabBot.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.tabBot.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.tabBot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBot.Location = new System.Drawing.Point(0, 25);
            this.tabBot.Name = "tabBot";
            this.tabBot.Size = new System.Drawing.Size(664, 401);
            this.tabBot.TabIndex = 2;
            this.tabBot.Text = "kryptonNavigator1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbbChef,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.cmbbTrajets,
            this.btnSet});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(664, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "Chef";
            // 
            // cmbbChef
            // 
            this.cmbbChef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbChef.Name = "cmbbChef";
            this.cmbbChef.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel2.Text = "Trajet";
            // 
            // cmbbTrajets
            // 
            this.cmbbTrajets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbTrajets.Name = "cmbbTrajets";
            this.cmbbTrajets.Size = new System.Drawing.Size(121, 25);
            // 
            // btnSet
            // 
            this.btnSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSet.Image = global::RaidBot.UI.Properties.Resources.cgreen;
            this.btnSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(23, 22);
            this.btnSet.ToolTipText = "attendre que tout les bots soient pret";
            this.btnSet.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // GroupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabBot);
            this.Controls.Add(this.toolStrip1);
            this.Name = "GroupControl";
            this.Size = new System.Drawing.Size(664, 426);
            this.Load += new System.EventHandler(this.GroupeWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabBot)).EndInit();
            this.tabBot.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Navigator.KryptonNavigator tabBot;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbbChef;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmbbTrajets;
        private System.Windows.Forms.ToolStripButton btnSet;
    }
}