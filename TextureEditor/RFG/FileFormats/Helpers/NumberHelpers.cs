using System;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x0200000B RID: 11
	public static class NumberHelpers
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002C14 File Offset: 0x00000E14
		public static short Swap(this short value)
		{
			ushort num = (ushort)((255 & value >> 8) | (65280 & (int)value << 8));
			return (short)num;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C38 File Offset: 0x00000E38
		public static ushort Swap(this ushort value)
		{
			return (ushort)((255 & value >> 8) | (65280 & (int)value << 8));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static int Swap(this int value)
		{
			return (int)((255u & (uint)value >> 24) | (65280u & (uint)value >> 8) | (uint)(16711680 & value << 8) | (uint)(-16777216 & value << 24));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C98 File Offset: 0x00000E98
		public static uint Swap(this uint value)
		{
			return (255u & value >> 24) | (65280u & value >> 8) | (16711680u & value << 8) | (4278190080u & value << 24);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static long Swap(this long value)
		{
            unchecked //Just doing this for now to avoid compilation errors. Will see if more accurate version of this is somewhere since this is a mess.
            {
                return (long)((255UL & (ulong)value >> 56) | (65280UL & (ulong)value >> 40) | (16711680UL & (ulong)value >> 24) | ((ulong)-16777216 & (ulong)value >> 8) | (ulong)(1095216660480L & value << 8) | (ulong)(280375465082880L & value << 24) | (ulong)(71776119061217280L & value << 40) | (ulong)(-72057594037927936L & value << 56));
            }
        }

		// Token: 0x0600004F RID: 79 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static ulong Swap(this ulong value)
		{
            unchecked //Just doing this for now to avoid compilation errors. Will see if more accurate version of this is somewhere since this is a mess.
            {
                return (255UL & value >> 56) | (65280UL & value >> 40) | (16711680UL & value >> 24) | ((ulong)-16777216 & value >> 8) | (1095216660480UL & value << 8) | (280375465082880UL & value << 24) | (71776119061217280UL & value << 40) | (18374686479671623680UL & value << 56);
            }
        }

		// Token: 0x06000050 RID: 80 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static int Align(this int value, int align)
		{
			if (value == 0)
			{
				return value;
			}
			int num = value % align;
			if (num > 0)
			{
				return value + (align - num);
			}
			return value;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public static uint Align(this uint value, uint align)
		{
			if (value == 0u)
			{
				return value;
			}
			uint num = value % align;
			if (num > 0u)
			{
				return value + (align - num);
			}
			return value;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E0C File Offset: 0x0000100C
		public static long Align(this long value, long align)
		{
			if (value == 0L)
			{
				return value;
			}
			long num = value % align;
			if (num > 0L)
			{
				return value + (align - num);
			}
			return value;
		}
	}
}
