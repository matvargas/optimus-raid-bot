namespace RaidBot.Tools.CodeTraductor.Interface
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.spGrid = new System.Windows.Forms.SplitContainer();
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.cmsInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.cmsOutput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblStats = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBulkOutput = new System.Windows.Forms.TextBox();
            this.txtNulkInput = new System.Windows.Forms.TextBox();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.btnTranslat = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.spGrid)).BeginInit();
            this.spGrid.Panel1.SuspendLayout();
            this.spGrid.Panel2.SuspendLayout();
            this.spGrid.SuspendLayout();
            this.cmsInput.SuspendLayout();
            this.cmsOutput.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // spGrid
            // 
            this.spGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spGrid.Location = new System.Drawing.Point(3, 3);
            this.spGrid.Name = "spGrid";
            // 
            // spGrid.Panel1
            // 
            this.spGrid.Panel1.Controls.Add(this.txtInput);
            // 
            // spGrid.Panel2
            // 
            this.spGrid.Panel2.Controls.Add(this.txtOutput);
            this.spGrid.Size = new System.Drawing.Size(454, 179);
            this.spGrid.SplitterDistance = 150;
            this.spGrid.TabIndex = 0;
            // 
            // txtInput
            // 
            this.txtInput.ContextMenuStrip = this.cmsInput;
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(150, 179);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = "";
            // 
            // cmsInput
            // 
            this.cmsInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collerToolStripMenuItem,
            this.copierToolStripMenuItem,
            this.collerToolStripMenuItem1});
            this.cmsInput.Name = "cmsInput";
            this.cmsInput.Size = new System.Drawing.Size(114, 70);
            // 
            // collerToolStripMenuItem
            // 
            this.collerToolStripMenuItem.Name = "collerToolStripMenuItem";
            this.collerToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.collerToolStripMenuItem.Text = "Coller";
            this.collerToolStripMenuItem.Click += new System.EventHandler(this.collerToolStripMenuItem_Click);
            // 
            // copierToolStripMenuItem
            // 
            this.copierToolStripMenuItem.Name = "copierToolStripMenuItem";
            this.copierToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.copierToolStripMenuItem.Text = "Copier";
            this.copierToolStripMenuItem.Click += new System.EventHandler(this.copierToolStripMenuItem_Click);
            // 
            // collerToolStripMenuItem1
            // 
            this.collerToolStripMenuItem1.Name = "collerToolStripMenuItem1";
            this.collerToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.collerToolStripMenuItem1.Text = "Couper";
            this.collerToolStripMenuItem1.Click += new System.EventHandler(this.collerToolStripMenuItem1_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.ContextMenuStrip = this.cmsOutput;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(300, 179);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "";
            // 
            // cmsOutput
            // 
            this.cmsOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.cmsOutput.Name = "cmsInput";
            this.cmsOutput.Size = new System.Drawing.Size(114, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem1.Text = "Coller";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem2.Text = "Copier";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem3.Text = "Couper";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(468, 25);
            this.tsMain.TabIndex = 1;
            this.tsMain.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton1.Text = "Translat";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 211);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblStats);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtBulkOutput);
            this.tabPage1.Controls.Add(this.txtNulkInput);
            this.tabPage1.Controls.Add(this.pbMain);
            this.tabPage1.Controls.Add(this.btnTranslat);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bulk translator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(8, 159);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(56, 13);
            this.lblStats.TabIndex = 9;
            this.lblStats.Text = "En attente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "output directory :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "class and type directory :";
            // 
            // txtBulkOutput
            // 
            this.txtBulkOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBulkOutput.Location = new System.Drawing.Point(100, 39);
            this.txtBulkOutput.Name = "txtBulkOutput";
            this.txtBulkOutput.Size = new System.Drawing.Size(344, 20);
            this.txtBulkOutput.TabIndex = 5;
            this.txtBulkOutput.Text = "C:\\Users\\asyap\\Downloads\\RaidBot-master\\RaidBot-master\\DofusInvocker translated";
            // 
            // txtNulkInput
            // 
            this.txtNulkInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNulkInput.Location = new System.Drawing.Point(138, 11);
            this.txtNulkInput.Name = "txtNulkInput";
            this.txtNulkInput.Size = new System.Drawing.Size(306, 20);
            this.txtNulkInput.TabIndex = 6;
            this.txtNulkInput.Text = "C:\\Users\\asyap\\Downloads\\RaidBot-master\\RaidBot-master\\DofusInvocker";
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.Location = new System.Drawing.Point(8, 121);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(444, 27);
            this.pbMain.TabIndex = 1;
            // 
            // btnTranslat
            // 
            this.btnTranslat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTranslat.Location = new System.Drawing.Point(379, 154);
            this.btnTranslat.Name = "btnTranslat";
            this.btnTranslat.Size = new System.Drawing.Size(75, 23);
            this.btnTranslat.TabIndex = 0;
            this.btnTranslat.Text = "Translat";
            this.btnTranslat.UseVisualStyleBackColor = true;
            this.btnTranslat.Click += new System.EventHandler(this.btnTranslat_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.spGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Translator";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 236);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tsMain);
            this.MinimumSize = new System.Drawing.Size(192, 260);
            this.Name = "FrmMain";
            this.Text = "as3 to c# translator";
            this.spGrid.Panel1.ResumeLayout(false);
            this.spGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spGrid)).EndInit();
            this.spGrid.ResumeLayout(false);
            this.cmsInput.ResumeLayout(false);
            this.cmsOutput.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spGrid;
        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ContextMenuStrip cmsInput;
        private System.Windows.Forms.ToolStripMenuItem collerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collerToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cmsOutput;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBulkOutput;
        private System.Windows.Forms.TextBox txtNulkInput;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.Button btnTranslat;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblStats;
    }
}

