namespace DiscUtils.Fat
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class FatFileStream : SparseStream
    {
        private Directory _dir;
        private long _dirId;
        private ClusterStream _stream;

        private bool didWrite = false;

        public FatFileStream(FatFileSystem fileSystem, Directory dir, long fileId, FileAccess access)
        {
            _dir = dir;
            _dirId = fileId;

            DirectoryEntry dirEntry = _dir.GetEntry(_dirId);
            _stream = new ClusterStream(fileSystem, access, (uint)dirEntry.FirstCluster, (uint)dirEntry.FileSize);
            _stream.FirstClusterChanged += FirstClusterAllocatedHandler;
        }

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override IEnumerable<StreamExtent> Extents
        {
            get
            {
                return new StreamExtent[] { new StreamExtent(0, Length) };
            }
        }

        public override void Close()
        {
            if (_dir.FileSystem.CanWrite)
            {
                try
                {
                    DateTime now = _dir.FileSystem.ConvertFromUtc(DateTime.UtcNow);

                    DirectoryEntry dirEntry = _dir.GetEntry(_dirId);
                    dirEntry.LastAccessTime = now;
                    if (didWrite)
                    {
                        dirEntry.FileSize = (int)_stream.Length;
                        dirEntry.LastWriteTime = now;
                    }

                    _dir.UpdateEntry(_dirId, dirEntry);
                }
                finally
                {
                    base.Close();
                }
            }
        }

        public override void SetLength(long value)
        {
            didWrite = true;
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            didWrite = true;
            _stream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        private void FirstClusterAllocatedHandler(uint cluster)
        {
            DirectoryEntry dirEntry = _dir.GetEntry(_dirId);
            dirEntry.FirstCluster = cluster;
            _dir.UpdateEntry(_dirId, dirEntry);
        }
    }
}
