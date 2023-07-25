namespace DiscUtils.Fat
{
    using System;
    using System.IO;

    internal sealed class ClusterReader
    {
        private Stream _stream;
        private int _firstDataSector;
        private int _sectorsPerCluster;
        private int _bytesPerSector;

        /// <summary>
        /// Pre-calculated value because of number of uses of this externally.
        /// </summary>
        private int _clusterSize;

        public ClusterReader(Stream stream, int firstDataSector, int sectorsPerCluster, int bytesPerSector)
        {
            _stream = stream;
            _firstDataSector = firstDataSector;
            _sectorsPerCluster = sectorsPerCluster;
            _bytesPerSector = bytesPerSector;

            _clusterSize = _sectorsPerCluster * _bytesPerSector;
        }

        public int ClusterSize
        {
            get { return _clusterSize; }
        }

        public void ReadCluster(uint cluster, byte[] buffer, int offset)
        {
            if (offset + ClusterSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset", "buffer is too small - cluster would overflow buffer");
            }

            uint firstSector = (uint)(((cluster - 2) * _sectorsPerCluster) + _firstDataSector);

            _stream.Position = firstSector * _bytesPerSector;
            if (Utilities.ReadFully(_stream, buffer, offset, _clusterSize) != _clusterSize)
            {
                throw new IOException("Failed to read cluster " + cluster);
            }
        }

        internal void WriteCluster(uint cluster, byte[] buffer, int offset)
        {
            if (offset + ClusterSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset", "buffer is too small - cluster would overflow buffer");
            }

            uint firstSector = (uint)(((cluster - 2) * _sectorsPerCluster) + _firstDataSector);

            _stream.Position = firstSector * _bytesPerSector;

            _stream.Write(buffer, offset, _clusterSize);
        }

        internal void WipeCluster(uint cluster)
        {
            uint firstSector = (uint)(((cluster - 2) * _sectorsPerCluster) + _firstDataSector);

            _stream.Position = firstSector * _bytesPerSector;

            _stream.Write(new byte[_clusterSize], 0, _clusterSize);
        }
    }
}
