using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TextureEditor.Utility;

//struct peg_entry::peg_flag_and_size_union
//{
//unsigned __int16 combined;
//};

//Keep note of this when doing the peg entry
//struct peg_entry
//{
//    et_ptr_offset<unsigned char,0> data;
//    unsigned __int16 width;
//    unsigned __int16 height;
//    unsigned __int16 bm_fmt;
//    peg_entry::peg_flag_and_size_union source_width;
//    unsigned __int16 anim_tiles_width;
//    unsigned __int16 anim_tiles_height;
//    unsigned __int16 num_frames;
//    unsigned __int16 flags;
//    et_ptr_offset<char,1> filename;
//    peg_entry::peg_flag_and_size_union source_height;
//    char fps;
//    char mip_levels;
//    unsigned int frame_size;
//    et_ptr_offset<peg_entry,1> next;
//    et_ptr_offset<peg_entry,1> prev;
//    unsigned int cache[2];
//};

namespace TextureEditor.Peg
{
    //Todo: Make use of fancy pants c# features like properties
    public class PegFile
    {
        //Todo: Rename these variables to be closer to their usage and purpose

        public uint Signature;
        public ushort Version;
        public ushort Platform; //Todo: Try to find an enum for this
        public uint DirectoryBlockSize; //Pretty sure this is the cpeg/cvbm file size, gotta check. Todo: Confirm this value is
        public uint DataBlockSize; //Pretty sure this is the gpeg size, gotta check. Todo: Confirm what this value is
        public ushort NumberOfBitmaps; //Todo: Figure out the difference between this and TotalEntries. Might be something to do with animated textures
        public ushort Flags; //Dunno what these do. Never seen them set to anything but 0
        public ushort TotalEntries; //Number of pegs in the file. 
        public ushort AlignValue; //Byte alignment of the (cpu? gpu? both?) peg file

        public List<PegEntry> Entries = new List<PegEntry>();

        private string _cpuFilePath;
        private string _gpuFilePath;

        public string cpuFileName;
        public string gpuFileName;

        public PegFile(string cpuFilePath, string gpuFilePath)
        {
            if (!File.Exists(cpuFilePath))
            {
                throw new Exception($"Could not locate cpu peg file at \"{cpuFilePath}\"");
            }
            if (!File.Exists(gpuFilePath))
            {
                throw new Exception($"Could not locate gpu peg file at \"{gpuFilePath}\"");
            }

            _cpuFilePath = cpuFilePath;
            _gpuFilePath = gpuFilePath;

            cpuFileName = Path.GetFileName(cpuFilePath);
            gpuFileName = Path.GetFileName(gpuFilePath);
        }

        //Todo: Consider adding warnings for unusual, problematic, or unsupported values. Maybe change some existing exceptions to warnings.
        public void Read()
        {
            using (var cpuFileStream = new FileStream(_cpuFilePath, FileMode.Open))
            {
                using (var gpuFileStream = new FileStream(_gpuFilePath, FileMode.Open))
                {
                    Entries.Clear();
                    var header = new BinaryReader(cpuFileStream);

                    Signature = header.ReadUInt32BE();
                    if (Signature != 1447773511 && Signature != 1195723606) //Equals GEKV as a string
                    {
                        throw new Exception("Header signature must be GEKV. Invalid peg file. Make sure that your packfile extractor didn't incorrectly extract the peg file you're trying to open.");
                    }
                    Version = header.ReadUInt16BE();
                    if (Version != 10)
                    {
                        throw new Exception($"Unsupported peg format version detected! Only peg version 10 is supported. Version {Version} was detected");
                    }

                    Platform = header.ReadUInt16BE(); //Todo: Add exception or warning for unknown or unsupported platform.
                    DirectoryBlockSize = header.ReadUInt32BE();
                    if (header.BaseStream.Length != DirectoryBlockSize)
                    {
                        throw new Exception($"Error, the size of the header file (cpeg_pc or cvbm_pc) does not match the size value stored in the header! Actual size: {header.BaseStream.Length} bytes, stored size: {DirectoryBlockSize} bytes.");
                    }

                    DataBlockSize = header.ReadUInt32BE();
                    NumberOfBitmaps = header.ReadUInt16BE();
                    Flags = header.ReadUInt16BE();
                    TotalEntries = header.ReadUInt16BE();
                    AlignValue = header.ReadUInt16BE();

                    //Read peg entries
                    for (int i = 0; i < NumberOfBitmaps; i++)
                    {
                        var entry = new PegEntry();
                        entry.Read(header);
                        Entries.Add(entry);
                    }

                    //Read peg entry names
                    foreach (var entry in Entries)
                    {
                        entry.Name = Util.ReadNullTerminatedString(header);
                    }
                }
            }
        }

        public Bitmap GetEntryBitmap(PegEntry entry)
        {
            return GetEntryBitmap(Entries.IndexOf(entry));
        }

        public Bitmap GetEntryBitmap(int entryIndex)
        {
            if (entryIndex >= Entries.Count)
                return null;

            var entry = Entries[entryIndex];
            if (entry.Edited)
            {
                return entry.Bitmap;
            }
            else
            {
                var gpuFileStream = new FileStream(_gpuFilePath, FileMode.Open);
                var rawData = new byte[entry.frame_size];

                gpuFileStream.Skip(entry.data);
                gpuFileStream.Read(rawData, 0, (int) entry.frame_size);
                //rawData = rawData.Reverse();
                var bitmap = Util.RawDataToBitmap(rawData, entry.bitmap_format, entry.width, entry.height);
                gpuFileStream.Dispose();

                return bitmap;
            }
        }

