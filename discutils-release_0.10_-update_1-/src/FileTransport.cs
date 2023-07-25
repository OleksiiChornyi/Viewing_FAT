namespace DiscUtils
{
    using System;
    using System.Globalization;
    using System.IO;

    [VirtualDiskTransport("file")]
    internal sealed class FileTransport : VirtualDiskTransport
    {
        private string _path;
        private string _extraInfo;

        public override bool IsRawDisk
        {
            get { return false; }
        }

        public override void Connect(Uri uri, string username, string password)
        {
            _path = uri.LocalPath;
            _extraInfo = uri.Fragment.TrimStart('#');

            if (!Directory.Exists(Path.GetDirectoryName(_path)))
            {
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, "No such file '{0}'", uri.OriginalString), _path);
            }
        }

        public override VirtualDisk OpenDisk(FileAccess access)
        {
            throw new NotSupportedException();
        }

        public override FileLocator GetFileLocator()
        {
            return new LocalFileLocator(Path.GetDirectoryName(_path));
        }

        public override string GetFileName()
        {
            return Path.GetFileName(_path);
        }

        public override string GetExtraInfo()
        {
            return _extraInfo;
        }
    }
}
