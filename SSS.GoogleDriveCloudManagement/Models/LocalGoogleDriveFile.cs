using SSS.GoogleDriveCloudManagement.Models;

namespace SSS.GoogleDriveCloudService.Models
{
    public class LocalGoogleDriveFile : GoogleDriveFile
    {
        public string FilePath { get; set; }
        public string LocalId { get; set; }
        public string CloudId { get; set; }
        public string AppVersion { get; set; }
        public string DisplayName { get; set; }
    }
}