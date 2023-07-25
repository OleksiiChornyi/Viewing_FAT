namespace DiscUtils.Sdi
{
    using System;
    using System.IO;

    internal class FileHeader
    {
        public string Tag;
        public ulong Type;
        public ulong BootCodeOffset;
        public ulong BootCodeSize;
        public ulong VendorId;
        public ulong DeviceId;
        public Guid DeviceModel;
        public ulong DeviceRole;
        ////Reserved ulong
        public Guid RuntimeGuid;
        public ulong RuntimeOEMRev;
        ////Reserved ulong
        public long PageAlignment;
        public ulong Checksum;

        public void ReadFrom(byte[] buffer, int offset)
        {
            Tag = Utilities.BytesToString(buffer, offset, 8);

            if (Tag != "$SDI0001")
            {
                throw new InvalidDataException("SDI format marker not found");
            }

            Type = Utilities.ToUInt64LittleEndian(buffer, offset + 0x08);
            BootCodeOffset = Utilities.ToUInt64LittleEndian(buffer, offset + 0x10);
            BootCodeSize = Utilities.ToUInt64LittleEndian(buffer, offset + 0x18);
            VendorId = Utilities.ToUInt64LittleEndian(buffer, offset + 0x20);
            DeviceId = Utilities.ToUInt64LittleEndian(buffer, offset + 0x28);
            DeviceModel = Utilities.ToGuidLittleEndian(buffer, offset + 0x30);
            DeviceRole = Utilities.ToUInt64LittleEndian(buffer, offset + 0x40);
            RuntimeGuid = Utilities.ToGuidLittleEndian(buffer, offset + 0x50);
            RuntimeOEMRev = Utilities.ToUInt64LittleEndian(buffer, offset + 0x60);
            PageAlignment = Utilities.ToInt64LittleEndian(buffer, offset + 0x70);
            Checksum = Utilities.ToUInt64LittleEndian(buffer, offset + 0x1F8);
        }
    }
}
