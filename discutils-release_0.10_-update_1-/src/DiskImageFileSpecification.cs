namespace DiscUtils
{
    /// <summary>
    /// Describes a particular file that is a constituent part of a virtual disk.
    /// </summary>
    public sealed class DiskImageFileSpecification
    {
        private string _name;
        private StreamBuilder _builder;

        internal DiskImageFileSpecification(string name, StreamBuilder builder)
        {
            _name = name;
            _builder = builder;
        }

        /// <summary>
        /// Gets name of the file.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the object that provides access to the file's content.
        /// </summary>
        /// <returns>A stream object that contains the file's content</returns>
        public SparseStream OpenStream()
        {
            return _builder.Build();
        }
    }
}
