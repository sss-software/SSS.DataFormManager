using System;
using System.Collections.Generic;

namespace SSS.EncryptionManagementService.Models
{
    /// <summary>
    /// Container to hold the result of an encryption of decryption request
    /// </summary>
    public class EncryptionResult
    {
        #region Constructors

        public EncryptionResult()
        {
            IsEncrypted = false;
            IsDecrypted = false;
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public List<string> Errors { get; }
        public List<Exception> Exceptions { get; }
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
        public string EncryptedToFilePath { get; set; }
        public string DecryptedToFilePath { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsDecrypted { get; set; }
        public bool IsEncryptedToFile { get; set; }
        public bool IsDecryptedToFile { get; set; }

        public bool IsEncryptionValid => (!string.IsNullOrEmpty(EncryptedText)) &&
                                         (IsEncrypted) &&
                                         (Errors != null) &&
                                         (Errors.Count == 0) &&
                                         (Exceptions != null) &&
                                         (Exceptions.Count == 0);

        public bool IsDecryptionValid => (!string.IsNullOrEmpty(DecryptedText)) &&
                                         (IsDecrypted) &&
                                         (Errors != null) &&
                                         (Errors.Count == 0) &&
                                         (Exceptions != null) &&
                                         (Exceptions.Count == 0);

        #endregion Properties
    }
}