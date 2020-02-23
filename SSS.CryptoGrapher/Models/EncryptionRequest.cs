using System.Text;

namespace SSS.EncryptionManagementService.Models
{
    /// <summary>
    /// Container to hold the parameters of an encryption request
    /// </summary>
    public class EncryptionRequest
    {
        /// <summary>
        /// The file path of the original unencrypted file.
        /// </summary>
        public string OriginalFilePath { get; set; }

        /// <summary>
        /// The file path of the encrypted file.
        /// </summary>
        public string EncryptToFilePath { get; set; }

        /// <summary>
        /// The string content of the encrypted file
        /// </summary>
        public string EncryptedText { get; set; }

        /// <summary>
        /// The string encoding of the contents of the unencrypted file
        /// </summary>
        public Encoding TextEncoding { get; set; }

        /// <summary>
        /// The file path of the decrypted file.
        /// </summary>
        public string DecryptToFilePath { get; set; }

        /// <summary>
        /// The string content of the decrypted file
        /// </summary>
        public string DecryptedText { get; set; }
    }
}