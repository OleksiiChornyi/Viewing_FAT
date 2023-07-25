namespace DiscUtils
{
    using System.Collections.Generic;
    using System.IO;

    internal class PassthroughStreamBuilder : StreamBuilder
    {
        private Stream _stream;

        public PassthroughStreamBuilder(Stream stream)
        {
            _stream = stream;
        }

        internal override List<BuilderExtent> FixExtents(out long totalLength)
        {
            _stream.Position = 0;
            List<BuilderExtent> result = new List<BuilderExtent>();
            result.Add(new BuilderStreamExtent(0, _stream));
            totalLength = _stream.Length;
            return result;
        }
    }
}
