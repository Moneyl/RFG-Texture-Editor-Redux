using System;
using System.Globalization;
using System.Text;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x02000002 RID: 2
	public static class StringHelpers
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static uint CRC32(this string input)
		{
			return BitConverter.ToUInt32(new CRC32().ComputeHash(Encoding.ASCII.GetBytes(input)), 0);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		public static uint KeyCRC32(this string input)
		{
			return BitConverter.ToUInt32(new BrokenCRC32().ComputeHash(Encoding.ASCII.GetBytes(input.ToLower())), 0);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208F File Offset: 0x0000028F
		public static uint GetHexNumber(this string input)
		{
			if (input.StartsWith("0x"))
			{
				return uint.Parse(input.Substring(2), NumberStyles.AllowHexSpecifier);
			}
			return uint.Parse(input);
		}
	}
}
