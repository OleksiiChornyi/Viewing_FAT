namespace DiscUtils.Partitions
{
    /// <summary>
    /// Calculates CRC32 of buffers.
    /// </summary>
    internal static class Crc32
    {
        private const uint Polynomial = 0xedb88320;

        private static readonly uint[] Table;

        static Crc32()
        {
            uint[] table = new uint[256];

            table[0] = 0;
            for (uint i = 0; i <= 255; ++i)
            {
                uint crc = i;

                for (int j = 8; j > 0; --j)
                {
                    if ((crc & 1) != 0)
                    {
                        crc = (crc >> 1) ^ Polynomial;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }

                table[i] = crc;

                Table = table;
            }
        }

        public static uint Compute(uint crc, byte[] buffer, int offset, int count)
        {
            uint value = crc;

            for (int i = 0; i < count; ++i)
            {
                byte b = buffer[offset + i];

                uint temp1 = (value >> 8) & 0x00FFFFFF;
                uint temp2 = Table[(value ^ b) & 0xFF];
                value = temp1 ^ temp2;
            }

            return value;
        }
    }
}
