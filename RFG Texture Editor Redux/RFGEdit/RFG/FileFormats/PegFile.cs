using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
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
				pegEntry.FrameBitmaps.Add(FrameToBitmap(pegFrame, pegEntry));
				num3++;
				if (pegFrame.NumFrames == 0)
				{
					throw new Exception("Frame count is 0");
				}
				if (pegFrame.NumFrames > 1)
				{
					for (int k = 1; k < pegFrame.NumFrames; k++)
					{
						PegFrame pegFrame2 = ReadFrame(header);
						pegEntry.Frames.Add(pegFrame2);
						pegEntry.FrameBitmaps.Add(FrameToBitmap(pegFrame2, pegEntry));
						num3++;
					}
				}
				Entries.Add(pegEntry);
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
			Stream stream = File.Open(headerFile, FileMode.Create, FileAccess.Write); //writes to cpu file (cpeg_pc or cvbm_pc)
			Stream stream2 = File.Open(dataFile, FileMode.Create, FileAccess.Write); //writes to gpu file (gpeg_pc or gvbm_pc)

            //Write header data to cpu file
			stream.WriteU32(1447773511); //magic sig
			stream.WriteU16(10); //version
            stream.WriteU16(0); //platform
			stream.WriteU32(0); //header size, initially write 0, will come back and write real value afterwards
			stream.WriteU32(0); //gpu data size, same method as previous ^^
			stream.WriteU16((ushort)Entries.Count); //number_of_bitmaps
			stream.WriteU16(0); //flags
			stream.WriteU16((ushort)Entries.Count); //total_entries (same as number_of_bitmaps as far as I can tell)
			stream.WriteU16(16); //Align value, was zero in original code, pretty sure it needs to be 16 based on all files I've viewed

            //Write peg entries to cpu and gpu files
			foreach (PegEntry pegEntry in Entries)
			{
                //Quick hack to avoid corrupting unchanged DXT compressed texture data and to compress imported data to DXT5.
                //Really need to refactor the entire project and make it cleaner and more understandable.
                if (pegEntry.Edited) 
                {
                    SwitchRedAndBlueChannels(pegEntry.data); //BGRA -> RGBA (no idea how it's BGRA here, need to refactor the shit out of this code).

                    int length = pegEntry.data.Length;
                    //Todo: Print array to text file at different steps and make sure everything is working properly. Also try disabling red/blue switch
                    //Todo: Issue is probably from the code that adds another color channel. Tried disabling red/blue switch and compression, no change detected.

                    //MessageBox.Show($"Final pixel: R: {pegEntry.data[length - 4]}, G: {pegEntry.data[length - 3]}, B: {pegEntry.data[length - 2]}, A: {pegEntry.data[length - 1]}", "Final pixel values");

                    // Convert RGBA data to DXT5 (compressed) to avoid hitting the games texture file size limits.
                    pegEntry.data = Squish.Compress(pegEntry.data, pegEntry.Frames[0].Width, pegEntry.Frames[0].Height, (int)Squish.Flags.DXT5);

                    var entry = pegEntry.Frames[0];
                    entry.Format = (ushort)PegFormat.DXT5;
                    entry.Size = (uint)pegEntry.data.Length;
                    pegEntry.Frames[0] = entry;
                }

                WriteFrame(stream, pegEntry.Frames[0], (uint)stream2.Length, (uint)(pegEntry.FrameBitmaps[0].Width * pegEntry.FrameBitmaps[0].Height * 4));

                //BGRA -> ARGB //No idea how it ends up being BGRA here, need to go through the code and figure out this nonsense.

                //ConvertBgraToArgb(pegEntry.data);
                //BGRA -> RGBA



                stream2.Write(pegEntry.data, 0, pegEntry.data.Length);
			}

            //Write peg names to cpu file
			foreach (PegEntry pegEntry2 in Entries)
			{
				stream.WriteASCIIZ(pegEntry2.Name);
			}
            
            //Seek to header_size and write it's value
			stream.Seek(8L, SeekOrigin.Begin);
			stream.WriteU32((uint)stream.Length);
            stream.WriteU32((uint)stream2.Length); //write value of gpu_data_size

            stream.Close();
            stream2.Close();
        }

        void SwitchRedAndBlueChannels(byte[] data)
        {
            var alphaChannel = new byte[data.Length / 4];
            var redChannel = new byte[data.Length / 4];
            var greenChannel = new byte[data.Length / 4];
            var blueChannel = new byte[data.Length / 4];

            int pixelIndex = 0;
            for (int i = 0; i < data.Length - 3; i += 4)
            {
                blueChannel[pixelIndex] = data[i];
                greenChannel[pixelIndex] = data[i + 1];
                redChannel[pixelIndex] = data[i + 2];
                alphaChannel[pixelIndex] = data[i + 3];
                pixelIndex++;
            }

            pixelIndex = 0;
            for (int i = 0; i < data.Length - 3; i += 4)
            {
                data[i] = redChannel[pixelIndex];
                data[i + 1] = greenChannel[pixelIndex];
                data[i + 2] = blueChannel[pixelIndex];
                data[i + 3] = alphaChannel[pixelIndex];
                pixelIndex++;
            }
        }

        void ConvertBgraToArgb(byte[] data)
        {
            var alphaChannel = new byte[data.Length / 4];
            var redChannel = new byte[data.Length / 4];
            var greenChannel = new byte[data.Length / 4];
            var blueChannel = new byte[data.Length / 4];

            int pixelIndex = 0;
            for (int i = 0; i < data.Length - 3; i += 4) //Todo: Make sure data.Length - 3 is correct
            {
                blueChannel[pixelIndex] = data[i];
                greenChannel[pixelIndex] = data[i + 1];
                redChannel[pixelIndex] = data[i + 2];
                alphaChannel[pixelIndex] = data[i + 3];
                pixelIndex++;
            }

            pixelIndex = 0;
            for (int i = 0; i < data.Length - 3; i += 4)
            {
                data[i] = alphaChannel[pixelIndex];
                data[i + 1] = redChannel[pixelIndex];
                data[i + 2] = greenChannel[pixelIndex];
                data[i + 3] = blueChannel[pixelIndex];
                pixelIndex++;
            }
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00003374 File Offset: 0x00001574
        private void WriteFrameToStream(Bitmap frame, Stream stream)
		{
			byte[] array = MakeByteArrayFromBitmap(frame);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003394 File Offset: 0x00001594
		private Bitmap FrameToBitmap(PegFrame frame, PegEntry entry)
		{
			_data.Seek((long)((ulong)frame.Data), SeekOrigin.Begin);
			byte[] array = new byte[frame.Size];
			_data.Read(array, 0, (int)frame.Size);
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
				result = MakeBitmapFromDXT((uint)frame.Width, (uint)frame.Height, array2, true);
			}
			else if (format == PegFormat.R5G6B5)
			{
				result = MakeBitmapFromR5G6B5((uint)frame.Width, (uint)frame.Height, array);
			}
			else
			{
				if (format != PegFormat.A8R8G8B8)
				{
					throw new Exception("unhandled format " + frame.Format.ToString());
				}
				result = MakeBitmapFromA8R8G8B8((uint)frame.Width, (uint)frame.Height, array);
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
			byte[] buffer = MakeByteArrayFromBitmap(bitmap);
			return MakeBitmapFromA8R8G8B8((uint)bitmap.Width, (uint)bitmap.Height, buffer);
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
            if (bitmap.PixelFormat == PixelFormat.Format24bppRgb) //really stored as bgr
            {
                //Currently not going to support this format due to issues solving a bug with it. Will add support for it and hopefully other formats later on.
                throw new Exception("Error! Unsupported bitmap format 24bppRgb detected! This format is currently not supported. Please be sure to save your edited textures as pngs with a bit depth of 32 and ensure that they have an alpha channel.");
                //Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                //BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                //byte[] array = new byte[bitmap.Width * bitmap.Height * 3]; //* 3 for rgb

                //for (int i = 0; i < array.Length; i++)
                //{
                //    array[i] = Marshal.ReadByte(bitmapData.Scan0, i);
                //}
                //bitmap.UnlockBits(bitmapData);

                ////Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                ////BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);

                ////IntPtr ptr = bmpData.Scan0;

                ////int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
                ////byte[] array = new byte[bytes];

                ////Marshal.Copy(ptr, array, 0, bytes);

                ////Is rgb image, add alpha channel 
                //var redChannel = new byte[array.Length / 3];
                //var greenChannel = new byte[array.Length / 3];
                //var blueChannel = new byte[array.Length / 3];

                //int pixelIndex = 0;
                //for (int i = 0; i < array.Length - 2; i += 3) //+= 3
                //{
                //    blueChannel[pixelIndex] = array[i];
                //    greenChannel[pixelIndex] = array[i + 1];
                //    redChannel[pixelIndex] = array[i + 2];
                //    pixelIndex++;
                //}

                //using (var File = new StreamWriter(@"C:\Users\moneyl\Desktop\RFGR Texture editor test\rgb-pre-alpha-add-color-channels.txt"))
                //{
                //    for (int i = 0; i < blueChannel.Length; i++)
                //    {
                //        File.WriteLine("\n\nPixel Index: {0}\n\tBlue: {1}, Green: {2}, Red: {3}", i, blueChannel[i], greenChannel[i], redChannel[i]);
                //    }
                //}

                //pixelIndex = 0;
                //array = new byte[bitmap.Width * bitmap.Height * 4];
                //for (int i = 0; i < array.Length - 3; i += 4)
                //{
                //    array[i] = blueChannel[pixelIndex];
                //    array[i + 1] = greenChannel[pixelIndex];
                //    array[i + 2] = redChannel[pixelIndex];
                //    array[i + 3] = 255; //Full opacity for alpha channel
                //    pixelIndex++;
                //}

                //pixelIndex = 0;
                //using (var File = new StreamWriter(@"C:\Users\moneyl\Desktop\RFGR Texture editor test\rgba-post-alpha-add-color-channels.txt"))
                //{
                //    for (int i = 0; i < array.Length - 3; i += 4)
                //    {
                //        File.WriteLine("\n\nPixel Index: {0}\n\tV0: {1}, V1: {2}, V2: {3}, V3: {4}", pixelIndex, array[i], array[i + 1], array[i + 2], array[i + 3]);
                //        pixelIndex++;
                //    }
                //}

                //return array;
            }
            else if (bitmap.PixelFormat == PixelFormat.Format32bppArgb) //really stored as bgra
            {
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                byte[] array = new byte[bitmap.Width * bitmap.Height * 4]; //* 3 for rgb

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = Marshal.ReadByte(bitmapData.Scan0, i);
                }
                bitmap.UnlockBits(bitmapData);
                return array;
            }
            else
            {
                //Doesn't equal rgb or argb so complain about unknown or unsupported format
                throw new Exception("Unknown or unsupported pixel format detected when importing texture! PixelFormat: " + bitmap.PixelFormat.ToString());
            }
        }
    }
}
