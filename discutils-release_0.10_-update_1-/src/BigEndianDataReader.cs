namespace DiscUtils
{
    using System.IO;

    internal class BigEndianDataReader : DataReader
    {
        public BigEndianDataReader(Stream stream)
            : base(stream)
        {
        }

        public override ushort ReadUInt16()
        {
            return Utilities.ToUInt16BigEndian(Utilities.ReadFully(_stream, 2), 0);
        }

        public override int ReadInt32()
        {
            return Utilities.ToInt32BigEndian(Utilities.ReadFully(_stream, 4), 0);
        }

        public override uint ReadUInt32()
        {
            return Utilities.ToUInt32BigEndian(Utilities.ReadFully(_stream, 4), 0);
        }

        public override long ReadInt64()
        {
            return Utilities.ToInt64BigEndian(Utilities.ReadFully(_stream, 8), 0);
        }

        public override ulong ReadUInt64()
        {
            return Utilities.ToUInt64BigEndian(Utilities.ReadFully(_stream, 8), 0);
        }

        public override byte[] ReadBytes(int count)
        {
            return Utilities.ReadFully(_stream, count);
        }
    }
}
