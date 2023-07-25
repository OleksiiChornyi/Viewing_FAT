namespace DiscUtils.Partitions
{
    using System.IO;

    internal abstract class PartitionTableFactory
    {
        public abstract bool DetectIsPartitioned(Stream s);

        public abstract PartitionTable DetectPartitionTable(VirtualDisk disk);
    }
}
