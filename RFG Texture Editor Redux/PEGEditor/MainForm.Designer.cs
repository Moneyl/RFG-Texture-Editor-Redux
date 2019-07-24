using System.Windows.Forms;

namespace PEGEditor
{
	// Token: 0x02000011 RID: 17
	public partial class MainForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000036E7 File Offset: 0x000018E7
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003708 File Offset: 0x00001908
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miImport = new System.Windows.Forms.ToolStripMenuItem();
            this.miExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.miExtractAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlFileInfo = new System.Windows.Forms.Panel();
            this.lblNumTextures = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.spltMain = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.cmMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelLine = new System.Windows.Forms.Panel();
            this.fdOpen = new System.Windows.Forms.OpenFileDialog();
            this.fdImport = new System.Windows.Forms.OpenFileDialog();
            this.fdExtract = new System.Windows.Forms.SaveFileDialog();
            this.fdExtractAll = new System.Windows.Forms.FolderBrowserDialog();
            this.menuMain.SuspendLayout();
            this.pnlFileInfo.SuspendLayout();
            this.spltMain.Panel1.SuspendLayout();
            this.spltMain.Panel2.SuspendLayout();
            this.spltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.cmMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewToolStripMenuItem,
            this.helpMenu});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(801, 28);
            this.menuMain.TabIndex = 5;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen,
            this.miSave,
            this.toolStripMenuItem1,
            this.miImport,
            this.miExtract,
            this.miExtractAll,
            this.toolStripMenuItem2,
            this.miExit});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 24);
            this.fileMenu.Text = "File";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(259, 26);
            this.miOpen.Text = "Open Texture Library...";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // miSave
            // 
            this.miSave.Enabled = false;
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(259, 26);
            this.miSave.Text = "Save Changes";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(256, 6);
            // 
            // miImport
            // 
            this.miImport.Enabled = false;
            this.miImport.Name = "miImport";
            this.miImport.Size = new System.Drawing.Size(259, 26);
            this.miImport.Text = "Import Selected Texture...";
            this.miImport.Click += new System.EventHandler(this.miImport_Click);
            // 
            // miExtract
            // 
            this.miExtract.Enabled = false;
            this.miExtract.Name = "miExtract";
            this.miExtract.Size = new System.Drawing.Size(259, 26);
            this.miExtract.Text = "Extract Selected Texture...";
            this.miExtract.Click += new System.EventHandler(this.miExtract_Click);
            // 
            // miExtractAll
            // 
            this.miExtractAll.Enabled = false;
            this.miExtractAll.Name = "miExtractAll";
            this.miExtractAll.Size = new System.Drawing.Size(259, 26);
            this.miExtractAll.Text = "Extract All Files...";
            this.miExtractAll.Click += new System.EventHandler(this.miExtractAll_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(256, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(259, 26);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem3.Text = "Solid background";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(55, 24);
            this.helpMenu.Text = "Help";
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(224, 26);
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // pnlFileInfo
            // 
            this.pnlFileInfo.BackColor = System.Drawing.Color.White;
            this.pnlFileInfo.Controls.Add(this.lblNumTextures);
            this.pnlFileInfo.Controls.Add(this.lblFile);
            this.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileInfo.Location = new System.Drawing.Point(0, 28);
            this.pnlFileInfo.Name = "pnlFileInfo";
            this.pnlFileInfo.Size = new System.Drawing.Size(801, 46);
            this.pnlFileInfo.TabIndex = 6;
            // 
            // lblNumTextures
            // 
            this.lblNumTextures.AutoSize = true;
            this.lblNumTextures.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumTextures.Location = new System.Drawing.Point(4, 26);
            this.lblNumTextures.Name = "lblNumTextures";
            this.lblNumTextures.Size = new System.Drawing.Size(82, 19);
            this.lblNumTextures.TabIndex = 1;
            this.lblNumTextures.Text = "0 Textures";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile.Location = new System.Drawing.Point(3, 2);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(199, 31);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "No File Loaded";
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltMain.Location = new System.Drawing.Point(0, 75);
            this.spltMain.Name = "spltMain";
            // 
            // spltMain.Panel1
            // 
            this.spltMain.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.spltMain.Panel1.Controls.Add(this.tvMain);
            // 
            // spltMain.Panel2
            // 
            this.spltMain.Panel2.AutoScroll = true;
            this.spltMain.Panel2.BackColor = System.Drawing.Color.White;
            this.spltMain.Panel2.Controls.Add(this.pbMain);
            this.spltMain.Size = new System.Drawing.Size(801, 484);
            this.spltMain.SplitterDistance = 247;
            this.spltMain.TabIndex = 7;
            // 
            // tvMain
            // 
            this.tvMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.ForeColor = System.Drawing.Color.White;
            this.tvMain.HideSelection = false;
            this.tvMain.HotTracking = true;
            this.tvMain.ItemHeight = 20;
            this.tvMain.LineColor = System.Drawing.Color.White;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            this.tvMain.Size = new System.Drawing.Size(247, 484);
            this.tvMain.TabIndex = 0;
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
            // 
            // pbMain
            // 
            this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.pbMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbMain.BackgroundImage")));
            this.pbMain.ContextMenuStrip = this.cmMain;
            this.pbMain.InitialImage = null;
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(550, 484);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // cmMain
            // 
            this.cmMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiExtract,
            this.cmiImport});
            this.cmMain.Name = "cmMain";
            this.cmMain.Size = new System.Drawing.Size(170, 52);
            // 
            // cmiExtract
            // 
            this.cmiExtract.Enabled = false;
            this.cmiExtract.Name = "cmiExtract";
            this.cmiExtract.Size = new System.Drawing.Size(169, 24);
            this.cmiExtract.Text = "Extract Image";
            this.cmiExtract.Click += new System.EventHandler(this.miExtract_Click);
            // 
            // cmiImport
            // 
            this.cmiImport.Enabled = false;
            this.cmiImport.Name = "cmiImport";
            this.cmiImport.Size = new System.Drawing.Size(169, 24);
            this.cmiImport.Text = "Import Image";
            this.cmiImport.Click += new System.EventHandler(this.miImport_Click);
            // 
            // pixelLine
            // 
            this.pixelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pixelLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pixelLine.Location = new System.Drawing.Point(0, 74);
            this.pixelLine.Name = "pixelLine";
            this.pixelLine.Size = new System.Drawing.Size(801, 1);
            this.pixelLine.TabIndex = 8;
            // 
            // fdOpen
            // 
            this.fdOpen.Filter = "All Supported Files|*.cpeg_pc;*.cvbm_pc|PEG Files|*.cpeg_pc|VBM Files|*.cvbm_pc";
            // 
            // fdImport
            // 
            this.fdImport.Filter = "PNG Image|*.png";
            // 
            // fdExtract
            // 
            this.fdExtract.Filter = "PNG Image|*.png";
            // 
            // fdExtractAll
            // 
            this.fdExtractAll.Description = "Please select the folder you would like to extract the textrures to:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 559);
            this.Controls.Add(this.spltMain);
            this.Controls.Add(this.pixelLine);
            this.Controls.Add(this.pnlFileInfo);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFG Texture Editor Redux";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.pnlFileInfo.ResumeLayout(false);
            this.pnlFileInfo.PerformLayout();
            this.spltMain.Panel1.ResumeLayout(false);
            this.spltMain.Panel2.ResumeLayout(false);
            this.spltMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.cmMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000040 RID: 64
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.MenuStrip menuMain;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.ToolStripMenuItem fileMenu;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.ToolStripMenuItem miOpen;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.ToolStripMenuItem miSave;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.ToolStripMenuItem miImport;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.ToolStripMenuItem miExtract;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.ToolStripMenuItem miExit;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.ToolStripMenuItem helpMenu;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.ToolStripMenuItem miAbout;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.ToolStripMenuItem miExtractAll;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.Panel pnlFileInfo;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.Label lblNumTextures;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.Label lblFile;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.SplitContainer spltMain;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.Panel pixelLine;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.TreeView tvMain;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.OpenFileDialog fdOpen;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.OpenFileDialog fdImport;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.SaveFileDialog fdExtract;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.PictureBox pbMain;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.ContextMenuStrip cmMain;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.ToolStripMenuItem cmiExtract;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.ToolStripMenuItem cmiImport;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.FolderBrowserDialog fdExtractAll;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
    }
}
