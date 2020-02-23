using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SSS.GoogleDriveCloudManagement.Constants;
using SSS.GoogleDriveCloudManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SSS.GoogleDriveCloudService.Services
{
    public static class GoogleDriveAccessService
    {
        #region Constructors

        static GoogleDriveAccessService()
        {
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public static List<string> Errors { get; private set; }
        public static List<Exception> Exceptions { get; private set; }

        #endregion Properties

        private static string[] Scopes = {
                                            DriveService.Scope.Drive,
                                            DriveService.Scope.DriveFile
                                         };

        public static bool CanConnectToGoogleDrive { get; private set; }
        public static DriveService DriveService { get; private set; }

        public static bool IsValidGoogleDrivService => (DriveService != null) &&
                                                      (Errors != null) &&
                                                      (Errors.Count == 0) &&
                                                      (Exceptions != null) &&
                                                      (Exceptions.Count == 0);

        public static bool IsValidUserCredentials => (UserCredential != null) &&
                                                      (Errors != null) &&
                                                      (Errors.Count == 0) &&
                                                      (Exceptions != null) &&
                                                      (Exceptions.Count == 0);

        public static UserCredential UserCredential { get; private set; }

        public static bool IsAuthenticated()
        {
            UserCredential = GetUserCredential();
            if (IsValidUserCredentials)
            {
                DriveService = GetGoogleDriveService(UserCredential);
                if (IsValidGoogleDrivService)
                {
                    CanConnectToGoogleDrive = true;
                    return true;
                }
                else
                {
                    CanConnectToGoogleDrive = false;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create and return the Google Drive API Service
        /// </summary>
        /// <returns></returns>
        private static DriveService GetGoogleDriveService(UserCredential credential)
        {
            DriveService service = null;
            try
            {
                service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = GoogleDriveConfig.ApplicationName,
                });
                return service;
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                return service;
            }
        }

        private static UserCredential GetUserCredential()
        {
            UserCredential credential = null;
            try
            {
                using (var stream = new FileStream(GoogleDriveConfig.UserCredentialSecretFilePath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                                                                             Scopes,
                                                                             GoogleDriveConfig.UserName,
                                                                             CancellationToken.None,
                                                                             new FileDataStore(GoogleDriveConfig.UserCredentialTokenFilePath, true)).Result;
                    WriteToConsole(GoogleDriveManagementConstants.CredentialFileCreated + GoogleDriveConfig.UserCredentialTokenFilePath);
                }
                return credential;
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                return credential;
            }
        }

        /// <summary>
        /// Write a message to the output console
        /// </summary>
        /// <param name="message"></param>
        private static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}