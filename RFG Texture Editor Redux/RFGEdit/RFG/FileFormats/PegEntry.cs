using System;
using System.Collections.Generic;
using System.Drawing;

namespace RFGEdit.RFG.FileFormats
{
	// Token: 0x0200000D RID: 13
	public class PegEntry
	{
		// Token: 0x04000022 RID: 34
		public string Name;

		// Token: 0x04000023 RID: 35
		public List<PegFrame> Frames = new List<PegFrame>();

		// Token: 0x04000024 RID: 36
		public List<Bitmap> FrameBitmaps = new List<Bitmap>();

		// Token: 0x04000025 RID: 37
		public byte[] data;

        public bool Edited = false;
    }
}
