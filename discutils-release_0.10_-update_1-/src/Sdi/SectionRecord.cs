namespace DiscUtils.Sdi
{
    internal class SectionRecord
    {
        public const int RecordSize = 64;

        public string SectionType;
        public ulong Attr;
        public long Offset;
        public long Size;
        public ulong PartitionType;

        public void ReadFrom(byte[] buffer, int offset)
        {
            SectionType = Utilities.BytesToString(buffer, offset, 8).TrimEnd('\0');
            Attr = Utilities.ToUInt64LittleEndian(buffer, offset + 8);
            Offset = Utilities.ToInt64LittleEndian(buffer, offset + 16);
            Size = Utilities.ToInt64LittleEndian(buffer, offset + 24);
            PartitionType = Utilities.ToUInt64LittleEndian(buffer, offset + 32);
        }
    }
}
