using System;
using System.IO;

namespace SSS.GoogleDriveCloudManagement.Models
{
    public static class GoogleDriveConfig
    {
        static GoogleDriveConfig()
        {
            if (!Directory.Exists(DownloadsFolderPath))
            {
                Directory.CreateDirectory(DownloadsFolderPath);
            }
            if (!Directory.Exists(UserCredentialTokenFolderPath))
            {
                Directory.CreateDirectory(UserCredentialTokenFolderPath);
            }
            if (!Directory.Exists(UserCredentialSecretFolderPath))
            {
                Directory.CreateDirectory(UserCredentialSecretFolderPath);
            }
        }

        public static string DocumentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string ApplicationName = "Slambert Software Solutions";
        public static string DownloadsFolderPath = Path.Combine(DocumentsFolderPath, @"APP\SSS-DFM\Downloads-GoogleDrive");
        public static string UserCredentialFolderPath = DocumentsFolderPath;
        public static string UserCredentialTokenFolderPath = Path.Combine(DocumentsFolderPath, @"APP\SSS-DFM\Json");
        public static string UserCredentialTokenFilePath = Path.Combine(UserCredentialTokenFolderPath, "token.credentials");
        public static string UserCredentialSecretFolderPath = Path.Combine(DocumentsFolderPath, @"APP\SSS-DFM\Secret");
        public static string UserCredentialSecretFilePath = Path.Combine(UserCredentialSecretFolderPath, @"credentials.json");
        public static string UserName = Environment.UserName;
    }
}