namespace DiscUtils
{
    internal class BuilderBufferExtentSource : BuilderExtentSource
    {
        private byte[] _buffer;

        public BuilderBufferExtentSource(byte[] buffer)
        {
            _buffer = buffer;
        }

        public override BuilderExtent Fix(long pos)
        {
            return new BuilderBufferExtent(pos, _buffer);
        }
    }
}
