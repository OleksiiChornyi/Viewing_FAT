namespace DiscUtils
{
    using System;
    using System.IO;

    internal abstract class VirtualDiskTransport : IDisposable
    {
        public abstract bool IsRawDisk { get; }

        public abstract void Connect(Uri uri, string username, string password);

        public abstract VirtualDisk OpenDisk(FileAccess access);

        public abstract FileLocator GetFileLocator();

        public abstract string GetFileName();

        public abstract string GetExtraInfo();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
