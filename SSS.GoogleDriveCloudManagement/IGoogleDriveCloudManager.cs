using System.Collections.Generic;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using SSS.GoogleDriveCloudManagement.Models;
using SSS.GoogleDriveCloudService.Models;

namespace SSS.GoogleDriveCloudService
{
    public interface IGoogleDriveCloudManager
    {
        GoogleDriveManagerResult CreateFolder(DriveService service, string dirName, string description, string parent);
        GoogleDriveManagerResult DeleteFile(DriveService service, string fileId);
        GoogleDriveManagerResult DownloadFile(DriveService service, string fileId);
        GoogleDriveManagerResult GetFile(DriveService service, string fileId);
        GoogleDriveManagerResult GetFolderById(DriveService service, string fileId);
        GoogleDriveManagerResult GetFolderByName(DriveService service, string folderName);
        GoogleDriveManagerResult GetGoogleDriveFiles(DriveService service);
        GoogleDriveManagerResult SearchFiles(DriveService service, string searchKey);
        GoogleDriveManagerResult TrashFile(DriveService service, string fileId);
        GoogleDriveManagerResult UnTrashFile(DriveService service, string fileId);
        GoogleDriveManagerResult UpdateFile(DriveService service, string uploadFile, string parentId, string fileId, string fileDescription);
        GoogleDriveManagerResult UploadBatchFilesToFolder(DriveService service, GoogleDriveFile folder, List<LocalGoogleDriveFile> files);
        bool UploadBatchFilesToFolder(DriveService service, string folderId, List<string> fileIds);
        GoogleDriveManagerResult UploadFile(DriveService service, LocalGoogleDriveFile localGoogleDriveFile, string folderId = null);
        bool UploadFileToFolder(DriveService service, File folder, File file);
        bool UploadFileToFolder(DriveService service, string folderId, string fileId);
    }
}