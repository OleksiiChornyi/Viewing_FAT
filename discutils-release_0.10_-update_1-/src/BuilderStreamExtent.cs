namespace DiscUtils
{
    using System.IO;

    internal class BuilderStreamExtent : BuilderExtent
    {
        private Stream _source;

        public BuilderStreamExtent(long start, Stream source)
            : base(start, source.Length)
        {
            _source = source;
        }

        internal override void PrepareForRead()
        {
        }

        internal override int Read(long diskOffset, byte[] block, int offset, int count)
        {
            _source.Position = diskOffset - Start;
            return _source.Read(block, offset, count);
        }

        internal override void DisposeReadState()
        {
        }
    }
}
