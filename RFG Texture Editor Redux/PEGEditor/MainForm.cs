using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PEGEditor.Properties;
using RFGEdit.RFG.FileFormats;

namespace PEGEditor
{
	// Token: 0x02000011 RID: 17
	public partial class MainForm : Form
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004392 File Offset: 0x00002592
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000043AB File Offset: 0x000025AB
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000043AD File Offset: 0x000025AD
		private void miExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000043B8 File Offset: 0x000025B8
		private void miOpen_Click(object sender, EventArgs e)
		{
			this.fdOpen.ShowDialog(this);
			if (!File.Exists(this.fdOpen.FileName))
			{
				return;
			}
			this.cpeg = new FileInfo(this.fdOpen.FileName);
			if (Path.GetExtension(this.cpeg.Name).ToLower() == ".cpeg_pc")
			{
				this.extension = "peg_pc";
			}
			else
			{
				this.extension = "vbm_pc";
			}
			if (!this.cpeg.Exists)
			{
				return;
			}
			this.gpeg = new FileInfo(Path.ChangeExtension(this.fdOpen.FileName, "g" + this.extension));
			if (!this.gpeg.Exists)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"Error: ",
					this.gpeg.Name,
					" doesn't exist. You must have both the c",
					this.extension,
					" and the g",
					this.extension,
					" files in the same place."
				}), "Error", MessageBoxButtons.OK);
				return;
			}
			this.currentPeg = new PegFile();
			Stream stream = File.OpenRead(Path.ChangeExtension(this.cpeg.FullName, "c" + this.extension));
			Stream stream2 = File.OpenRead(Path.ChangeExtension(this.gpeg.FullName, "g" + this.extension));
			this.currentPeg.Read(stream, stream2);
			stream.Close();
			stream2.Close();
			if (this.currentPeg.Entries.Count == 0)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"Invalid ",
					this.extension,
					": The supplied ",
					this.extension,
					" file has no images."
				}), "Error");
				return;
			}
			this.lblNumTextures.Text = this.currentPeg.Entries.Count.ToString() + " Textures";
			this.lblFile.Text = Path.ChangeExtension(this.cpeg.Name, "").TrimEnd(new char[]
			{
				'.'
			});
			this.tvMain.Nodes.Clear();
			foreach (PegEntry pegEntry in this.currentPeg.Entries)
			{
				TreeNode treeNode = new TreeNode(pegEntry.Name);
				treeNode.Tag = pegEntry;
				this.tvMain.Nodes.Add(treeNode);
			}
			this.tvMain.SelectedNode = this.tvMain.Nodes[0];
			this.unsavedChanges = false;
			this.miSave.Enabled = true;
			this.miExtractAll.Enabled = true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000046C4 File Offset: 0x000028C4
		private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.miExtract.Enabled = true;
			this.miImport.Enabled = true;
			this.cmiExtract.Enabled = true;
			this.cmiImport.Enabled = true;
			PegEntry pegEntry = this.tvMain.SelectedNode.Tag as PegEntry;
			this.currentBmp = pegEntry.FrameBitmaps[0];
			this.currentFilename = Path.ChangeExtension(pegEntry.Name, "png");
			this.pbMain.Image = this.currentBmp;
			this.currentEntry = pegEntry;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004757 File Offset: 0x00002957
		private void miAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show("RF:G Texture Editor\r\nCreated by 0luke0\r\nThanks to:\r\n\tGibbed\r\n\t  http://gib.me/\r\n\tThe RF:G Modding commmunity\r\n\t  community.redfaction.com", "Red Faction: Guerrilla Textrue Editor by 0luke0");
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000476C File Offset: 0x0000296C
		private void miExtract_Click(object sender, EventArgs e)
		{
			this.fdExtract.FileName = this.currentFilename;
			this.fdExtract.ShowDialog(this);
			if (this.fdExtract.FileName == this.currentFilename || this.fdExtract.FileName == "")
			{
				return;
			}
			this.currentBmp.Save(this.fdExtract.FileName);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000047E0 File Offset: 0x000029E0
		private void miImport_Click(object sender, EventArgs e)
		{
			this.fdImport.FileName = this.currentFilename;
			this.fdImport.ShowDialog(this);
			if (this.fdImport.FileName == this.currentFilename || this.fdImport.FileName == "")
			{
				return;
			}
			this.unsavedChanges = true;
			try
			{
				Bitmap bitmap = new Bitmap(this.fdImport.FileName);
				PegFrame value = this.currentEntry.Frames[0];
				value.Format = 407;
				value.Fps = 1; //Previously set a ushort here to 257, which is equivalent to setting these two to 1
                value.MipLevels = 1;
				this.currentEntry.FrameBitmaps[0] = bitmap;
				this.currentEntry.Frames[0] = value;
				this.currentEntry.data = PegFile.MakeByteArrayFromBitmap(bitmap);
				this.tvMain_AfterSelect(null, null);
			}
			catch (Exception ex)
			{
				ex.GetHashCode();
				MessageBox.Show("Error:\r\n\tCould not load bitmap from:\r\n\t" + this.fdImport.FileName);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000048F4 File Offset: 0x00002AF4
		private void miSave_Click(object sender, EventArgs e)
		{
			unsavedChanges = false;
			currentPeg.Write(Path.ChangeExtension(cpeg.FullName, "c" + extension), Path.ChangeExtension(gpeg.FullName, "g" + extension));
			MessageBox.Show("Changes saved to:\r\n\t" + Path.ChangeExtension(cpeg.Name, "c" + extension) + "\r\n\t" + Path.ChangeExtension(cpeg.Name, "g" + extension),
                "Save Complete");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000049B0 File Offset: 0x00002BB0
		private void miExtractAll_Click(object sender, EventArgs e)
		{
			this.fdExtractAll.SelectedPath = "";
			this.fdExtractAll.ShowDialog();
			if (this.fdExtractAll.SelectedPath == "")
			{
				return;
			}
			foreach (PegEntry pegEntry in this.currentPeg.Entries)
			{
				Bitmap bitmap = pegEntry.FrameBitmaps[0];
				bitmap.Save(this.fdExtractAll.SelectedPath + "\\" + Path.ChangeExtension(pegEntry.Name, "png"));
			}
			MessageBox.Show("Textures extracted to:\r\n\t" + this.fdExtractAll.SelectedPath, "Extract Complete");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004A90 File Offset: 0x00002C90
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.unsavedChanges)
			{
				return;
			}
			DialogResult dialogResult = MessageBox.Show("Do you want to save your changes to the CPEG/GPEG files?", "Unsaved changes!", MessageBoxButtons.YesNoCancel);
			if (dialogResult == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}
			if (dialogResult == DialogResult.Yes)
			{
				this.miSave_Click(null, null);
			}
		}

		// Token: 0x0400005B RID: 91
		private PegFile currentPeg;

		// Token: 0x0400005C RID: 92
		private PegEntry currentEntry;

		// Token: 0x0400005D RID: 93
		private Bitmap currentBmp;

		// Token: 0x0400005E RID: 94
		private string currentFilename;

		// Token: 0x0400005F RID: 95
		private FileInfo cpeg;

		// Token: 0x04000060 RID: 96
		private FileInfo gpeg;

		// Token: 0x04000061 RID: 97
		private bool unsavedChanges;

		// Token: 0x04000062 RID: 98
		private string extension = "peg_pc";
	}
}
