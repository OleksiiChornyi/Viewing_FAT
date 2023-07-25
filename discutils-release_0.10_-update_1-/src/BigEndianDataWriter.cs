namespace DiscUtils
{
    using System.IO;

    internal class BigEndianDataWriter : DataWriter
    {
        public BigEndianDataWriter(Stream stream)
            : base(stream)
        {
        }

        public override void Write(ushort value)
        {
            byte[] buffer = new byte[2];
            Utilities.WriteBytesBigEndian(value, buffer, 0);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public override void Write(int value)
        {
            byte[] buffer = new byte[4];
            Utilities.WriteBytesBigEndian(value, buffer, 0);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public override void Write(uint value)
        {
            byte[] buffer = new byte[4];
            Utilities.WriteBytesBigEndian(value, buffer, 0);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public override void Write(long value)
        {
            byte[] buffer = new byte[8];
            Utilities.WriteBytesBigEndian(value, buffer, 0);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public override void Write(ulong value)
        {
            byte[] buffer = new byte[8];
            Utilities.WriteBytesBigEndian(value, buffer, 0);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public override void WriteBytes(byte[] value, int offset, int count)
        {
            _stream.Write(value, offset, count);
        }
    }
}
