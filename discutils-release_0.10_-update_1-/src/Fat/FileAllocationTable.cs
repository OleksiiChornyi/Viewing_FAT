namespace DiscUtils.Fat
{
    using System;
    using System.IO;

    internal class FileAllocationTable
    {
        private Stream _stream;
        private ushort _firstFatSector;
        private byte _numFats;

        private FatBuffer _buffer;

        public FileAllocationTable(FatType type, Stream stream, ushort firstFatSector, uint fatSize, byte numFats, byte activeFat)
        {
            _stream = stream;
            _firstFatSector = firstFatSector;
            _numFats = numFats;

            _stream.Position = (firstFatSector + (fatSize * activeFat)) * Utilities.SectorSize;
            _buffer = new FatBuffer(type, Utilities.ReadFully(_stream, (int)(fatSize * Utilities.SectorSize)));
        }

        internal bool IsFree(uint val)
        {
            return _buffer.IsFree(val);
        }

        internal bool IsEndOfChain(uint val)
        {
            return _buffer.IsEndOfChain(val);
        }

        internal bool IsBadCluster(uint val)
        {
            return _buffer.IsBadCluster(val);
        }

        internal uint GetNext(uint cluster)
        {
            return _buffer.GetNext(cluster);
        }

        internal void SetEndOfChain(uint cluster)
        {
            _buffer.SetEndOfChain(cluster);
        }

        internal void SetBadCluster(uint cluster)
        {
            _buffer.SetBadCluster(cluster);
        }

        internal void SetNext(uint cluster, uint next)
        {
            _buffer.SetNext(cluster, next);
        }

        internal void Flush()
        {
            for (int i = 0; i < _numFats; ++i)
            {
                _buffer.WriteDirtyRegions(_stream, (_firstFatSector * Utilities.SectorSize) + (_buffer.Size * i));
            }

            _buffer.ClearDirtyRegions();
        }

        internal bool TryGetFreeCluster(out uint cluster)
        {
            return _buffer.TryGetFreeCluster(out cluster);
        }

        internal void FreeChain(uint head)
        {
            _buffer.FreeChain(head);
        }
    }
}
