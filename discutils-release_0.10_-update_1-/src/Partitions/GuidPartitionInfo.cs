namespace DiscUtils.Partitions
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides access to partition records in a GUID partition table.
    /// </summary>
    public sealed class GuidPartitionInfo : PartitionInfo
    {
        private GuidPartitionTable _table;
        private GptEntry _entry;

        internal GuidPartitionInfo(GuidPartitionTable table, GptEntry entry)
        {
            _table = table;
            _entry = entry;
        }

        /// <summary>
        /// Gets the first sector of the partion (relative to start of disk) as a Logical Block Address.
        /// </summary>
        public override long FirstSector
        {
            get { return _entry.FirstUsedLogicalBlock; }
        }

        /// <summary>
        /// Gets the last sector of the partion (relative to start of disk) as a Logical Block Address (inclusive).
        /// </summary>
        public override long LastSector
        {
            get { return _entry.LastUsedLogicalBlock; }
        }

        /// <summary>
        /// Gets the type of the partition, as a GUID.
        /// </summary>
        public override Guid GuidType
        {
            get { return _entry.PartitionType; }
        }

        /// <summary>
        /// Always returns Zero.
        /// </summary>
        public override byte BiosType
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the type of the partition as a string.
        /// </summary>
        public override string TypeAsString
        {
            get { return _entry.FriendlyPartitionType; }
        }

        /// <summary>
        /// Gets the name of the partition.
        /// </summary>
        public string Name
        {
            get { return _entry.Name; }
        }

        /// <summary>
        /// Gets the attributes of the partition.
        /// </summary>
        public long Attributes
        {
            get { return (long)_entry.Attributes; }
        }

        /// <summary>
        /// Gets the unique identity of this specific partition.
        /// </summary>
        public Guid Identity
        {
            get { return _entry.Identity; }
        }

        internal override PhysicalVolumeType VolumeType
        {
            get { return PhysicalVolumeType.GptPartition; }
        }

        /// <summary>
        /// Opens a stream to access the content of the partition.
        /// </summary>
        /// <returns>The new stream</returns>
        public override SparseStream Open()
        {
            return _table.Open(_entry);
        }
    }
}
