namespace DiscUtils
{
    using System;
    using System.IO;

    internal sealed class LocalFileLocator : FileLocator
    {
        private string _dir;

        public LocalFileLocator(string dir)
        {
            _dir = dir;
        }

        public override bool Exists(string fileName)
        {
            return File.Exists(Path.Combine(_dir, fileName));
        }

        public override Stream Open(string fileName, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStream(Path.Combine(_dir, fileName), mode, access, share);
        }

        public override FileLocator GetRelativeLocator(string path)
        {
            return new LocalFileLocator(Path.Combine(_dir, path));
        }

        public override string GetFullPath(string path)
        {
            string combinedPath = Path.Combine(_dir, path);
            if (string.IsNullOrEmpty(combinedPath))
            {
                return Environment.CurrentDirectory;
            }
            else
            {
                return Path.GetFullPath(combinedPath);
            }
        }

        public override string GetDirectoryFromPath(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public override string GetFileFromPath(string path)
        {
            return Path.GetFileName(path);
        }

        public override DateTime GetLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(Path.Combine(_dir, path));
        }

        public override bool HasCommonRoot(FileLocator other)
        {
            LocalFileLocator otherLocal = other as LocalFileLocator;
            if (otherLocal == null)
            {
                return false;
            }

            // If the paths have drive specifiers, then common root depends on them having a common
            // drive letter.
            string otherDir = otherLocal._dir;
            if (otherDir.Length >= 2 && _dir.Length >= 2)
            {
                if (otherDir[1] == ':' && _dir[1] == ':')
                {
                    return Char.ToUpperInvariant(otherDir[0]) == Char.ToUpperInvariant(_dir[0]);
                }
            }

            return true;
        }

        public override string ResolveRelativePath(string path)
        {
            return Utilities.ResolveRelativePath(_dir, path);
        }
    }
}
