using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using RFG.FileFormats.Helpers;

// Full peg file def is here: https://github.com/Moneyl/RFGR-Modding-Tools/blob/master/File%20formats/File%20definitions/cpeg_pc/cpeg_pc.ksy

namespace RFGEdit.RFG.FileFormats
{
	// Token: 0x02000010 RID: 16
	public class PegFile
	{
        // Token: 0x04000039 RID: 57
        public List<PegEntry> Entries = new List<PegEntry>();

        // Token: 0x0400003A RID: 58
        public uint FileSize;

        // Token: 0x0400003B RID: 59
        public uint Unknown0C;

        // Token: 0x0400003C RID: 60
        public ushort Unknown12;

        // Token: 0x0400003D RID: 61
        public uint Unknown16;

        // Token: 0x0400003E RID: 62
        private Stream _data;

        // Token: 0x0400003F RID: 63
        private Stream _header;

        // Token: 0x0600005B RID: 91 RVA: 0x00002F20 File Offset: 0x00001120
        public PegFrame ReadFrame(Stream stream)
        {
            var frame = new PegFrame
            {
                Data = stream.ReadU32(),
                Width = stream.ReadU16(),
                Height = stream.ReadU16(),
                Format = stream.ReadU16(),
                SourceWidth = stream.ReadU16(),
                AnimTilesWidth = stream.ReadU16(),
                AnimTilesHeight = stream.ReadU16(),
                NumFrames = stream.ReadU16(),
                Flags = stream.ReadU16(),
                Filename = stream.ReadU32(),
                SourceHeight = stream.ReadU16(),
                Fps = stream.ReadU8(),
                MipLevels = stream.ReadU8(),
                Size = stream.ReadU32(),
                Next = stream.ReadU32(),
                Previous = stream.ReadU32(),
                Cache1 = stream.ReadU32(),
                Cache2 = stream.ReadU32()
            };
            return frame;
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00002F58 File Offset: 0x00001158
        public void Read(Stream headerStream, Stream dataStream)
		{
			_header = headerStream;
			_data = dataStream;
			Stream header = _header;
			Entries.Clear();

			if (header.ReadU32() != 1447773511) //Magic sig
			{
				throw new Exception("Header signature must be GEKV");
			}
			if (header.ReadU16() != 10) //version
			{
				throw new Exception("Only version 10 is supported");
			}
            header.ReadU16(); //Platform

			FileSize = header.ReadU32(); //directory_block_size
			if (header.Length != (long)FileSize)
			{
				throw new Exception("The size of file does not match size in header");
			}

			Unknown0C = header.ReadU32(); //data_block_size
			int num = (int)header.ReadU16(); //number_of_bitmaps
			Unknown12 = header.ReadU16(); //flags
			int num2 = (int)header.ReadU16(); //total_entries (not sure why they have this and number_of_bitmaps, have same value)
			Unknown16 = (uint)header.ReadU16(); //align_value (always 16 from what I've seen)
			string[] array = new string[num];
			header.Seek((long)(24 + 48 * num2), SeekOrigin.Begin); //Can probably clean this up instead of doing 24 + 48 * total_entries
			for (int i = 0; i < num; i++) //Get each texture filename
			{
				array[i] = header.ReadASCIIZ(); 
			}
			int num3 = 0;
			header.Seek(24L, SeekOrigin.Begin); //Seek to start of peg entries, 24 bytes in.
			for (int j = 0; j < num; j++)
			{
				PegEntry pegEntry = new PegEntry();
				pegEntry.Name = array[j];
				PegFrame pegFrame = ReadFrame(header);
				pegEntry.Frames.Add(pegFrame);
				pegEntry.FrameBitmaps.Add(this.FrameToBitmap(pegFrame, pegEntry));
				num3++;
				if (pegFrame.NumFrames == 0)
				{
					throw new Exception("Frame count is 0");
				}
				if (pegFrame.NumFrames > 1)
				{
					for (int k = 1; k < pegFrame.NumFrames; k++)
					{
						PegFrame pegFrame2 = this.ReadFrame(header);
						pegEntry.Frames.Add(pegFrame2);
						pegEntry.FrameBitmaps.Add(this.FrameToBitmap(pegFrame2, pegEntry));
						num3++;
					}
				}
				this.Entries.Add(pegEntry);
			}
			if (num3 != num2)
			{
				throw new Exception("something bad happened");
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003128 File Offset: 0x00001328
		public void WriteFrame(Stream stream, PegFrame frame, uint offset, uint size)
		{
			stream.WriteU32(offset);
			stream.WriteU16(frame.Width);
			stream.WriteU16(frame.Height);
			stream.WriteU16(frame.Format);
            stream.WriteU16(frame.SourceWidth);
            stream.WriteU16(frame.AnimTilesWidth);
            stream.WriteU16(frame.AnimTilesHeight);
            stream.WriteU16(frame.NumFrames);
            stream.WriteU16(frame.Flags);
            stream.WriteU32(frame.Filename);
            stream.WriteU16(frame.SourceHeight);
            stream.WriteU8(frame.Fps);
            stream.WriteU8(frame.MipLevels);
            stream.WriteU32(frame.Size);
            stream.WriteU32(frame.Next); //Should probably init these to zero so junk doesn't get output here. (if that's even a thing)
            stream.WriteU32(frame.Previous);
            stream.WriteU32(frame.Cache1);
            stream.WriteU32(frame.Cache2);
        }

		// Token: 0x0600005E RID: 94 RVA: 0x00003208 File Offset: 0x00001408
        //Todo: Fix this so it follows the correct formatting.
		public void Write(string headerFile, string dataFile)
		{
			Stream stream = File.Open(headerFile, FileMode.Create, FileAccess.Write);
			Stream stream2 = File.Open(dataFile, FileMode.Create, FileAccess.Write);
			stream.WriteU32(1447773511u);
			stream.WriteU32(10u);
			stream.WriteU32(0u);
			stream.WriteU32(0u);
			stream.WriteU16((ushort)this.Entries.Count);
			stream.WriteU16(0);
			stream.WriteU16((ushort)this.Entries.Count);
			stream.WriteU16(0);
			foreach (PegEntry pegEntry in this.Entries)
			{
				this.WriteFrame(stream, pegEntry.Frames[0], (uint)stream2.Length, (uint)(pegEntry.FrameBitmaps[0].Width * pegEntry.FrameBitmaps[0].Height * 4));
				stream2.Write(pegEntry.data, 0, pegEntry.data.Length);
			}
			foreach (PegEntry pegEntry2 in this.Entries)
			{
				stream.WriteASCIIZ(pegEntry2.Name);
			}
			stream.Seek(8L, SeekOrigin.Begin);
			stream.WriteU32((uint)stream.Length);
			stream.Close();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003374 File Offset: 0x00001574
		private void WriteFrameToStream(Bitmap frame, Stream stream)
		{
			byte[] array = PegFile.MakeByteArrayFromBitmap(frame);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003394 File Offset: 0x00001594
		private Bitmap FrameToBitmap(PegFrame frame, PegEntry entry)
		{
			this._data.Seek((long)((ulong)frame.Data), SeekOrigin.Begin);
			byte[] array = new byte[frame.Size];
			this._data.Read(array, 0, (int)frame.Size);
			entry.data = array;
			PegFormat format = (PegFormat)frame.Format;
			Bitmap result;
			if (format == PegFormat.DXT1 || format == PegFormat.DXT3 || format == PegFormat.DXT5)
			{
				Squish.Flags flags = (Squish.Flags)0;
				if (format == PegFormat.DXT1)
				{
					flags |= Squish.Flags.DXT1;
				}
				else if (format == PegFormat.DXT3)
				{
					flags |= Squish.Flags.DXT3;
				}
				else if (format == PegFormat.DXT5)
				{
					flags |= Squish.Flags.DXT5;
				}
				byte[] array2 = new byte[(int)(frame.Width * frame.Height * 4)];
				Squish.Decompress(array2, (uint)frame.Width, (uint)frame.Height, array, (int)flags);
				result = PegFile.MakeBitmapFromDXT((uint)frame.Width, (uint)frame.Height, array2, true);
			}
			else if (format == PegFormat.R5G6B5)
			{
				result = PegFile.MakeBitmapFromR5G6B5((uint)frame.Width, (uint)frame.Height, array);
			}
			else
			{
				if (format != PegFormat.A8R8G8B8)
				{
					throw new Exception("unhandled format " + frame.Format.ToString());
				}
				result = PegFile.MakeBitmapFromA8R8G8B8((uint)frame.Width, (uint)frame.Height, array);
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034D0 File Offset: 0x000016D0
		private static Bitmap MakeBitmapFromDXT(uint width, uint height, byte[] buffer, bool keepAlpha)
		{
			Bitmap bitmap = new Bitmap((int)width, (int)height, PixelFormat.Format32bppArgb);
			for (uint num = 0u; num < width * height * 4u; num += 4u)
			{
				byte b = buffer[(int)((UIntPtr)num)];
				buffer[(int)((UIntPtr)num)] = buffer[(int)((UIntPtr)(num + 2u))];
				buffer[(int)((UIntPtr)(num + 2u))] = b;
			}
			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
			Marshal.Copy(buffer, 0, bitmapData.Scan0, (int)(width * height * 4u));
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003554 File Offset: 0x00001754
		private static Bitmap MakeBitmapFromR5G6B5(uint width, uint height, byte[] buffer)
		{
			Bitmap bitmap = new Bitmap((int)width, (int)height, PixelFormat.Format16bppRgb565);
			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
			Marshal.Copy(buffer, 0, bitmapData.Scan0, (int)(width * height * 2u));
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000035AC File Offset: 0x000017AC
		public static Bitmap Test()
		{
			Bitmap bitmap = new Bitmap("C:\\Users\\Luke\\Desktop\\GPEg\\map_mini_overlay.png");
			byte[] buffer = PegFile.MakeByteArrayFromBitmap(bitmap);
			return PegFile.MakeBitmapFromA8R8G8B8((uint)bitmap.Width, (uint)bitmap.Height, buffer);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000035E0 File Offset: 0x000017E0
		public static Bitmap MakeBitmapFromA8R8G8B8(uint width, uint height, byte[] buffer)
		{
			Bitmap bitmap = new Bitmap((int)width, (int)height, PixelFormat.Format32bppArgb);
			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
			Marshal.Copy(buffer, 0, bitmapData.Scan0, (int)(width * height * 4u));
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003638 File Offset: 0x00001838
		public static byte[] MakeByteArrayFromBitmap(Bitmap bitmap)
		{
			Bitmap image = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(image);
			graphics.DrawImageUnscaled(bitmap, 0, 0);
			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
			byte[] array = new byte[bitmap.Width * bitmap.Height * 4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Marshal.ReadByte(bitmapData.Scan0, i);
			}
			bitmap.UnlockBits(bitmapData);
			return array;
		}
    }
}
