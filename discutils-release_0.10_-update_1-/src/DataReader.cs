namespace DiscUtils
{
    using System.IO;

    /// <summary>
    /// Base class for reading binary data from a stream.
    /// </summary>
    internal abstract class DataReader
    {
        protected Stream _stream;

        public DataReader(Stream stream)
        {
            _stream = stream;
        }

        public long Position
        {
            get { return _stream.Position; }
        }

        public long Length
        {
            get { return _stream.Length; }
        }

        public void Skip(int bytes)
        {
            ReadBytes(bytes);
        }

        public abstract ushort ReadUInt16();

        public abstract int ReadInt32();

        public abstract uint ReadUInt32();

        public abstract long ReadInt64();

        public abstract ulong ReadUInt64();

        public abstract byte[] ReadBytes(int count);
    }
}
