using System.Collections.Generic;

namespace SSS.ArchiveManagementService.Models
{
    public class ZipFileSetting
    {
        public string Password { get; set; }
        public string DirectoryPathInArchive { get; set; }
        public string SaveToPath { get; set; }
        public ZipEncryptionMethod EncryptionMethod { get; set; }
        public IEnumerable<ZipEntrySetting> ZipEntries { get; set; }

        public void AddReadMeFile(ZipEntrySetting readMeZipSetting)
        {
            this.ReadMeZipEntrySetting = readMeZipSetting;
        }

        public bool CanAddReadMe() => (ReadMeZipEntrySetting != null);

        public ZipEntrySetting ReadMeZipEntrySetting { get; set; }
    }
}