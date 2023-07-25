namespace DiscUtils.Vfs
{
    using System;
    using System.IO;

    /// <summary>
    /// Interface implemented by a class representing a file.
    /// </summary>
    /// <remarks>
    /// File system implementations should have a class that implements this
    /// interface.  If the file system implementation is read-only, it is
    /// acceptable to throw <c>NotImplementedException</c> from setters.
    /// </remarks>
    public interface IVfsFile
    {
        /// <summary>
        /// Gets or sets the last access time in UTC.
        /// </summary>
        DateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the last write time in UTC.
        /// </summary>
        DateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the last creation time in UTC.
        /// </summary>
        DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the file's attributes.
        /// </summary>
        FileAttributes FileAttributes { get; set; }

        /// <summary>
        /// Gets the length of the file.
        /// </summary>
        long FileLength { get; }

        /// <summary>
        /// Gets a buffer to access the file's contents.
        /// </summary>
        IBuffer FileContent { get; }
    }

    /// <summary>
    /// Interface implemented by classes representing files, in file systems that support multi-stream files.
    /// </summary>
    public interface IVfsFileWithStreams : IVfsFile
    {
        /// <summary>
        /// Creates a new stream.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <returns>An object representing the stream.</returns>
        SparseStream CreateStream(string name);

        /// <summary>
        /// Opens an existing stream.
        /// </summary>
        /// <param name="name">The name of the stream.</param>
        /// <returns>An object representing the stream.</returns>
        /// <remarks>The implementation must not implicitly create the stream if it doesn't already
        /// exist.</remarks>
        SparseStream OpenExistingStream(string name);
    }
}
