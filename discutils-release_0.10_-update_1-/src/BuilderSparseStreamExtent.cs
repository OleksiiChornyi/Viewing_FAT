namespace DiscUtils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class BuilderSparseStreamExtent : BuilderExtent
    {
        private SparseStream _stream;

        public BuilderSparseStreamExtent(long start, SparseStream stream)
            : base(start, stream.Length)
        {
            _stream = stream;
        }

        public override IEnumerable<StreamExtent> StreamExtents
        {
            get { return StreamExtent.Offset(_stream.Extents, Start); }
        }

        internal override void PrepareForRead()
        {
        }

        internal override int Read(long diskOffset, byte[] block, int offset, int count)
        {
            _stream.Position = diskOffset - Start;
            return _stream.Read(block, offset, count);
        }

        internal override void DisposeReadState()
        {
        }
    }
}
