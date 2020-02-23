using System;
using System.Collections.Generic;
using System.IO;

namespace SSS.FileManagementService.Models
{
    /// <summary>
    /// Container to hold the result of a file manager operation
    /// </summary>
    public class FileManagerResult
    {
        #region Constructors

        public FileManagerResult()
        {
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public FileStream FileStreamObj { get; set; }
        public string FileStreamText { get; set; }
        public List<string> Errors { get; }
        public List<Exception> Exceptions { get; }

        public bool IsFileStreamValid => (FileStreamObj != null) &&
                                         (Errors != null) &&
                                         (Errors.Count == 0) &&
                                         (Exceptions != null) &&
                                         (Exceptions.Count == 0);

        public bool IsFileStreamTextValid => (!string.IsNullOrEmpty(FileStreamText)) &&
                                             (Errors != null) &&
                                             (Errors.Count == 0) &&
                                             (Exceptions != null) &&
                                             (Exceptions.Count == 0);

        public bool IsFileWritten { get; set; }

        #endregion Properties
    }
}