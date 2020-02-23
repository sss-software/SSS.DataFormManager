namespace SSS.ArchiveManagementService.Models
{
    public class ZipExtractionSetting
    {
        public string ZipFilePath { get; set; }
        public string ExtractionDirectory { get; set; }
        public string Password { get; set; }
        public bool IsPasswordProtected { get; set; }
    }
}