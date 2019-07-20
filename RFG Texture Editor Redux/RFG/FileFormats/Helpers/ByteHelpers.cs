using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x02000012 RID: 18
	public static class ByteHelpers
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public static object BytesToStructure(this byte[] data, Type type)
		{
			if (data.Length != Marshal.SizeOf(type))
			{
				throw new Exception("structure size is not the same as the data size");
			}
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			object result = Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), type);
			gchandle.Free();
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004B14 File Offset: 0x00002D14
		public static string GetASCIIZ(this byte[] data, int offset)
		{
			int num = offset;
			while (num < data.Length && data[num] != 0)
			{
				num++;
			}
			if (num == offset)
			{
				return "";
			}
			return Encoding.ASCII.GetString(data, offset, num - offset);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004B4D File Offset: 0x00002D4D
		public static string GetASCIIZ(this byte[] data, uint offset)
		{
			return data.GetASCIIZ((int)offset);
		}
	}
}
