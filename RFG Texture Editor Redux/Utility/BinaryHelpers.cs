using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TextureEditor.Utility
{
    public static class BinaryHelpers
    {
        //BinaryReader extension functions
        public static string ReadNullTerminatedString(this BinaryReader stream)
        {
            var String = new StringBuilder();
            do
            {
                String.Append(stream.ReadChar()); //Since the character isn't a null byte, add it to the string
            }
            while (stream.PeekChar() != 0); //Read bytes until a null byte (string terminator) is reached

            stream.ReadByte(); //Read past the null terminator
            return String.ToString();
        }

        public static string ReadFixedLengthString(this BinaryReader stream, int length)
        {
            var String = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                String.Append(stream.ReadChar());
            }
            return String.ToString();
        }

        public static string ReadFixedLengthString(this BinaryReader stream, uint length)
        {
            return stream.ReadFixedLengthString((int)length);
        }

        public static int Align(this BinaryReader reader, long alignmentValue = 2048)
        {
            long position = reader.BaseStream.Position;
            int remainder = (int)(position % alignmentValue);
            int paddingSize = 0;
            if (remainder > 0)
            {
                paddingSize = (int)alignmentValue - remainder;
            }
            else
            {
                paddingSize = 0;
            }
            reader.Skip(paddingSize);
            return paddingSize;
        }

        public static ushort PeekUshort(this BinaryReader reader)
        {
            ushort val = reader.ReadUInt16();
            reader.BaseStream.Seek(-2, SeekOrigin.Current);
            return val;
        }

        public static BinaryReader Skip(this BinaryReader reader, long skipDistance)
        {
            reader.BaseStream.Seek(skipDistance, SeekOrigin.Current);
            return reader; //Return self so things like Align() can be chained with this.
        }

        public static BinaryWriter Skip(this BinaryWriter writer, long skipDistance)
        {
            writer.BaseStream.Seek(skipDistance, SeekOrigin.Current);
            return writer; //Return self so things like Align() can be chained with this.
        }

        public static void Skip(this Stream stream, long skipDistance)
        {
            stream.Seek(skipDistance, SeekOrigin.Current);
        }

        public static XElement ReadVector3fToXElement(this BinaryReader reader, string elementName)
        {
            var data = new XElement(elementName);
            data.Add(new XElement("x", reader.ReadSingle()));
            data.Add(new XElement("y", reader.ReadSingle()));
            data.Add(new XElement("z", reader.ReadSingle()));
            return data;
        }

        public static void SeekAndSkip(this BinaryReader reader, long seekPos, long skipDistance)
        {
            reader.BaseStream.Seek(seekPos, SeekOrigin.Begin);
            reader.Skip(skipDistance);
        }

        //Stream extension functions
        public static int GetAlignmentPad(this Stream stream, long alignmentValue = 2048)
        {
            int remainder = (int)(stream.Position % 2048);
            if (remainder > 0)
            {
                return 2048 - remainder;
            }
            return 0;
        }

        public static uint GetAlignmentPad(this Stream stream, uint alignmentValue = 2048)
        {
            uint remainder = ((uint)stream.Position) % 2048;
            if (remainder > 0)
            {
                return 2048 - remainder;
            }
            return 0;
        }

        //BinaryWriter extension functions
        public static int GetAlignmentPad(this BinaryWriter writer, long alignmentValue = 2048)
        {
            return writer.BaseStream.GetAlignmentPad(alignmentValue);
        }

        public static uint GetAlignmentPad(this BinaryWriter writer, uint alignmentValue = 2048)
        {
            return writer.BaseStream.GetAlignmentPad(alignmentValue);
        }

        public static BinaryWriter WriteAsciiString(this BinaryWriter writer, String stringOut, bool nullTerminate)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(stringOut);
            writer.Write(bytes, 0, bytes.Length);
            if (nullTerminate)
            {
                writer.Write((byte)0);
            }

            return writer;
        }

        public static int Align(this BinaryWriter writer, long alignmentValue = 2048)
        {
            long position = writer.BaseStream.Position;
            int remainder = (int)(position % alignmentValue);

            int paddingSize = 0;
            if (remainder > 0)
                paddingSize = (int)alignmentValue - remainder;
            else
                paddingSize = 0;

            writer.Write(Enumerable.Repeat((byte)0x0, paddingSize).ToArray(), 0, paddingSize);
            return paddingSize;
        }

        public static void WriteNullBytes(this BinaryWriter writer, long numBytesToWrite)
        {
            var bytes = new byte[numBytesToWrite];
            writer.Write(bytes);
        }

        public static void WriteNullTerminatedString(this BinaryWriter stream, string output)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(output);
            stream.Write(bytes, 0, bytes.Length);
            stream.Write((byte)0);
        }

        public static uint GetAlignmentPad(long currentPos, long alignmentValue)
        {
            uint remainder = (uint)(currentPos % alignmentValue);
            uint paddingSize = 0;
            if (remainder > 0)
            {
                paddingSize = (uint)alignmentValue - remainder;
            }
            else
            {
                paddingSize = 0;
            }

            return paddingSize;
        }


        //Misc helpers
        public static int GetAlignmentPad(long position)
        {
            int remainder = (int)(position % 2048);
            if (remainder > 0)
            {
                return 2048 - remainder;
            }
            return 0;
        }


        // Note this MODIFIES THE GIVEN ARRAY then returns a reference to the modified array.
        public static byte[] Reverse(this byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        public static UInt16 ReadUInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt16(binRdr.ReadBytesRequired(sizeof(UInt16)).Reverse(), 0);
        }

        public static Int16 ReadInt16BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt16(binRdr.ReadBytesRequired(sizeof(Int16)).Reverse(), 0);
        }

        public static UInt32 ReadUInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof(UInt32)).Reverse(), 0);
        }

        public static Int32 ReadInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof(Int32)).Reverse(), 0);
        }

        public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
        {
            var result = binRdr.ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.", byteCount, result.Length));

            return result;
        }
    }
}
