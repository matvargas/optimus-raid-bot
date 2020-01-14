namespace RaidBot.UI.Forms
{
    partial class TrajetManager
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chargerDansLideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lstTrajet = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterToolStripMenuItem,
            this.supprimerToolStripMenuItem,
            this.chargerDansLideToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 70);
            // 
            // ajouterToolStripMenuItem
            // 
            this.ajouterToolStripMenuItem.Name = "ajouterToolStripMenuItem";
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ajouterToolStripMenuItem.Text = "Ajouter";
            this.ajouterToolStripMenuItem.Click += new System.EventHandler(this.ajouterToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            // 
            // chargerDansLideToolStripMenuItem
            // 
            this.chargerDansLideToolStripMenuItem.Name = "chargerDansLideToolStripMenuItem";
            this.chargerDansLideToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.chargerDansLideToolStripMenuItem.Text = "Charger dans l\'ide";
            this.chargerDansLideToolStripMenuItem.Click += new System.EventHandler(this.chargerDansLideToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // lstTrajet
            // 
            this.lstTrajet.ContextMenuStrip = this.contextMenuStrip1;
            this.lstTrajet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTrajet.Location = new System.Drawing.Point(0, 0);
            this.lstTrajet.Name = "lstTrajet";
            this.lstTrajet.Size = new System.Drawing.Size(431, 450);
            this.lstTrajet.TabIndex = 1;
            // 
            // TrajetManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 450);
            this.Controls.Add(this.lstTrajet);
            this.Name = "TrajetManager";
            this.Text = "TrajetManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TrajetManager_FormClosed);
            this.Load += new System.EventHandler(this.TrajetManager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ajouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lstTrajet;
        private System.Windows.Forms.ToolStripMenuItem chargerDansLideToolStripMenuItem;
    }
}