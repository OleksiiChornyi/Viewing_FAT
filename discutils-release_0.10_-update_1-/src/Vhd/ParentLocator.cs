namespace DiscUtils.Vhd
{
    internal class ParentLocator
    {
        public const string PlatformCodeWindowsRelativeUnicode = "W2ru";
        public const string PlatformCodeWindowsAbsoluteUnicode = "W2ku";

        public string PlatformCode;
        public int PlatformDataSpace;
        public int PlatformDataLength;
        public long PlatformDataOffset;

        public ParentLocator()
        {
            PlatformCode = string.Empty;
        }

        public ParentLocator(ParentLocator toCopy)
        {
            PlatformCode = toCopy.PlatformCode;
            PlatformDataSpace = toCopy.PlatformDataSpace;
            PlatformDataLength = toCopy.PlatformDataLength;
            PlatformDataOffset = toCopy.PlatformDataOffset;
        }

        public static ParentLocator FromBytes(byte[] data, int offset)
        {
            ParentLocator result = new ParentLocator();
            result.PlatformCode = Utilities.BytesToString(data, offset, 4);
            result.PlatformDataSpace = Utilities.ToInt32BigEndian(data, offset + 4);
            result.PlatformDataLength = Utilities.ToInt32BigEndian(data, offset + 8);
            result.PlatformDataOffset = Utilities.ToInt64BigEndian(data, offset + 16);
            return result;
        }

        internal void ToBytes(byte[] data, int offset)
        {
            Utilities.StringToBytes(PlatformCode, data, offset, 4);
            Utilities.WriteBytesBigEndian(PlatformDataSpace, data, offset + 4);
            Utilities.WriteBytesBigEndian(PlatformDataLength, data, offset + 8);
            Utilities.WriteBytesBigEndian((uint)0, data, offset + 12);
            Utilities.WriteBytesBigEndian(PlatformDataOffset, data, offset + 16);
        }
    }
}
