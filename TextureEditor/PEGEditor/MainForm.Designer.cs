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
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PEGEditor.MainForm));
			this.menuMain = new global::System.Windows.Forms.MenuStrip();
			this.fileMenu = new global::System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new global::System.Windows.Forms.ToolStripMenuItem();
			this.miSave = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.miImport = new global::System.Windows.Forms.ToolStripMenuItem();
			this.miExtract = new global::System.Windows.Forms.ToolStripMenuItem();
			this.miExtractAll = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.miExit = new global::System.Windows.Forms.ToolStripMenuItem();
			this.helpMenu = new global::System.Windows.Forms.ToolStripMenuItem();
			this.miAbout = new global::System.Windows.Forms.ToolStripMenuItem();
			this.pnlFileInfo = new global::System.Windows.Forms.Panel();
			this.lblNumTextures = new global::System.Windows.Forms.Label();
			this.lblFile = new global::System.Windows.Forms.Label();
			this.spltMain = new global::System.Windows.Forms.SplitContainer();
			this.tvMain = new global::System.Windows.Forms.TreeView();
			this.pbMain = new global::System.Windows.Forms.PictureBox();
			this.cmMain = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmiExtract = new global::System.Windows.Forms.ToolStripMenuItem();
			this.cmiImport = new global::System.Windows.Forms.ToolStripMenuItem();
			this.pixelLine = new global::System.Windows.Forms.Panel();
			this.fdOpen = new global::System.Windows.Forms.OpenFileDialog();
			this.fdImport = new global::System.Windows.Forms.OpenFileDialog();
			this.fdExtract = new global::System.Windows.Forms.SaveFileDialog();
			this.fdExtractAll = new global::System.Windows.Forms.FolderBrowserDialog();
			this.menuMain.SuspendLayout();
			this.pnlFileInfo.SuspendLayout();
			this.spltMain.Panel1.SuspendLayout();
			this.spltMain.Panel2.SuspendLayout();
			this.spltMain.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pbMain).BeginInit();
			this.cmMain.SuspendLayout();
			base.SuspendLayout();
			this.menuMain.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileMenu,
				this.helpMenu
			});
			this.menuMain.Location = new global::System.Drawing.Point(0, 0);
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new global::System.Drawing.Size(801, 24);
			this.menuMain.TabIndex = 5;
			this.menuMain.Text = "menuStrip1";
			this.fileMenu.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.miOpen,
				this.miSave,
				this.toolStripMenuItem1,
				this.miImport,
				this.miExtract,
				this.miExtractAll,
				this.toolStripMenuItem2,
				this.miExit
			});
			this.fileMenu.Name = "fileMenu";
			this.fileMenu.Size = new global::System.Drawing.Size(37, 20);
			this.fileMenu.Text = "File";
			this.miOpen.Name = "miOpen";
			this.miOpen.Size = new global::System.Drawing.Size(208, 22);
			this.miOpen.Text = "Open Texture Library...";
			this.miOpen.Click += new global::System.EventHandler(this.miOpen_Click);
			this.miSave.Enabled = false;
			this.miSave.Name = "miSave";
			this.miSave.Size = new global::System.Drawing.Size(208, 22);
			this.miSave.Text = "Save Changes";
			this.miSave.Click += new global::System.EventHandler(this.miSave_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(205, 6);
			this.miImport.Enabled = false;
			this.miImport.Name = "miImport";
			this.miImport.Size = new global::System.Drawing.Size(208, 22);
			this.miImport.Text = "Import Selected Texture...";
			this.miImport.Click += new global::System.EventHandler(this.miImport_Click);
			this.miExtract.Enabled = false;
			this.miExtract.Name = "miExtract";
			this.miExtract.Size = new global::System.Drawing.Size(208, 22);
			this.miExtract.Text = "Extract Selected Texture...";
			this.miExtract.Click += new global::System.EventHandler(this.miExtract_Click);
			this.miExtractAll.Enabled = false;
			this.miExtractAll.Name = "miExtractAll";
			this.miExtractAll.Size = new global::System.Drawing.Size(208, 22);
			this.miExtractAll.Text = "Extract All Files...";
			this.miExtractAll.Click += new global::System.EventHandler(this.miExtractAll_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(205, 6);
			this.miExit.Name = "miExit";
			this.miExit.Size = new global::System.Drawing.Size(208, 22);
			this.miExit.Text = "Exit";
			this.miExit.Click += new global::System.EventHandler(this.miExit_Click);
			this.helpMenu.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.miAbout
			});
			this.helpMenu.Name = "helpMenu";
			this.helpMenu.Size = new global::System.Drawing.Size(44, 20);
			this.helpMenu.Text = "Help";
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new global::System.Drawing.Size(210, 22);
			this.miAbout.Text = "About RF:G Texture Editor";
			this.miAbout.Click += new global::System.EventHandler(this.miAbout_Click);
			this.pnlFileInfo.BackColor = global::System.Drawing.Color.White;
			this.pnlFileInfo.Controls.Add(this.lblNumTextures);
			this.pnlFileInfo.Controls.Add(this.lblFile);
			this.pnlFileInfo.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.pnlFileInfo.Location = new global::System.Drawing.Point(0, 24);
			this.pnlFileInfo.Name = "pnlFileInfo";
			this.pnlFileInfo.Size = new global::System.Drawing.Size(801, 46);
			this.pnlFileInfo.TabIndex = 6;
			this.lblNumTextures.AutoSize = true;
			this.lblNumTextures.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblNumTextures.Location = new global::System.Drawing.Point(4, 26);
			this.lblNumTextures.Name = "lblNumTextures";
			this.lblNumTextures.Size = new global::System.Drawing.Size(68, 16);
			this.lblNumTextures.TabIndex = 1;
			this.lblNumTextures.Text = "0 Textures";
			this.lblFile.AutoSize = true;
			this.lblFile.Font = new global::System.Drawing.Font("Arial", 15.75f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblFile.Location = new global::System.Drawing.Point(3, 2);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new global::System.Drawing.Size(156, 24);
			this.lblFile.TabIndex = 0;
			this.lblFile.Text = "No File Loaded";
			this.spltMain.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.spltMain.FixedPanel = global::System.Windows.Forms.FixedPanel.Panel1;
			this.spltMain.Location = new global::System.Drawing.Point(0, 71);
			this.spltMain.Name = "spltMain";
			this.spltMain.Panel1.BackColor = global::System.Drawing.Color.FromArgb(64, 64, 64);
			this.spltMain.Panel1.Controls.Add(this.tvMain);
			this.spltMain.Panel2.BackColor = global::System.Drawing.Color.White;
			///this.spltMain.Panel2.BackgroundImage = global::PEGEditor.Properties.Resources.Checker2;
			this.spltMain.Panel2.Controls.Add(this.pbMain);
			this.spltMain.Size = new global::System.Drawing.Size(801, 488);
			this.spltMain.SplitterDistance = 247;
			this.spltMain.TabIndex = 7;
			this.tvMain.BackColor = global::System.Drawing.Color.FromArgb(64, 64, 64);
			this.tvMain.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tvMain.ForeColor = global::System.Drawing.Color.White;
			this.tvMain.HideSelection = false;
			this.tvMain.HotTracking = true;
			this.tvMain.ItemHeight = 20;
			this.tvMain.LineColor = global::System.Drawing.Color.White;
			this.tvMain.Location = new global::System.Drawing.Point(0, 0);
			this.tvMain.Name = "tvMain";
			this.tvMain.Size = new global::System.Drawing.Size(247, 488);
			this.tvMain.TabIndex = 0;
			this.tvMain.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
			this.pbMain.BackColor = global::System.Drawing.Color.Transparent;
			this.pbMain.ContextMenuStrip = this.cmMain;
			this.pbMain.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.pbMain.Location = new global::System.Drawing.Point(0, 0);
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new global::System.Drawing.Size(550, 488);
			this.pbMain.TabIndex = 0;
			this.pbMain.TabStop = false;
			this.cmMain.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.cmiExtract,
				this.cmiImport
			});
			this.cmMain.Name = "cmMain";
			this.cmMain.Size = new global::System.Drawing.Size(147, 48);
			this.cmiExtract.Enabled = false;
			this.cmiExtract.Name = "cmiExtract";
			this.cmiExtract.Size = new global::System.Drawing.Size(146, 22);
			this.cmiExtract.Text = "Extract Image";
			this.cmiExtract.Click += new global::System.EventHandler(this.miExtract_Click);
			this.cmiImport.Enabled = false;
			this.cmiImport.Name = "cmiImport";
			this.cmiImport.Size = new global::System.Drawing.Size(146, 22);
			this.cmiImport.Text = "Import Image";
			this.cmiImport.Click += new global::System.EventHandler(this.miImport_Click);
			this.pixelLine.BackColor = global::System.Drawing.Color.FromArgb(64, 64, 64);
			this.pixelLine.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.pixelLine.Location = new global::System.Drawing.Point(0, 70);
			this.pixelLine.Name = "pixelLine";
			this.pixelLine.Size = new global::System.Drawing.Size(801, 1);
			this.pixelLine.TabIndex = 8;
			this.fdOpen.Filter = "All Supported Files|*.cpeg_pc;*.cvbm_pc|PEG Files|*.cpeg_pc|VBM Files|*.cvbm_pc";
			this.fdImport.Filter = "PNG Image|*.png";
			this.fdExtract.Filter = "PNG Image|*.png";
			this.fdExtractAll.Description = "Please select the folder you would like to extract the textrures to:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 14f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(801, 559);
			base.Controls.Add(this.spltMain);
			base.Controls.Add(this.pixelLine);
			base.Controls.Add(this.pnlFileInfo);
			base.Controls.Add(this.menuMain);
			this.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			///base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuMain;
			base.Name = "MainForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RF:G Texture Editor by 0luke0";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.pnlFileInfo.ResumeLayout(false);
			this.pnlFileInfo.PerformLayout();
			this.spltMain.Panel1.ResumeLayout(false);
			this.spltMain.Panel2.ResumeLayout(false);
			this.spltMain.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pbMain).EndInit();
			this.cmMain.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
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
	}
}
