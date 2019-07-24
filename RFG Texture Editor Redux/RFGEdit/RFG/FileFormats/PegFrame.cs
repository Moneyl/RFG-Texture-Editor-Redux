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
        public ushort AnimTilesWidth; //In the case of animated textures, the number of textures going horizontally.
        public ushort AnimTilesHeight; //In the case of animated textures, the number of textures going vertically.
        public ushort NumFrames; //previously "Frames"
        public ushort Flags; //previous "Unknown12", offset 18
        public uint Filename; //Filename hash?
        public ushort SourceHeight; 
        public byte Fps; //Previously "Unknown1A", offset 26
        public byte MipLevels;
        public uint Size; //FrameSize
        public uint Next; //Runtime value
        public uint Previous; //Runtime value
        public uint Cache1; //Unknown purpose
        public uint Cache2; //Unknown purpose
	}
}
