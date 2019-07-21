using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

namespace RFG.FileFormats.Helpers
{
	// Token: 0x02000009 RID: 9
	public class CRC32 : HashAlgorithm
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A11 File Offset: 0x00000C11
		public static uint DefaultPolynomial
		{
			get
			{
				return 3988292384u;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A18 File Offset: 0x00000C18
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A1F File Offset: 0x00000C1F
		public static bool AutoCache
		{
			get
			{
				return _AutoCache;
			}
			set
			{
				_AutoCache = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A27 File Offset: 0x00000C27
		public static uint DefaultAllOnes
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A41 File Offset: 0x00000C41
		public static void ClearCache()
		{
			CachedCRC32Tables.Clear();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A50 File Offset: 0x00000C50
		protected static uint[] BuildCRC32Table(uint polynomial)
		{
			uint[] array = new uint[256];
			for (uint num = 0u; num < 256u; num += 1u)
			{
				uint num2 = num;
				for (int i = 8; i > 0; i--)
				{
					if ((num2 & 1u) == 1u)
					{
						num2 = (num2 >> 1 ^ polynomial);
					}
					else
					{
						num2 >>= 1;
					}
				}
				array[(int)((UIntPtr)num)] = num2;
			}
			return array;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A9E File Offset: 0x00000C9E
		public CRC32() : this(DefaultPolynomial)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AAB File Offset: 0x00000CAB
		public CRC32(uint polynomial) : this(polynomial, AutoCache)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002ABC File Offset: 0x00000CBC
		public CRC32(uint polymonial, bool cacheTable)
		{
			AllOnes = DefaultAllOnes;
			HashSizeValue = 32;
			CRC32Table = (uint[])CachedCRC32Tables[polymonial];
			if (CRC32Table == null)
			{
				CRC32Table = BuildCRC32Table(polymonial);
				if (cacheTable)
				{
					CachedCRC32Tables.Add(polymonial, CRC32Table);
				}
			}
			Initialize();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B30 File Offset: 0x00000D30
		public override void Initialize()
		{
			CRC = AllOnes;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B40 File Offset: 0x00000D40
		protected override void HashCore(byte[] buffer, int offset, int count)
		{
			for (int i = offset; i < count; i++)
			{
				ulong num = (ulong)((CRC & 255u) ^ (uint)buffer[i]);
				CRC >>= 8;
				CRC ^= CRC32Table[(int)(checked((IntPtr)num))];
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B8F File Offset: 0x00000D8F
		protected override byte[] HashFinal()
		{
			return BitConverter.GetBytes(CRC ^ AllOnes);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public new byte[] ComputeHash(Stream inputStream)
		{
			byte[] array = new byte[4096];
			int cbSize;
			while ((cbSize = inputStream.Read(array, 0, 4096)) > 0)
			{
				HashCore(array, 0, cbSize);
			}
			return HashFinal();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BDF File Offset: 0x00000DDF
		public new byte[] ComputeHash(byte[] buffer)
		{
			return ComputeHash(buffer, 0, buffer.Length);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BEC File Offset: 0x00000DEC
		public new byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			HashCore(buffer, offset, count);
			return HashFinal();
		}

		// Token: 0x04000010 RID: 16
		protected uint AllOnes;

		// Token: 0x04000011 RID: 17
		protected static Hashtable CachedCRC32Tables = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000012 RID: 18
		protected static bool _AutoCache = true;

		// Token: 0x04000013 RID: 19
		protected uint[] CRC32Table;

		// Token: 0x04000014 RID: 20
		private uint CRC;
	}
}
