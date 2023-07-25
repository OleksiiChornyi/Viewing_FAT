namespace DiscUtils.Vhd
{
    using System.IO;

    internal class Header
    {
        public string Cookie;
        public long DataOffset;

        public static Header FromStream(Stream stream)
        {
            return FromBytes(Utilities.ReadFully(stream, 16), 0);
        }

        public static Header FromBytes(byte[] data, int offset)
        {
            Header result = new Header();
            result.Cookie = Utilities.BytesToString(data, offset, 8);
            result.DataOffset = Utilities.ToInt64BigEndian(data, offset + 8);
            return result;
        }
    }
}
