namespace DiscUtils.Vhd
{
    internal sealed class DiskExtent : VirtualDiskExtent
    {
        private DiskImageFile _file;

        public DiskExtent(DiskImageFile file)
        {
            _file = file;
        }

        public override bool IsSparse
        {
            get { return _file.IsSparse; }
        }

        public override long Capacity
        {
            get { return _file.Capacity; }
        }

        public override long StoredSize
        {
            get { return _file.StoredSize; }
        }

        public override MappedStream OpenContent(SparseStream parent, Ownership ownsParent)
        {
            return _file.DoOpenContent(parent, ownsParent);
        }
    }
}
