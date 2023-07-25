namespace DiscUtils.Sdi
{
    /// <summary>
    /// Information about a blob (or section) within an SDI file.
    /// </summary>
    public class Section
    {
        private SectionRecord _record;
        private int _index;

        internal Section(SectionRecord record, int index)
        {
            _record = record;
            _index = index;
        }

        /// <summary>
        /// Gets the zero-based index of this section.
        /// </summary>
        public int Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Gets the type of this section.
        /// </summary>
        /// <remarks>Sample types are "PART" (disk partition), "WIM" (Windows Imaging Format)</remarks>
        public string SectionType
        {
            get { return _record.SectionType; }
        }

        /// <summary>
        /// Gets the MBR partition type of the partition, for "PART" sections.
        /// </summary>
        public byte PartitionType
        {
            get { return (byte)_record.PartitionType; }
        }

        /// <summary>
        /// Gets the length of the section.
        /// </summary>
        public long Length
        {
            get { return _record.Size; }
        }
    }
}
