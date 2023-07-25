namespace DiscUtils.Vfs
{
    using System.IO;

    /// <summary>
    /// Delegate for instantiating a file system.
    /// </summary>
    /// <param name="stream">The stream containing the file system</param>
    /// <param name="volumeInfo">Optional, information about the volume the file system is on</param>
    /// <param name="parameters">Parameters for the file system</param>
    /// <returns>A file system implementation</returns>
    public delegate DiscFileSystem VfsFileSystemOpener(Stream stream, VolumeInfo volumeInfo, FileSystemParameters parameters);

    /// <summary>
    /// Class holding information about a file system.
    /// </summary>
    public sealed class VfsFileSystemInfo : DiscUtils.FileSystemInfo
    {
        private string _name;
        private string _description;
        private VfsFileSystemOpener _openDelegate;

        /// <summary>
        /// Initializes a new instance of the VfsFileSystemInfo class.
        /// </summary>
        /// <param name="name">The name of the file system</param>
        /// <param name="description">A one-line description of the file system</param>
        /// <param name="openDelegate">A delegate that can open streams as the indicated file system</param>
        public VfsFileSystemInfo(string name, string description, VfsFileSystemOpener openDelegate)
        {
            _name = name;
            _description = description;
            _openDelegate = openDelegate;
        }

        /// <summary>
        /// Gets the name of the file system.
        /// </summary>
        public override string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets a one-line description of the file system.
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Opens a volume using the file system.
        /// </summary>
        /// <param name="volume">The volume to access</param>
        /// <param name="parameters">Parameters for the file system</param>
        /// <returns>A file system instance</returns>
        public override DiscFileSystem Open(VolumeInfo volume, FileSystemParameters parameters)
        {
            return _openDelegate(volume.Open(), volume, parameters);
        }

        /// <summary>
        /// Opens a stream using the file system.
        /// </summary>
        /// <param name="stream">The stream to access</param>
        /// <param name="parameters">Parameters for the file system</param>
        /// <returns>A file system instance</returns>
        public override DiscFileSystem Open(Stream stream, FileSystemParameters parameters)
        {
            return _openDelegate(stream, null, parameters);
        }
    }
}
