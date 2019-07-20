using System;
using System.Windows.Forms;

namespace PEGEditor
{
	// Token: 0x02000006 RID: 6
	internal static class Program
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000029E8 File Offset: 0x00000BE8
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Warning());
			Application.Run(new MainForm());
		}
	}
}
