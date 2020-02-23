namespace SSS.GoogleDriveCloudManagement.Constants
{
    public class GoogleDriveManagementConstants
    {
        public const string FileNotFound = "The specified file was not found: ";
        public const string FileOnDiskDoesNotExistError = "The specified file does not exist.";
        public const string FileNotUpdatedError = "The specified file was not updated on Google Drive.";
        public const string NoFilesFound = "No files found: ";
        public const string FolderNotFound = "The specified folder was not found: ";
        public const string FolderByNameNotFound = "The specified folder by given name was not found: ";
        public const string FolderNotCreated = "Folder could not be created. ";
        public const string CredentialFileCreated = "Credential file saved to: ";
        public const string FolderNotCreatedException = "Folder could not be created: ";
        public const string GettingFileListException = "An exception on retrieving file list from Google Drive: ";
        public const string GettingFileException = "An exception on getting file from Google Drive: ";
        public const string UploadingFileException = "An exception on uploading a file to Google Drive: ";
        public const string UploadingBatchFilesToFolderException = "An exception on uploading batch files to Google Drive Folder: ";
        public const string UpdateFileException = "An exception on updating a file on Google Drive: ";

        public const string TrashingFileException = "An exception trashing a Google Drive file: ";
        public const string TrashingFileError = "An error ocurred trashing a Google Drive file: ";

        public const string UnTrashingFileException = "An exception untrashing a Google Drive file: ";
        public const string UnTrashingFileError = "An error ocurred untrashing a Google Drive file: ";
        public const string FileNotOwnedByUserError = "File not owned by user. File cannot be trashed. ";

        public const string FieldsInPartialResponse = "nextPageToken, files(id, name, size, version, createdTime)";
        public const string FileIdReturned = "File ID: ";
        public const string FolderError = "Folder ID: ";
        public const string FileUploadedMessage = "File uploaded with ID: ";

        public const string GetFolderByIdException = "Exception getting folder by ID: ";
        public const string GetFolderByNameException = "Exception getting folder by Name: ";
        public const string DeleteFileException = "Exception on deleting Google Drive file: ";
        public const string DownloadFileException = "Exception on downloading Google Drive file: ";

        public const string DownloadCompleteStatus = "Download completed.";
        public const string DownloadFailedStatus = "Download failed.";
        public const string DownloadInProgressStatus = "Download in progress. Bytes downloaded: ";
        public const string SearchFileException = "An exception pccurred during searching of files: ";
    }
}