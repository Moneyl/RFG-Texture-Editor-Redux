using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PEGEditor
{
	// Token: 0x02000005 RID: 5
	public partial class Warning : Form
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000029D2 File Offset: 0x00000BD2
		public Warning()
		{
			InitializeComponent();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029E0 File Offset: 0x00000BE0
		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
