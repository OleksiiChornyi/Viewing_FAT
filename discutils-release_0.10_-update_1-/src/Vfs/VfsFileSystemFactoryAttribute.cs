namespace DiscUtils.Vfs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Attribute identifying file system factory classes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class VfsFileSystemFactoryAttribute : Attribute
    {
    }
}
