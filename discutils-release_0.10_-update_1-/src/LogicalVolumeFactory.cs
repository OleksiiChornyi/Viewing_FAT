namespace DiscUtils
{
    using System.Collections.Generic;

    internal abstract class LogicalVolumeFactory
    {
        public abstract bool HandlesPhysicalVolume(PhysicalVolumeInfo volume);

        public abstract void MapDisks(IEnumerable<VirtualDisk> disks, Dictionary<string, LogicalVolumeInfo> result);
    }
}
