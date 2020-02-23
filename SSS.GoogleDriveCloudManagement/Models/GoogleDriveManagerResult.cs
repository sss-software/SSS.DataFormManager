using SSS.GoogleDriveCloudManagement.Models;
using System;
using System.Collections.Generic;

namespace SSS.GoogleDriveCloudService.Models
{
    public class GoogleDriveManagerResult
    {
        #region Constructors

        public GoogleDriveManagerResult()
        {
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public List<string> Errors { get; }
        public List<Exception> Exceptions { get; }
        public List<GoogleDriveFile> GoogleDriveFiles { get; set; }
        public List<LocalGoogleDriveFile> LocalGoogleDriveFiles { get; set; }
        public GoogleDriveFile GoogleDriveFile { get; set; }
        public string GoogleDriveFileId { get; set; }
        public string GoogleDriveStatus { get; set; }
        public string GoogleDriveFilePath { get; set; }
        public bool IsGoogleDriveFileUpdated { get; set; }
        public bool IsGoogleDriveFileTrashed { get; set; }
        public bool IsGoogleDriveFileUnTrashed { get; set; }
        public bool IsGoogleDriveFileUploaded { get; set; }

        public bool IsValidGoogleDriveManagerResult => (Errors != null) &&
                                                       (Errors.Count == 0) &&
                                                       (Exceptions != null) &&
                                                       (Exceptions.Count == 0);

        #endregion Properties
    }
}