using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
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
			InitializeComponent();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000043AB File Offset: 0x000025AB
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000043AD File Offset: 0x000025AD
		private void miExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000043B8 File Offset: 0x000025B8
		private void miOpen_Click(object sender, EventArgs e)
		{
			fdOpen.ShowDialog(this);
			if (!File.Exists(fdOpen.FileName))
			{
				return;
			}
			cpeg = new FileInfo(fdOpen.FileName);
			if (Path.GetExtension(cpeg.Name).ToLower() == ".cpeg_pc")
			{
				extension = "peg_pc";
			}
			else
			{
				extension = "vbm_pc";
			}
			if (!cpeg.Exists)
			{
				return;
			}
			gpeg = new FileInfo(Path.ChangeExtension(fdOpen.FileName, "g" + extension));
			if (!gpeg.Exists)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"Error: ",
					gpeg.Name,
					" doesn't exist. You must have both the c",
					extension,
					" and the g",
					extension,
					" files in the same place."
				}), "Error", MessageBoxButtons.OK);
				return;
			}
			currentPeg = new PegFile();
			Stream stream = File.OpenRead(Path.ChangeExtension(cpeg.FullName, "c" + extension));
			Stream stream2 = File.OpenRead(Path.ChangeExtension(gpeg.FullName, "g" + extension));
			currentPeg.Read(stream, stream2);
			stream.Close();
			stream2.Close();
			if (currentPeg.Entries.Count == 0)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"Invalid ",
					extension,
					": The supplied ",
					extension,
					" file has no images."
				}), "Error");
				return;
			}
			lblNumTextures.Text = currentPeg.Entries.Count.ToString() + " Textures";
			lblFile.Text = Path.ChangeExtension(cpeg.Name, "").TrimEnd(new char[]
			{
				'.'
			});
			tvMain.Nodes.Clear();
			foreach (PegEntry pegEntry in currentPeg.Entries)
			{
				TreeNode treeNode = new TreeNode(pegEntry.Name);
				treeNode.Tag = pegEntry;
				tvMain.Nodes.Add(treeNode);
			}
			tvMain.SelectedNode = tvMain.Nodes[0];
			unsavedChanges = false;
			miSave.Enabled = true;
			miExtractAll.Enabled = true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000046C4 File Offset: 0x000028C4
		private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			miExtract.Enabled = true;
			miImport.Enabled = true;
			cmiExtract.Enabled = true;
			cmiImport.Enabled = true;
			PegEntry pegEntry = tvMain.SelectedNode.Tag as PegEntry;
			currentBmp = pegEntry.FrameBitmaps[0];
			currentFilename = Path.ChangeExtension(pegEntry.Name, "png");
			pbMain.Image = currentBmp;
			currentEntry = pegEntry;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004757 File Offset: 0x00002957
		private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "RFG Texture Editor Redux\n\nA patch of the original texture editor by 0luke0 made from decompiled source code.\n\ngithub repo: https://github.com/Moneyl/RFG-Texture-Editor-Redux\n\nThanks to Gibbed for his work on reversing the peg format and writing the original extraction code.",
                "RFG Texture Editor Redux");
        }

		// Token: 0x0600006F RID: 111 RVA: 0x0000476C File Offset: 0x0000296C
		private void miExtract_Click(object sender, EventArgs e)
		{
			fdExtract.FileName = currentFilename;
			fdExtract.ShowDialog(this);
			if (fdExtract.FileName == currentFilename || fdExtract.FileName == "")
			{
				return;
			}
			currentBmp.Save(fdExtract.FileName);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000047E0 File Offset: 0x000029E0
		private void miImport_Click(object sender, EventArgs e)
		{
			fdImport.FileName = currentFilename;
			fdImport.ShowDialog(this);
			if (fdImport.FileName == currentFilename || fdImport.FileName == "")
			{
				return;
			}
			unsavedChanges = true;
			try
			{
				Bitmap bitmap = new Bitmap(fdImport.FileName);
                //if (bitmap.PixelFormat == PixelFormat.Format24bppRgb) //really stored as bgr
                //{
                //    //Is rgb image, add alpha channel 

                //}
                //else if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) //really stored as bgra
                //{
                //    //Doesn't equal rgb or argb so complain about unknown or unsupported format
                //}
                //Todo: Update size in case texture size is different
				PegFrame value = currentEntry.Frames[0];
                value.Width = (ushort)bitmap.Width;
                value.Height = (ushort)bitmap.Height;
                value.SourceWidth = 36352;// (or -29184 if it was an int)... Seems this needs to be this value unless you're using an animated texture like vfx_container.cpeg_pc//(ushort)bitmap.Width;
                value.SourceHeight = (ushort)bitmap.Height;
				value.Format = (ushort)PegFormat.A8R8G8B8;
				value.Fps = 1; //Previously set a ushort here to 257, which is equivalent to setting these two to 1
                value.MipLevels = 1;
                value.Size = (uint)(bitmap.Width * bitmap.Height * 4); //Todo: This might be inaccurate now that all imports are being converted to DXT5
				currentEntry.FrameBitmaps[0] = bitmap; //format either ARGB or RGB
				currentEntry.Frames[0] = value; //Starts at last pixel of image and flips array because I dunno why
				currentEntry.data = PegFile.MakeByteArrayFromBitmap(bitmap); 
                currentEntry.Edited = true;
				tvMain_AfterSelect(null, null);
			}
			catch (Exception ex)
			{
				ex.GetHashCode();
				MessageBox.Show("Error: Could not load bitmap from " + fdImport.FileName + ", message: " + ex.Message);
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
			fdExtractAll.SelectedPath = "";
			fdExtractAll.ShowDialog();
			if (fdExtractAll.SelectedPath == "")
			{
				return;
			}
			foreach (PegEntry pegEntry in currentPeg.Entries)
			{
				Bitmap bitmap = pegEntry.FrameBitmaps[0];
				bitmap.Save(fdExtractAll.SelectedPath + "\\" + Path.ChangeExtension(pegEntry.Name, "png"));
			}
			MessageBox.Show("Textures extracted to:\r\n\t" + fdExtractAll.SelectedPath, "Extract Complete");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004A90 File Offset: 0x00002C90
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!unsavedChanges)
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
				miSave_Click(null, null);
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

        private Image CheckerboardBackgroundReference;

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool Checked = ((ToolStripMenuItem)sender).Checked;

            if (Checked) //Use solid background
            {
                //Hacky way to keep track of the checkerboard background image. 
                //Works for now but will need to be changed when the codebase is cleaned up.
                CheckerboardBackgroundReference = pbMain.BackgroundImage; 
                pbMain.BackgroundImage = null;
            }
            else //use checkerboard background
            {
                pbMain.BackgroundImage = CheckerboardBackgroundReference;
            }
        }
    }
}
