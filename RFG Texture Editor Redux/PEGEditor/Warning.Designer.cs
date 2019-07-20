namespace PEGEditor
{
	// Token: 0x02000005 RID: 5
	public partial class Warning : global::System.Windows.Forms.Form
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002709 File Offset: 0x00000909
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002728 File Offset: 0x00000928
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PEGEditor.Warning));
			this.lblWarningTitle = new global::System.Windows.Forms.Label();
			this.lblWarning = new global::System.Windows.Forms.Label();
			this.btnClose = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.lblWarningTitle.AutoSize = true;
			this.lblWarningTitle.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblWarningTitle.ForeColor = global::System.Drawing.Color.FromArgb(192, 0, 0);
			this.lblWarningTitle.Location = new global::System.Drawing.Point(12, 9);
			this.lblWarningTitle.Name = "lblWarningTitle";
			this.lblWarningTitle.Size = new global::System.Drawing.Size(69, 18);
			this.lblWarningTitle.TabIndex = 0;
			this.lblWarningTitle.Text = "Warning:";
			this.lblWarning.AutoSize = true;
			this.lblWarning.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lblWarning.ForeColor = global::System.Drawing.Color.Black;
			this.lblWarning.Location = new global::System.Drawing.Point(28, 27);
			this.lblWarning.Name = "lblWarning";
			this.lblWarning.Size = new global::System.Drawing.Size(375, 54);
			this.lblWarning.TabIndex = 1;
			this.lblWarning.Text = "Please backup any file that you edit with this tool!\r\nThis tool does NOT make any backups, and it can not\r\nguarantee that all texure modifications will work.";
			this.btnClose.Location = new global::System.Drawing.Point(368, 84);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new global::System.Drawing.Size(41, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Ok";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new global::System.EventHandler(this.btnClose_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.White;
			base.ClientSize = new global::System.Drawing.Size(421, 117);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.lblWarning);
			base.Controls.Add(this.lblWarningTitle);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			//base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Warning";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RF:G Texture Editor by 0luke0";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000002 RID: 2
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000003 RID: 3
		private global::System.Windows.Forms.Label lblWarningTitle;

		// Token: 0x04000004 RID: 4
		private global::System.Windows.Forms.Label lblWarning;

		// Token: 0x04000005 RID: 5
		private global::System.Windows.Forms.Button btnClose;
	}
}
