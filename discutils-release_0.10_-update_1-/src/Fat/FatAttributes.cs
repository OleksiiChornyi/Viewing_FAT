namespace DiscUtils.Fat
{
    using System;

    [Flags()]
    internal enum FatAttributes : byte
    {
        ReadOnly = 0x01,
        Hidden = 0x02,
        System = 0x04,
        VolumeId = 0x08,
        Directory = 0x10,
        Archive = 0x20,
    }
}
