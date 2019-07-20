using System;
using System.Runtime.InteropServices;

namespace RFGEdit.RFG.FileFormats
{
    public struct PegFrame
	{
        public uint Data;
        public ushort Width;
        public ushort Height;
        public ushort Format; //bitmap_format
        public ushort SourceWidth;
        public ushort AnimTilesWidth; //anim_tiles_width
        public ushort AnimTilesHeight; //anim_tiles_height
        public ushort NumFrames; //previously "Frames"
        public ushort Flags;
        public uint Filename; //Filename hash?
        public ushort SourceHeight;
        public byte Fps;
        public byte MipLevels;
        public uint Size; //FrameSize
        public uint Next; //Runtime value
        public uint Previous; //Runtime value
        public uint Cache1; //Unknown purpose
        public uint Cache2; //Unknown purpose
	}
}
