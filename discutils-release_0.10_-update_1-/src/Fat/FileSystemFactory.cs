namespace DiscUtils.Fat
{
    using System.IO;
    using DiscUtils.Vfs;

    [VfsFileSystemFactory]
    internal class FileSystemFactory : VfsFileSystemFactory
    {
        public override DiscUtils.FileSystemInfo[] Detect(Stream stream, VolumeInfo volume)
        {
            if (FatFileSystem.Detect(stream))
            {
                return new DiscUtils.FileSystemInfo[] { new VfsFileSystemInfo("FAT", "Microsoft FAT", Open) };
            }

            return new DiscUtils.FileSystemInfo[0];
        }

        private DiscFileSystem Open(Stream stream, VolumeInfo volumeInfo, FileSystemParameters parameters)
        {
            return new FatFileSystem(stream, Ownership.None, parameters);
        }
    }
}
