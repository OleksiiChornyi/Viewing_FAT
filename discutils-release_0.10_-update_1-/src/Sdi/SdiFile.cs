namespace DiscUtils.Sdi
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Class for accessing the contents of Simple Deployment Image (.sdi) files.
    /// </summary>
    /// <remarks>SDI files are primitive disk images, containing multiple blobs.</remarks>
    public sealed class SdiFile : IDisposable
    {
        private Stream _stream;
        private Ownership _ownership;
        private FileHeader _header;
        private List<SectionRecord> _sections;

        /// <summary>
        /// Initializes a new instance of the SdiFile class.
        /// </summary>
        /// <param name="stream">The stream formatted as an SDI file.</param>
        public SdiFile(Stream stream)
            : this(stream, Ownership.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SdiFile class.
        /// </summary>
        /// <param name="stream">The stream formatted as an SDI file.</param>
        /// <param name="ownership">Whether to pass ownership of <c>stream</c> to the new instance</param>
        public SdiFile(Stream stream, Ownership ownership)
        {
            _stream = stream;
            _ownership = ownership;

            byte[] page = Utilities.ReadFully(_stream, 512);

            _header = new FileHeader();
            _header.ReadFrom(page, 0);

            _stream.Position = _header.PageAlignment * 512;
            byte[] toc = Utilities.ReadFully(_stream, (int)(_header.PageAlignment * 512));

            _sections = new List<SectionRecord>();
            int pos = 0;
            while (Utilities.ToUInt64LittleEndian(toc, pos) != 0)
            {
                SectionRecord record = new SectionRecord();
                record.ReadFrom(toc, pos);

                _sections.Add(record);

                pos += SectionRecord.RecordSize;
            }
        }

        /// <summary>
        /// Gets all of the sections within the file.
        /// </summary>
        public IEnumerable<Section> Sections
        {
            get
            {
                int i = 0;
                foreach (var section in _sections)
                {
                    yield return new Section(section, i++);
                }
            }
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (_ownership == Ownership.Dispose && _stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }

        /// <summary>
        /// Opens a stream to access a particular section.
        /// </summary>
        /// <param name="index">The zero-based index of the section</param>
        /// <returns>A stream that can be used to access the section.</returns>
        public Stream OpenSection(int index)
        {
            return new SubStream(_stream, _sections[index].Offset, _sections[index].Size);
        }
    }
}
