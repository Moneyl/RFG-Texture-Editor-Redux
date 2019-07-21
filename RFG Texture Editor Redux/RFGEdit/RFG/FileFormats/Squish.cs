using System;
using System.Runtime.InteropServices;

namespace RFGEdit.RFG.FileFormats
{
    public class Squish
	{
        [DllImport("squish.dll", EntryPoint = "DecompressImage")]
		public static extern void Decompress([MarshalAs(UnmanagedType.LPArray)] byte[] rgba, uint width, uint height, [MarshalAs(UnmanagedType.LPArray)] byte[] blocks, int flags);

        [DllImport("squish.dll", EntryPoint = "CompressImage")]
        public static extern void Compress_Raw([MarshalAs(UnmanagedType.LPArray)] byte[] rgba, uint width, uint height, [MarshalAs(UnmanagedType.LPArray)] byte[] blocks, int flags); //additional arg: float* metric = 0

        [DllImport("squish.dll", EntryPoint = "GetStorageRequirements")]
        public static extern int GetStorageRequirements(uint width, uint height, int flags);

        public static byte[] Compress(byte[] rgba, uint width, uint height, int flags)
        {
            var compressBuffer = new byte[GetStorageRequirements(width, height, flags)];
            Compress_Raw(rgba, width, height, compressBuffer, flags);
            return compressBuffer;
        }

        public enum Flags
		{
            DXT1 = 1,
            DXT3 = 2,
            DXT5 = 4,
            ColourClusterFit = 8,
            ColourRangeFit = 16,
            ColourMetricPerceptual = 32,
            ColourMetricUniform = 64,
            WeightColourByAlpha = 128,
            ColourIterativeClusterFit = 256
		}
	}
}
