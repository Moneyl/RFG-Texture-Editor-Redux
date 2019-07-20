using System;
using System.Runtime.InteropServices;

namespace RFGEdit.RFG.FileFormats
{
	// Token: 0x02000007 RID: 7
	public class Squish
	{
		// Token: 0x06000036 RID: 54
		[DllImport("squish.dll", EntryPoint = "DecompressImage")]
		public static extern void Decompress([MarshalAs(UnmanagedType.LPArray)] byte[] rgba, uint width, uint height, [MarshalAs(UnmanagedType.LPArray)] byte[] blocks, int flags);

		// Token: 0x02000008 RID: 8
		public enum Flags
		{
			// Token: 0x04000007 RID: 7
			DXT1 = 1,
			// Token: 0x04000008 RID: 8
			DXT3,
			// Token: 0x04000009 RID: 9
			DXT5 = 4,
			// Token: 0x0400000A RID: 10
			ColourClusterFit = 8,
			// Token: 0x0400000B RID: 11
			ColourRangeFit = 16,
			// Token: 0x0400000C RID: 12
			ColourMetricPerceptual = 32,
			// Token: 0x0400000D RID: 13
			ColourMetricUniform = 64,
			// Token: 0x0400000E RID: 14
			WeightColourByAlpha = 128,
			// Token: 0x0400000F RID: 15
			ColourIterativeClusterFit = 256
		}
	}
}
