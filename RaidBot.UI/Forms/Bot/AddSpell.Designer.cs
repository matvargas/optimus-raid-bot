namespace RaidBot.UI.Forms.Bot
{
    partial class AddSpell
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
            this.lstSpell = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.lblError = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Priority = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.scintilla = new ScintillaNET.Scintilla();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lblError)).BeginInit();
            this.SuspendLayout();
            // 
            // lstSpell
            // 
            this.lstSpell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSpell.Location = new System.Drawing.Point(12, 12);
            this.lstSpell.Name = "lstSpell";
            this.lstSpell.Size = new System.Drawing.Size(196, 212);
            this.lstSpell.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblError.DropDownWidth = 232;
            this.lblError.Items.AddRange(new object[] {
            "Sur les ennemies",
            "Sur les allies",
            "Sur moi",
            "A cotee des ennemies",
            "A cotee des allies",
            "A cotee de moi"});
            this.lblError.Location = new System.Drawing.Point(214, 175);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(369, 21);
            this.lblError.TabIndex = 1;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButton1.Location = new System.Drawing.Point(493, 199);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 6;
            this.kryptonButton1.Values.Text = "Ajouter";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonLabel1.Location = new System.Drawing.Point(214, 202);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(50, 22);
            this.kryptonLabel1.TabIndex = 8;
            this.kryptonLabel1.Values.Text = "Priorité";
            // 
            // Priority
            // 
            this.Priority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Priority.Location = new System.Drawing.Point(270, 202);
            this.Priority.Name = "Priority";
            this.Priority.Size = new System.Drawing.Size(80, 22);
            this.Priority.TabIndex = 9;
            // 
            // scintilla
            // 
            this.scintilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla.Location = new System.Drawing.Point(214, 12);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(369, 143);
            this.scintilla.TabIndex = 12;
            this.scintilla.Text = "function check()\r\n    return true\r\nend\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "No error found";
            // 
            // AddSpell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 236);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scintilla);
            this.Controls.Add(this.Priority);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lstSpell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 182);
            this.Name = "AddSpell";
            this.Text = "AddSpell";
            this.Load += new System.EventHandler(this.AddSpell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonListBox lstSpell;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox lblError;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown Priority;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.Label label1;
    }
}