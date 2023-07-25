namespace DiscUtils
{
    using System;

    internal class BuilderBufferExtent : BuilderExtent
    {
        private bool _fixedBuffer;
        private byte[] _buffer;

        public BuilderBufferExtent(long start, long length)
            : base(start, length)
        {
        }

        public BuilderBufferExtent(long start, byte[] buffer)
            : base(start, buffer.Length)
        {
            _fixedBuffer = true;
            _buffer = buffer;
        }

        internal override void PrepareForRead()
        {
            if (!_fixedBuffer)
            {
                _buffer = GetBuffer();
            }
        }

        internal override int Read(long diskOffset, byte[] block, int offset, int count)
        {
            int startOffset = (int)(diskOffset - Start);
            int numBytes = (int)Math.Min(Length - startOffset, count);
            Array.Copy(_buffer, startOffset, block, offset, numBytes);
            return numBytes;
        }

        internal override void DisposeReadState()
        {
            if (!_fixedBuffer)
            {
                _buffer = null;
            }
        }

        protected virtual byte[] GetBuffer()
        {
            throw new NotSupportedException("Derived class should implement");
        }
    }
}
