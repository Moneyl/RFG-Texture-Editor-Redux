using System;
using System.Runtime.InteropServices;

namespace RFGEdit.RFG.FileFormats
{
    public struct PegFrame
	{
        public uint Offset;
        public ushort Width;
        public ushort Height;
        public uint Format; //bitmap_format
        public ushort Unknown0C; //pixel_format
        public ushort Unknown0E; //anim_tiles_width
        public ushort Frames; //anim_tiles_height

		// Token: 0x0400002D RID: 45
		[FieldOffset(18)]
		public byte Unknown12;

		// Token: 0x0400002E RID: 46
		[FieldOffset(19)]
		public byte Unknown13;

		// Token: 0x0400002F RID: 47
		[FieldOffset(20)]
		public uint Pointer;

		// Token: 0x04000030 RID: 48
		[FieldOffset(24)]
		public ushort Unknown18;

		// Token: 0x04000031 RID: 49
		[FieldOffset(26)]
		public ushort Unknown1A;

		// Token: 0x04000032 RID: 50
		[FieldOffset(28)]
		public uint Size;

		// Token: 0x04000033 RID: 51
		[FieldOffset(32)]
		public uint Unknown20;

		// Token: 0x04000034 RID: 52
		[FieldOffset(36)]
		public uint Unknown24;

		// Token: 0x04000035 RID: 53
		[FieldOffset(40)]
		public uint Unknown28;

		// Token: 0x04000036 RID: 54
		[FieldOffset(44)]
		public uint Unknown2C;
	}
}