        public byte[] GetEntryRawData(PegEntry entry)
        {
            return GetEntryRawData(Entries.IndexOf(entry));
        }

        public byte[] GetEntryRawData(int entryIndex)
        {
            if (entryIndex >= Entries.Count)
                return null;

            var entry = Entries[entryIndex];
            var gpuFileStream = new FileStream(_gpuFilePath, FileMode.Open);

            var rawData = new byte[entry.frame_size];
            gpuFileStream.Skip(entry.data);
            gpuFileStream.Read(rawData, 0, (int)entry.frame_size);
            gpuFileStream.Dispose();
            return rawData;
        }

        public void Write()
        {
            //Get raw data of entries ahead of time. This is a lame way to handle this imo but due to the design of GetEntryRawData necessary
            //to avoid multiple streams trying to read the gpu file at once. The goal of this is to keep memory usage low and never
            //cache big images for long, but the execution could be better.
            var rawDatas = new List<byte[]>();
            foreach (var entry in Entries)
            {
                rawDatas.Add(GetEntryRawData(entry));
            }

            using (var cpuFileStream = new FileStream(_cpuFilePath, FileMode.Truncate))
            {
                using (var gpuFileStream = new FileStream(_gpuFilePath, FileMode.Truncate))
                {
                    var cpuFile = new BinaryWriter(cpuFileStream);
                    var gpuFile = new BinaryWriter(gpuFileStream);

                    WriteEntryData(gpuFile, rawDatas); //Write entry data to gpu file first so that entry size can be counted.
                    WriteHeader(cpuFile);
                    WriteEntries(cpuFile);
                    WriteEntryNames(cpuFile);

                    cpuFile.Seek(8, SeekOrigin.Begin); //Seek to cpu_file and gpu_file size values in cpu_file.
                    cpuFile.Write((uint)cpuFile.BaseStream.Length); //Update cpu_file size variable
                    cpuFile.Write((uint)gpuFile.BaseStream.Length); //Update gpu_file size variable
                }
            }
        }

        private void WriteHeader(BinaryWriter header)
        {
            header.Write(Signature);
            header.Write(Version);
            header.Write(Platform);
            header.Write(DirectoryBlockSize); //Updated to the correct value in Write() once all the info is gathered
            header.Write(DataBlockSize);
            header.Write(NumberOfBitmaps);
            header.Write(Flags);
            header.Write(TotalEntries);
            header.Write(AlignValue);
        }

        private void WriteEntries(BinaryWriter header)
        {
            foreach (var entry in Entries)
            {
                entry.Write(header);
            }
        }

        private void WriteEntryNames(BinaryWriter header)
        {
            foreach (var entry in Entries)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(entry.Name);
                header.Write(bytes, 0, bytes.Length);
                header.Write((byte)0);
            }
        }

        private void WriteEntryData(BinaryWriter gpuFile, List<byte[]> rawDatas)
        {
            int index = 0;
            uint offset = 0;
            foreach (var entry in Entries)
            {
                var rawData = rawDatas[index];
                if (entry.bitmap_format == PegFormat.PC_8888)
                {
                    //rawData = Util.RgbaToBgra(rawData);
                    throw new Exception($"Entry {index}, \"{entry.Name}\", has the format PC_8888. " 
                                        + $"Saving for this pixel format isn't supported yet, just viewing & extracting.");
                }

                if (entry.Edited)
                {
                    if (entry.bitmap_format == PegFormat.PC_DXT1)
                    {
                        var compressBuffer = Squish.Compress(rawData, entry.width, entry.height, Squish.Flags.DXT1);
                        gpuFile.Write(compressBuffer);
                        entry.frame_size = (uint)compressBuffer.Length;
                    }
                    else if (entry.bitmap_format == PegFormat.PC_DXT3)
                    {
                        var compressBuffer = Squish.Compress(rawData, entry.width, entry.height, Squish.Flags.DXT3);
                        gpuFile.Write(compressBuffer);
                        entry.frame_size = (uint)compressBuffer.Length;
                    }
                    else if (entry.bitmap_format == PegFormat.PC_DXT5)
                    {
                        var compressBuffer = Squish.Compress(rawData, entry.width, entry.height, Squish.Flags.DXT5);
                        gpuFile.Write(compressBuffer);
                        entry.frame_size = (uint)compressBuffer.Length;
                    }
                    //else if (entry.bitmap_format == PegFormat.PC_8888)
                    //{
                    //    //gpuFile.Write(rawData);
                    //    //entry.frame_size = (uint)rawData.Length;
                    //    //throw new Exception($"Entry {index}, \"{entry.Name}\", has the format PC_8888. Saving for this pixel format isn't supported yet, just viewing.");
                    //}
                    else
                    {
                        throw new Exception($"Unsupported PEG data format detected! {entry.bitmap_format.ToString()} is not yet supported.");
                    }
                }
                else
                {
                    gpuFile.Write(rawData);
                    entry.frame_size = (uint)rawData.Length;
                    entry.data = offset;
                }
                //Align to alignment value
                gpuFile.Align(AlignValue);
                //Update offset to start of next entry
                offset = (uint)gpuFile.BaseStream.Position;
                index++;
            }
        }
    }
}