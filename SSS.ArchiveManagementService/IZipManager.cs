using SSS.ArchiveManagementService.Models;

namespace SSS.ArchiveManagementService
{
    public interface IZipManager
    {
        ZipManagerResult CreateZipArchive(ZipFileSetting setting);
    }
}