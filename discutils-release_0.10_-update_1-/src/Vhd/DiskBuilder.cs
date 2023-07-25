namespace DiscUtils.Vhd
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Creates new VHD disks by wrapping existing streams.
    /// </summary>
    /// <remarks>Using this method for creating virtual disks avoids consuming
    /// large amounts of memory, or going via the local file system when the aim
    /// is simply to present a VHD version of an existing disk.</remarks>
    public sealed class DiskBuilder : DiskImageBuilder
    {
        private FileType _diskType = FileType.Dynamic;

        /// <summary>
        /// Gets or sets the type of VHD file to build.
        /// </summary>
        public FileType DiskType
        {
            get { return _diskType; }
            set { _diskType = value; }
        }

        /// <summary>
        /// Initiates the build process.
        /// </summary>
        /// <param name="baseName">The base name for the VMDK, for example 'foo' to create 'foo.vhd'.</param>
        /// <returns>A set of one or more logical files that constitute the VHD.  The first file is
        /// the 'primary' file that is normally attached to VMs.</returns>
        public override DiskImageFileSpecification[] Build(string baseName)
        {
            if (string.IsNullOrEmpty(baseName))
            {
                throw new ArgumentException("Invalid base file name", "baseName");
            }

            if (Content == null)
            {
                throw new InvalidOperationException("No content stream specified");
            }

            List<DiskImageFileSpecification> fileSpecs = new List<DiskImageFileSpecification>();

            Geometry geometry = Geometry ?? Geometry.FromCapacity(Content.Length);

            Footer footer = new Footer(geometry, Content.Length, DiskType);

            if (_diskType == FileType.Fixed)
            {
                footer.UpdateChecksum();

                byte[] footerSector = new byte[Sizes.Sector];
                footer.ToBytes(footerSector, 0);

                SparseStream footerStream = SparseStream.FromStream(new MemoryStream(footerSector, false), Ownership.None);
                Stream imageStream = new ConcatStream(Ownership.None, Content, footerStream);
                fileSpecs.Add(new DiskImageFileSpecification(baseName + ".vhd", new PassthroughStreamBuilder(imageStream)));
            }
            else if (_diskType == FileType.Dynamic)
            {
                fileSpecs.Add(new DiskImageFileSpecification(baseName + ".vhd",  new DynamicDiskBuilder(Content, footer, (uint)Sizes.OneMiB * 2)));
            }
            else
            {
                throw new InvalidOperationException("Only Fixed and Dynamic disk types supported");
            }

            return fileSpecs.ToArray();
        }
    }
}
