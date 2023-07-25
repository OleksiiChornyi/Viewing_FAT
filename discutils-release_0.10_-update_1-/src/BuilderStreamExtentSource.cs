namespace DiscUtils
{
    using System.IO;

    internal class BuilderStreamExtentSource : BuilderExtentSource
    {
        private Stream _stream;

        public BuilderStreamExtentSource(Stream stream)
        {
            _stream = stream;
        }

        public override BuilderExtent Fix(long pos)
        {
            return new BuilderStreamExtent(pos, _stream);
        }
    }
}
