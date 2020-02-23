using Ionic.Zip;
using SSS.ArchiveManagementService.Consoles;
using SSS.ArchiveManagementService.Models;
using System;

namespace SSS.ArchiveManagementService
{
    public class ZipManager : IZipManager
    {
        #region fields

        internal ZipManagerResult result;

        #endregion fields

        #region Methods

        public ZipManagerResult CreateZipArchive(ZipFileSetting setting)
        {
            result = new ZipManagerResult();
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    if (setting.CanAddReadMe())
                    {
                        zip.AddFile(setting.ReadMeZipEntrySetting.FilePath, setting.ReadMeZipEntrySetting.DirectoryPathInArchive);
                    }

                    zip.Encryption = ZipEncryptionMethodConversionService.GetZipEnriptionAlgorithm(setting.EncryptionMethod);
                    zip.Password = setting.Password;
                    foreach (ZipEntrySetting zipEntry in setting.ZipEntries)
                    {
                        zip.AddFile(zipEntry.FilePath, zipEntry.DirectoryPathInArchive);
                    }

                    zip.Save(setting.SaveToPath);
                    result.IsZipFileCreated = true;
                }
                return result;
            }
            catch (Exception e)
            {
                result.IsZipFileCreated = false;
                result.Exceptions.Add(e);
                return result;
            }
        }

        private ZipManagerResult UnzipArchive(ZipExtractionSetting extractionSetting)
        {
            result = new ZipManagerResult();
            try
            {
                using (ZipFile zip = ZipFile.Read(extractionSetting.ZipFilePath))
                {
                    if (extractionSetting.IsPasswordProtected)
                    {
                        zip.Password = extractionSetting.Password;
                    }
                    zip.ExtractAll(extractionSetting.ExtractionDirectory, ExtractExistingFileAction.DoNotOverwrite);
                }
                WriteToConsole(ZipManagerServiceConstants.SuccessfulExtractionMessage);
                result.IsZipFileExtracted = true;
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                Console.WriteLine(ZipManagerServiceConstants.ExceptionExtractionMessage);
                return result;
            }
        }

        private void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }

        #endregion Methods
    }
}