using System.Drawing;
using System.IO;
using TextureEditor.Utility;

namespace TextureEditor.Peg
{
    public class PegEntry
    {
        public uint data; //Data offset of the entry in the gpeg file.
        public ushort width;
        public ushort height;
        public PegFormat bitmap_format; //Todo: Make an enum for this. //Todo: Figure out if rfg actually supports all values in the enum.
        public ushort source_width; //Todo: Figure out if really two byte values, actual var type is peg_entry::peg_flag_and_size_union 
        public ushort anim_tiles_width;
        public ushort anim_tiles_height;
        public ushort num_frames;
        public ushort flags;
        public uint filename; //Filename offset for this entry in the header file?
        public ushort source_height; //Todo: Figure out if really two byte values, actual var type is peg_entry::peg_flag_and_size_union 
        public byte fps;
        public byte mip_levels;
        public uint frame_size;
        public uint next = 0; //This value and the following 3 are runtime only values AFAIK and are always zero. //Todo: Double check that's true.
        public uint previous = 0;
        public uint cache0 = 0;
        public uint cache1 = 0;

        //Note: This is stored separately from the other entry data. Placed in this class for coding convenience.
        public string Name;

        //Note: Only use these two if Edited == true
        //This is the raw/unconverted data. Used an edited texture wasn't imported, to avoid two conversions
        public byte[] RawData;
        //This is stored in the gpu file (gpeg_pc or gvbm_pc) and converted to a bitmap for easy use with the editor.
        public Bitmap Bitmap; //Todo: Figure out if some other type like BitmapImage or ImageSource would work better here.
        //True if an edited version was imported
        public bool Edited = false;

        public void Read(BinaryReader header)
        {
            data = header.ReadUInt32BE();
            width = header.ReadUInt16BE();
            height = header.ReadUInt16BE();
            bitmap_format = (PegFormat)header.ReadUInt16BE();
            source_width = header.ReadUInt16BE();
            anim_tiles_width = header.ReadUInt16BE();
            anim_tiles_height = header.ReadUInt16BE();
            num_frames = header.ReadUInt16BE();
            flags = header.ReadUInt16BE();
            filename = header.ReadUInt32BE();
            source_height = header.ReadUInt16BE();
            fps = header.ReadByte();
            mip_levels = header.ReadByte();
            frame_size = header.ReadUInt32BE();
            next = header.ReadUInt32BE();
            previous = header.ReadUInt32BE();
            cache0 = header.ReadUInt32BE();
            cache1 = header.ReadUInt32BE();
        }

        public void Write(BinaryWriter header)
        {
            //Todo: Update these values before writing since some could've changed.
            header.Write(data);
            header.Write(width);
            header.Write(height);
            header.Write((ushort)bitmap_format);
            header.Write(source_width);
            header.Write(anim_tiles_width);
            header.Write(anim_tiles_height);
            header.Write(num_frames);
            header.Write(flags);
            header.Write(filename);
            header.Write(source_height);
            header.Write(fps);
            header.Write(mip_levels);
            header.Write(frame_size);
            header.Write(next);
            header.Write(previous);
            header.Write(cache0);
            header.Write(cache1);
        }
    }
}
