namespace DiscUtils
{
    using System.IO;

    internal abstract class DataWriter
    {
        protected Stream _stream;

        public DataWriter(Stream stream)
        {
            _stream = stream;
        }

        public abstract void Write(ushort value);

        public abstract void Write(int value);

        public abstract void Write(uint value);

        public abstract void Write(long value);

        public abstract void Write(ulong value);

        public abstract void WriteBytes(byte[] value, int offset, int count);

        public void WriteBytes(byte[] value)
        {
            WriteBytes(value, 0, value.Length);
        }
    }
}
