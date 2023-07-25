namespace DiscUtils
{
    using System.Collections.Generic;

    internal abstract class BuilderExtent
    {
        private long _start;
        private long _length;

        public BuilderExtent(long start, long length)
        {
            _start = start;
            _length = length;
        }

        /// <summary>
        /// Gets the parts of the stream that are stored.
        /// </summary>
        /// <remarks>This may be an empty enumeration if all bytes are zero.</remarks>
        public virtual IEnumerable<StreamExtent> StreamExtents
        {
            get
            {
                return new StreamExtent[] { new StreamExtent(Start, Length) };
            }
        }

        internal long Start
        {
            get { return _start; }
        }

        internal long Length
        {
            get { return _length; }
        }

        internal abstract void PrepareForRead();

        internal abstract int Read(long diskOffset, byte[] block, int offset, int count);

        internal abstract void DisposeReadState();
    }
}
