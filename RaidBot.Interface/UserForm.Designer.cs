namespace RaidBot.Interface
{
    partial class UserForm
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
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.LogTab = new System.Windows.Forms.TabPage();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.CombatsTab = new System.Windows.Forms.TabPage();
            this.AddSpellButton = new System.Windows.Forms.Button();
            this.CibleLabel = new System.Windows.Forms.Label();
            this.SpellsLabel = new System.Windows.Forms.Label();
            this.CiblesComboBox = new System.Windows.Forms.ComboBox();
            this.SpellsComboBox = new System.Windows.Forms.ComboBox();
            this.SpellsListBox = new System.Windows.Forms.ListBox();
            this.MapTab = new System.Windows.Forms.TabPage();
            this.MainTabControl.SuspendLayout();
            this.LogTab.SuspendLayout();
            this.CombatsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.LogTab);
            this.MainTabControl.Controls.Add(this.CombatsTab);
            this.MainTabControl.Controls.Add(this.MapTab);
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(591, 542);
            this.MainTabControl.TabIndex = 0;
            // 
            // LogTab
            // 
            this.LogTab.Controls.Add(this.LogTextBox);
            this.LogTab.Location = new System.Drawing.Point(4, 22);
            this.LogTab.Name = "LogTab";
            this.LogTab.Padding = new System.Windows.Forms.Padding(3);
            this.LogTab.Size = new System.Drawing.Size(583, 516);
            this.LogTab.TabIndex = 0;
            this.LogTab.Text = "Log";
            this.LogTab.UseVisualStyleBackColor = true;
            // 
            // LogTextBox
            // 
            this.LogTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.LogTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogTextBox.Location = new System.Drawing.Point(8, 6);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(569, 448);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // CombatsTab
            // 
            this.CombatsTab.Controls.Add(this.AddSpellButton);
            this.CombatsTab.Controls.Add(this.CibleLabel);
            this.CombatsTab.Controls.Add(this.SpellsLabel);
            this.CombatsTab.Controls.Add(this.CiblesComboBox);
            this.CombatsTab.Controls.Add(this.SpellsComboBox);
            this.CombatsTab.Controls.Add(this.SpellsListBox);
            this.CombatsTab.Location = new System.Drawing.Point(4, 22);
            this.CombatsTab.Name = "CombatsTab";
            this.CombatsTab.Padding = new System.Windows.Forms.Padding(3);
            this.CombatsTab.Size = new System.Drawing.Size(583, 516);
            this.CombatsTab.TabIndex = 1;
            this.CombatsTab.Text = "Combats";
            this.CombatsTab.UseVisualStyleBackColor = true;
            // 
            // AddSpellButton
            // 
            this.AddSpellButton.Location = new System.Drawing.Point(321, 381);
            this.AddSpellButton.Name = "AddSpellButton";
            this.AddSpellButton.Size = new System.Drawing.Size(156, 23);
            this.AddSpellButton.TabIndex = 5;
            this.AddSpellButton.Text = "Ajouter";
            this.AddSpellButton.UseVisualStyleBackColor = true;
            this.AddSpellButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CibleLabel
            // 
            this.CibleLabel.AutoSize = true;
            this.CibleLabel.Location = new System.Drawing.Point(370, 248);
            this.CibleLabel.Name = "CibleLabel";
            this.CibleLabel.Size = new System.Drawing.Size(35, 13);
            this.CibleLabel.TabIndex = 4;
            this.CibleLabel.Text = "Cibles";
            // 
            // SpellsLabel
            // 
            this.SpellsLabel.AutoSize = true;
            this.SpellsLabel.Location = new System.Drawing.Point(370, 103);
            this.SpellsLabel.Name = "SpellsLabel";
            this.SpellsLabel.Size = new System.Drawing.Size(31, 13);
            this.SpellsLabel.TabIndex = 3;
            this.SpellsLabel.Text = "Sorts";
            // 
            // CiblesComboBox
            // 
            this.CiblesComboBox.FormattingEnabled = true;
            this.CiblesComboBox.Items.AddRange(new object[] {
            "Ennemis ",
            "Alliés",
            "Invocation",
            "Moi "});
            this.CiblesComboBox.Location = new System.Drawing.Point(306, 296);
            this.CiblesComboBox.Name = "CiblesComboBox";
            this.CiblesComboBox.Size = new System.Drawing.Size(194, 21);
            this.CiblesComboBox.TabIndex = 2;
            // 
            // SpellsComboBox
            // 
            this.SpellsComboBox.FormattingEnabled = true;
            this.SpellsComboBox.Location = new System.Drawing.Point(306, 142);
            this.SpellsComboBox.Name = "SpellsComboBox";
            this.SpellsComboBox.Size = new System.Drawing.Size(194, 21);
            this.SpellsComboBox.TabIndex = 1;
            // 
            // SpellsListBox
            // 
            this.SpellsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SpellsListBox.FormattingEnabled = true;
            this.SpellsListBox.Location = new System.Drawing.Point(22, 25);
            this.SpellsListBox.Name = "SpellsListBox";
            this.SpellsListBox.Size = new System.Drawing.Size(229, 457);
            this.SpellsListBox.TabIndex = 0;
            // 
            // MapTab
            // 
            this.MapTab.Location = new System.Drawing.Point(4, 22);
            this.MapTab.Name = "MapTab";
            this.MapTab.Padding = new System.Windows.Forms.Padding(3);
            this.MapTab.Size = new System.Drawing.Size(583, 516);
            this.MapTab.TabIndex = 2;
            this.MapTab.Text = "Map";
            this.MapTab.UseVisualStyleBackColor = true;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 544);
            this.Controls.Add(this.MainTabControl);
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
            this.MainTabControl.ResumeLayout(false);
            this.LogTab.ResumeLayout(false);
            this.CombatsTab.ResumeLayout(false);
            this.CombatsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage LogTab;
        private System.Windows.Forms.TabPage CombatsTab;
        public System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.Button AddSpellButton;
        private System.Windows.Forms.Label CibleLabel;
        private System.Windows.Forms.Label SpellsLabel;
        private System.Windows.Forms.ComboBox CiblesComboBox;
        public System.Windows.Forms.ComboBox SpellsComboBox;
        public System.Windows.Forms.ListBox SpellsListBox;
        private System.Windows.Forms.TabPage MapTab;
    }
}