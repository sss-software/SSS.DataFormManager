using Google.Apis.Download;
using Google.Apis.Drive.v3;
using SSS.GoogleDriveCloudManagement.Constants;
using SSS.GoogleDriveCloudManagement.Models;
using SSS.GoogleDriveCloudManagement.Services;
using SSS.GoogleDriveCloudService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SSS.GoogleDriveCloudService
{
    public class GoogleDriveCloudManager : IGoogleDriveCloudManager
    {
        #region fields

        internal GoogleDriveManagerResult result;

        #endregion fields

        /// <summary>
        /// Create a new Directory file on Google Drive: GoogleDriveService.Files.Create(metaData)
        /// </summary>
        /// a Valid authenticated DriveService
        /// The title of the file. Used to identify file or folder name.
        /// A short description of the file.
        /// Collection of parent folders which contain this file.
        /// Setting this field will put the file in all of the provided folders. root folder.
        /// Documentation: https://developers.google.com/drive/v3/reference/files/create
        public GoogleDriveManagerResult CreateFolder(DriveService service, string dirName, string description, string parent)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File folder = null;
                Google.Apis.Drive.v3.Data.File metaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = dirName,
                    Description = description,
                    MimeType = GoogleDriveMimeService.FolderMimeType
                };
                FilesResource.CreateRequest request = service.Files.Create(metaData);
                folder = request.Execute();
                if (folder != null)
                {
                    result.GoogleDriveFileId = folder.Id;
                }
                else
                {
                    result.Errors.Add(GoogleDriveManagementConstants.FolderNotCreated);
                }
                return result;
            }
            catch (Exception e)
            {
                WriteToConsole(GoogleDriveManagementConstants.FolderNotCreatedException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        public GoogleDriveManagerResult DeleteFile(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                FilesResource.DeleteRequest request = service.Files.Delete(fileId);
                result.GoogleDriveStatus = request.Execute();
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.DeleteFileException + e.Message);
                return result;
            }
        }

        public GoogleDriveManagerResult DownloadFile(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                FilesResource.GetRequest request = service.Files.Get(fileId);
                Google.Apis.Drive.v3.Data.File response = request.Execute();

                string FileName = response.Name;
                result.GoogleDriveFilePath = Path.Combine(GoogleDriveConfig.DownloadsFolderPath, FileName);

                MemoryStream stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                WriteToConsole(GoogleDriveManagementConstants.DownloadInProgressStatus + progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                WriteToConsole(GoogleDriveManagementConstants.DownloadCompleteStatus);
                                SaveStream(stream, result.GoogleDriveFilePath);
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                WriteToConsole(GoogleDriveManagementConstants.DownloadFailedStatus);
                                break;
                            }
                    }
                };
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                request.DownloadWithStatus(stream);
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.DownloadFileException + e.Message);
                return result;
            }
        }

        public GoogleDriveManagerResult GetFile(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                FilesResource.GetRequest request = service.Files.Get(fileId);
                Google.Apis.Drive.v3.Data.File file = request.Execute();
                if (file != null)
                {
                    GoogleDriveFile gdf = new GoogleDriveFile()
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime
                    };
                    result.GoogleDriveFile = gdf;
                }
                else
                {
                    WriteToConsole(GoogleDriveManagementConstants.FileNotFound + fileId);
                    result.Errors.Add(GoogleDriveManagementConstants.FileNotFound);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.GettingFileException + fileId);
                return result;
            }
        }

        /// <summary>
        /// Get folder by Id
        /// </summary>
        /// <param name="service"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult GetFolderById(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File response = null;
                List<Google.Apis.Drive.v3.Data.File> folders = SearchFilesWithNativeResult(service, GetSearchString(GoogleDriveEnumerators.FoldersOnly)).ToList();
                if (folders != null)
                {
                    response = folders.Where(f => f.Id == fileId).FirstOrDefault();
                    if (response != null)
                    {
                        GoogleDriveFile gdf = new GoogleDriveFile()
                        {
                            Id = response.Id,
                            Name = response.Name,
                            Size = response.Size,
                            Version = response.Version,
                            CreatedTime = response.CreatedTime
                        };
                        result.GoogleDriveFile = gdf;
                    }
                }
                else
                {
                    result.GoogleDriveFile = null;
                    result.Errors.Add(GoogleDriveManagementConstants.FolderNotFound);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.GetFolderByIdException + fileId);
                return result;
            }
        }

        /// <summary>
        /// Get folder by Name
        /// </summary>
        /// <param name="service"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult GetFolderByName(DriveService service, string folderName)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File response = null;
                List<Google.Apis.Drive.v3.Data.File> files = SearchFilesWithNativeResult(service, GetSearchString(GoogleDriveEnumerators.FoldersOnly)).ToList();
                if (files != null)
                {
                    response = files.Where(f => f.Name == folderName).FirstOrDefault();
                    if (response != null)
                    {
                        GoogleDriveFile gdf = new GoogleDriveFile()
                        {
                            Id = response.Id,
                            Name = response.Name,
                            Size = response.Size,
                            Version = response.Version,
                            CreatedTime = response.CreatedTime
                        };
                        result.GoogleDriveFile = gdf;
                    }
                }
                else
                {
                    result.GoogleDriveFile = null;
                    result.Errors.Add(GoogleDriveManagementConstants.FolderByNameNotFound);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.GetFolderByNameException + e.Message);
                return result;
            }
        }

        /// <summary>
        /// Get all files from Google Drive.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult GetGoogleDriveFiles(DriveService service)
        {
            try
            {
                result = new GoogleDriveManagerResult();
                FilesResource.ListRequest fileListRequest = service.Files.List();
                fileListRequest.PageSize = 10;
                fileListRequest.Fields = GoogleDriveManagementConstants.FieldsInPartialResponse;
                IList<Google.Apis.Drive.v3.Data.File> files = fileListRequest.Execute().Files;
                List<GoogleDriveFile> fileList = new List<GoogleDriveFile>();

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        GoogleDriveFile File = new GoogleDriveFile
                        {
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime
                        };
                        fileList.Add(File);
                    }
                    result.GoogleDriveFiles = fileList;
                }
                else
                {
                    result.GoogleDriveFiles = null;
                    result.Errors.Add(GoogleDriveManagementConstants.NoFilesFound);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                Console.WriteLine(GoogleDriveManagementConstants.GettingFileListException + e.Message);
                return result;
            }
        }

        public GoogleDriveManagerResult SearchFiles(DriveService service, string searchKey)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                FilesResource.ListRequest fileListRequest = service.Files.List();
                fileListRequest.PageSize = 10;
                fileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";
                fileListRequest.Q = searchKey;
                IList<Google.Apis.Drive.v3.Data.File> files = fileListRequest.Execute().Files;
                List<GoogleDriveFile> fileList = new List<GoogleDriveFile>();

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        GoogleDriveFile File = new GoogleDriveFile
                        {
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime
                        };
                        fileList.Add(File);
                    }
                    result.GoogleDriveFiles = fileList;
                }
                else
                {
                    result.GoogleDriveFiles = null;
                    result.Errors.Add(GoogleDriveManagementConstants.NoFilesFound);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.SearchFileException + e.Message);
                return result;
            }
        }

        /// <summary>
        /// Trash a file on Google Drive: GooglDriveService.Files.Trash
        /// Instead of permanently deleting a file you can simply trash it.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult TrashFile(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File response = null;
                Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File();
                if ((bool)file.OwnedByMe)
                {
                    file.Trashed = true;
                    FilesResource.UpdateRequest request = service.Files.Update(file, fileId);
                    response = request.Execute();
                    if (response != null)
                    {
                        result.GoogleDriveFileId = response.Id;
                    }
                    else
                    {
                        result.Errors.Add(GoogleDriveManagementConstants.FileNotOwnedByUserError);
                    }
                }
                else
                {
                    result.Errors.Add(GoogleDriveManagementConstants.FileNotOwnedByUserError);
                }
                return result;
            }
            catch (Exception e)
            {
                WriteToConsole(GoogleDriveManagementConstants.TrashingFileException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        /// <summary>
        /// Untrash a file on Google Drive: GooglDriveService.Files.Updaate
        /// Instead of permanently deleting a file you can simply trash it.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult UnTrashFile(DriveService service, string fileId)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File response = null;
                Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File();
                if ((bool)file.OwnedByMe)
                {
                    file.Trashed = false;
                    FilesResource.UpdateRequest request = service.Files.Update(file, fileId);
                    response = request.Execute();
                    if (response != null)
                    {
                        result.GoogleDriveFileId = response.Id;
                    }
                    else
                    {
                        result.Errors.Add(GoogleDriveManagementConstants.UnTrashingFileError);
                    }
                }
                else
                {
                    result.Errors.Add(GoogleDriveManagementConstants.FileNotOwnedByUserError);
                }
                return result;
            }
            catch (Exception e)
            {
                WriteToConsole(GoogleDriveManagementConstants.UnTrashingFileException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        /// <summary>
        /// Updates a file on Google Drive: GooglDriveService.Files.Update
        /// Requirements: A Valid authenticated DriveService path to the file to upload
        /// If upload succeeded returns the File resource of the uploaded file
        /// If the upload fails returns null
        /// </summary>
        /// <param name="service"></param>
        /// <param name="uploadFile"></param>
        /// <param name="parentId"></param>
        /// <param name="fileId">The resource id for the file we would like to update</param>
        /// <param name="fileDescription"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult UpdateFile(DriveService service, string uploadFile, string parentId,
                                                                string fileId, string fileDescription)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                if (System.IO.File.Exists(uploadFile))
                {
                    var mimeType = GoogleDriveMimeService.GetFileMimeType(uploadFile);
                    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = System.IO.Path.GetFileName(uploadFile),
                        Description = fileDescription,
                        MimeType = mimeType,
                        Parents = new List<string> { parentId }
                    };
                    byte[] byteArray = System.IO.File.ReadAllBytes(uploadFile);
                    MemoryStream stream = new MemoryStream(byteArray);
                    FilesResource.UpdateMediaUpload request = service.Files.Update(body, fileId, stream, mimeType);
                    request.ProgressChanged += Request_ProgressChanged;
                    request.ResponseReceived += Request_ResponseReceived;
                    request.Upload();
                    Google.Apis.Drive.v3.Data.File response = request.ResponseBody;
                    if (response != null)
                    {
                        result.GoogleDriveFileId = response.Id;
                        result.IsGoogleDriveFileUpdated = true;
                    }
                    else
                    {
                        result.IsGoogleDriveFileUpdated = false;
                        result.Errors.Add(GoogleDriveManagementConstants.FileNotUpdatedError);
                    }
                }
                else
                {
                    result.Errors.Add(GoogleDriveManagementConstants.FileOnDiskDoesNotExistError);
                }
                return result;
            }
            catch (Exception e)
            {
                WriteToConsole(GoogleDriveManagementConstants.UpdateFileException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        public bool UploadBatchFilesToFolder(DriveService service, string folderId, List<string> fileIds)
        {
            bool result = false;
            return result;
        }

        /// <summary>
        /// Upload a batch load of files in a google drive folder
        /// </summary>
        /// <param name="service"></param>
        /// <param name="folder"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public GoogleDriveManagerResult UploadBatchFilesToFolder(DriveService service, GoogleDriveFile folder, List<LocalGoogleDriveFile> files)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                GoogleDriveManagerResult googleDriveManagerResult = GetFolderById(service, folder.Id);

                if ((googleDriveManagerResult != null) && (googleDriveManagerResult.IsValidGoogleDriveManagerResult))
                {
                    List<LocalGoogleDriveFile> uploadedLocalGoogleDriveFiles = new List<LocalGoogleDriveFile>();
                    var parents = new List<string>();
                    parents.Add(googleDriveManagerResult.GoogleDriveFileId);
                    foreach (LocalGoogleDriveFile localGoogleDriveFile in files)
                    {
                        FilesResource.CreateMediaUpload request;
                        using (var stream = new System.IO.FileStream(localGoogleDriveFile.FilePath, System.IO.FileMode.Open))
                        {
                            var fileMetadata = GetFileMetaData(localGoogleDriveFile, parents);
                            request = service.Files.Create(fileMetadata, stream, GoogleDriveMimeService.GetFileMimeType(localGoogleDriveFile.FilePath));
                            request.Fields = "id";
                            request.Upload();
                        }
                        var uploadedFile = request.ResponseBody;
                        localGoogleDriveFile.CloudId = uploadedFile.Id;
                        uploadedLocalGoogleDriveFiles.Add(localGoogleDriveFile);
                        WriteToConsole(GoogleDriveManagementConstants.FileUploadedMessage + uploadedFile.Id);
                    }
                    result.LocalGoogleDriveFiles = uploadedLocalGoogleDriveFiles;
                }
                else
                {
                    WriteToConsole(GoogleDriveManagementConstants.FolderNotFound + folder.Id);
                    result.Errors.Add(GoogleDriveManagementConstants.FolderNotFound + folder.Id);
                }
                return result;
            }
            catch (Exception e)
            {
                WriteToConsole(GoogleDriveManagementConstants.UploadingBatchFilesToFolderException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        /// <summary>
        /// Creates a file on Google Drive : GoogleDriveService.Files.Create
        /// </summary>
        /// <param name="service"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        ///
        public GoogleDriveManagerResult UploadFile(DriveService service, LocalGoogleDriveFile localGoogleDriveFile, string folderId = null)
        {
            result = new GoogleDriveManagerResult();
            try
            {
                Google.Apis.Drive.v3.Data.File uploadedFile = null;
                Google.Apis.Drive.v3.Data.File fileMetaData = null;
                FilesResource.CreateMediaUpload request;
                List<string> parents = null;
                if (folderId != null)
                {
                    parents = new List<string> { folderId };
                    fileMetaData = GetFileMetaData(localGoogleDriveFile, parents);
                }
                else
                {
                    fileMetaData = GetFileMetaData(localGoogleDriveFile);
                }
                using (var fileStream = new System.IO.FileStream(localGoogleDriveFile.FilePath, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(fileMetaData, fileStream, fileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                uploadedFile = request.ResponseBody;
                WriteToConsole(GoogleDriveManagementConstants.FileIdReturned + uploadedFile.Id);
                result.GoogleDriveFileId = uploadedFile.Id;
                return result;
            }
            catch (Exception e)
            {
                result.GoogleDriveFileId = string.Empty;
                Console.WriteLine(GoogleDriveManagementConstants.GettingFileListException + e.Message);
                result.Exceptions.Add(e);
                return result;
            }
        }

        public bool UploadFileToFolder(DriveService service, string folderId, string fileId)
        {
            bool result = false;
            return result;
        }

        public bool UploadFileToFolder(DriveService service, Google.Apis.Drive.v3.Data.File folder, Google.Apis.Drive.v3.Data.File file)
        {
            bool result = false;
            return result;
        }

        private bool DoesFolderExist(DriveService service, string folderId)
        {
            bool exists = false;

            return exists;
        }

        private Google.Apis.Drive.v3.Data.File GetFileMetaData(LocalGoogleDriveFile localGoogleDriveFile, IList<string> parents = null)
        {
            var fileMetaData = new Google.Apis.Drive.v3.Data.File();
            fileMetaData.Name = Path.GetFileName(Path.GetFileName(localGoogleDriveFile.FilePath));
            if (parents != null)
            {
                fileMetaData.Parents = parents;
            }
            var mimeType = System.Web.MimeMapping.GetMimeMapping(localGoogleDriveFile.FilePath);
            if (string.IsNullOrEmpty(mimeType))
            {
                fileMetaData.MimeType = GoogleDriveMimeService.GetFileMimeType(localGoogleDriveFile.FilePath);
            }
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("LocalId", localGoogleDriveFile.LocalId);
            properties.Add("AppVersion", localGoogleDriveFile.AppVersion);
            properties.Add("DisplayName", localGoogleDriveFile.DisplayName);
            fileMetaData.AppProperties = properties;
            return fileMetaData;
        }

        private string GetSearchString(GoogleDriveEnumerators searchBy)
        {
            string searchString = string.Empty;
            switch (searchBy)
            {
                case GoogleDriveEnumerators.FoldersOnly:
                    searchString = @"mimeType = 'application/vnd.google-apps.folder'";
                    break;

                case GoogleDriveEnumerators.FilesOnly:
                    searchString = @"mimeType != 'application/vnd.google-apps.folder'";
                    break;

                default:
                    break;
            }
            return searchString;
        }

        private void Request_ProgressChanged(Google.Apis.Upload.IUploadProgress obj)
        {
            if (obj != null)
            {
                //var response += obj.Status + " " + obj.BytesSent;
            }
        }

        private void Request_ResponseReceived(Google.Apis.Drive.v3.Data.File obj)
        {
            if (obj != null)
            {
                // MessageBox.Show("File was uploaded sucessfully--" + obj.Id);
            }
        }

        private void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        private IList<Google.Apis.Drive.v3.Data.File> SearchFilesWithNativeResult(DriveService service, string searchKey)
        {
            try
            {
                FilesResource.ListRequest fileListRequest = service.Files.List();
                fileListRequest.PageSize = 10;
                fileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";
                fileListRequest.Q = searchKey;
                IList<Google.Apis.Drive.v3.Data.File> files = fileListRequest.Execute().Files;
                return files.ToList();
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(GoogleDriveManagementConstants.SearchFileException + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Write a message to the output console
        /// </summary>
        /// <param name="message"></param>
        private void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}