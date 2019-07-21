using System;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x0200000A RID: 10
	public class BrokenCRC32 : CRC32
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002BFD File Offset: 0x00000DFD
		public override void Initialize()
		{
			AllOnes = 0u;
			base.Initialize();
		}
	}
}
