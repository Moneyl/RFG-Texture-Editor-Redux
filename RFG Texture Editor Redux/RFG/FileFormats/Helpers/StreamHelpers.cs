using System;
using System.IO;
using System.Text;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x02000004 RID: 4
	public static class StreamHelpers
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020DB File Offset: 0x000002DB
		public static bool ReadBoolean(this Stream stream)
		{
			return stream.ReadU8() > 0;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		public static void WriteBoolean(this Stream stream, bool value)
		{
			stream.WriteU8((byte)(value ? 1 : 0)); //Changed this to fix compilation
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F9 File Offset: 0x000002F9
		public static byte ReadU8(this Stream stream)
		{
			return (byte)stream.ReadByte();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002102 File Offset: 0x00000302
		public static void WriteU8(this Stream stream, byte value)
		{
			stream.WriteByte(value);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000210B File Offset: 0x0000030B
		public static char ReadS8(this Stream stream)
		{
			return (char)stream.ReadByte();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002114 File Offset: 0x00000314
		public static void WriteS8(this Stream stream, char value)
		{
			stream.WriteByte((byte)value);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002120 File Offset: 0x00000320
		public static short ReadS16(this Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			return BitConverter.ToInt16(array, 0);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002148 File Offset: 0x00000348
		public static void WriteS16(this Stream stream, short value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002168 File Offset: 0x00000368
		public static short ReadS16BE(this Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			return BitConverter.ToInt16(array, 0).Swap();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002194 File Offset: 0x00000394
		public static void WriteS16BE(this Stream stream, short value)
		{
			byte[] bytes = BitConverter.GetBytes(value.Swap());
			stream.Write(bytes, 0, 2);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021B8 File Offset: 0x000003B8
		public static ushort ReadU16(this Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			return BitConverter.ToUInt16(array, 0);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E0 File Offset: 0x000003E0
		public static void WriteU16(this Stream stream, ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 2);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002200 File Offset: 0x00000400
		public static ushort ReadU16BE(this Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			return BitConverter.ToUInt16(array, 0).Swap();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
		public static void WriteU16BE(this Stream stream, ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value.Swap());
			stream.Write(bytes, 0, 2);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002250 File Offset: 0x00000450
		public static uint ReadU24BE(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 1, 3);
			return BitConverter.ToUInt32(array, 0).Swap();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000227C File Offset: 0x0000047C
		public static void WriteU24BE(this Stream stream, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value.Swap());
			stream.Write(bytes, 1, 3);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022A0 File Offset: 0x000004A0
		public static int ReadS32(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022C8 File Offset: 0x000004C8
		public static void WriteS32(this Stream stream, int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022E8 File Offset: 0x000004E8
		public static int ReadS32BE(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToInt32(array, 0).Swap();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002314 File Offset: 0x00000514
		public static void WriteS32BE(this Stream stream, int value)
		{
			byte[] bytes = BitConverter.GetBytes(value.Swap());
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002338 File Offset: 0x00000538
		public static uint ReadU32(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002360 File Offset: 0x00000560
		public static void WriteU32(this Stream stream, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002380 File Offset: 0x00000580
		public static uint ReadU32BE(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToUInt32(array, 0).Swap();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023AC File Offset: 0x000005AC
		public static void WriteU32BE(this Stream stream, uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value.Swap());
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023D0 File Offset: 0x000005D0
		public static long ReadS64(this Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023F8 File Offset: 0x000005F8
		public static long ReadS64BE(this Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			return BitConverter.ToInt64(array, 0).Swap();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002424 File Offset: 0x00000624
		public static ulong ReadU64(this Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			return BitConverter.ToUInt64(array, 0);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000244C File Offset: 0x0000064C
		public static ulong ReadU64BE(this Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			return BitConverter.ToUInt64(array, 0).Swap();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002478 File Offset: 0x00000678
		public static float ReadF32(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			return BitConverter.ToSingle(array, 0);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024A0 File Offset: 0x000006A0
		public static void WriteF32(this Stream stream, float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024C0 File Offset: 0x000006C0
		public static float ReadF32BE(this Stream stream)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			uint value = BitConverter.ToUInt32(array, 0).Swap();
			array = BitConverter.GetBytes(value);
			return BitConverter.ToSingle(array, 0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024FC File Offset: 0x000006FC
		public static void WriteF32BE(this Stream stream, float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			uint value2 = BitConverter.ToUInt32(bytes, 0).Swap();
			bytes = BitConverter.GetBytes(value2);
			stream.Write(bytes, 0, 4);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002530 File Offset: 0x00000730
		public static double ReadF64(this Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			return BitConverter.ToDouble(array, 0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002558 File Offset: 0x00000758
		public static void WriteF64(this Stream stream, double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			stream.Write(bytes, 0, 8);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002575 File Offset: 0x00000775
		public static double ReadF64BE(this Stream stream)
		{
			return BitConverter.Int64BitsToDouble((long)stream.ReadU64BE());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002584 File Offset: 0x00000784
		public static void WriteF64BE(this Stream stream, double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			ulong value2 = BitConverter.ToUInt64(bytes, 0).Swap();
			bytes = BitConverter.GetBytes(value2);
			stream.Write(bytes, 0, 8);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025B8 File Offset: 0x000007B8
		public static string ReadASCII(this Stream stream, uint size)
		{
			byte[] array = new byte[size];
			stream.Read(array, 0, array.Length);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E4 File Offset: 0x000007E4
		public static string ReadASCIIZ(this Stream stream)
		{
			int num = 0;
			byte[] array = new byte[64];
			for (;;)
			{
				stream.Read(array, num, 1);
				if (array[num] == 0)
				{
					goto IL_42;
				}
				if (num >= array.Length)
				{
					if (array.Length >= 4096)
					{
						break;
					}
					Array.Resize<byte>(ref array, array.Length + 64);
				}
				num++;
			}
			throw new InvalidOperationException();
			IL_42:
			if (num == 0)
			{
				return "";
			}
			return Encoding.ASCII.GetString(array, 0, num);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000264C File Offset: 0x0000084C
		public static void WriteASCII(this Stream stream, string value)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(value);
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002670 File Offset: 0x00000870
		public static void WriteASCIIZ(this Stream stream, string value)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(value);
			stream.Write(bytes, 0, bytes.Length);
			stream.WriteByte(0);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000269C File Offset: 0x0000089C
		public static int ReadAligned(this Stream stream, byte[] buffer, int offset, int size, int align)
		{
			if (size == 0)
			{
				return 0;
			}
			int result = stream.Read(buffer, offset, size);
			int num = size % align;
			if (num > 0)
			{
				stream.Seek((long)(align - num), SeekOrigin.Current);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026D0 File Offset: 0x000008D0
		public static void WriteAligned(this Stream stream, byte[] buffer, int offset, int size, int align)
		{
			if (size == 0)
			{
				return;
			}
			stream.Write(buffer, offset, size);
			int num = size % align;
			if (num > 0)
			{
				byte[] buffer2 = new byte[align - num];
				stream.Write(buffer2, 0, align - num);
			}
		}
	}
}
