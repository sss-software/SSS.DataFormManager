using System;
using System.Collections.Generic;

namespace SSS.ArchiveManagementService.Models
{
    public class ZipManagerResult
    {
        #region Constructors

        public ZipManagerResult()
        {
            IsZipFileCreated = false;
            IsZipFileExtracted = false;
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public List<string> Errors { get; }
        public List<Exception> Exceptions { get; }
        public bool IsZipFileCreated { get; set; }
        public bool IsZipFileExtracted { get; set; }
        public string ZipMessage { get; set; }

        #endregion Properties
    }
}