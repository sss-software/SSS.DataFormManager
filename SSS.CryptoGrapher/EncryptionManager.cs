using SSS.EncryptionManagementService.Constants;
using SSS.EncryptionManagementService.Models;
using SSS.FileManagementService;
using SSS.FileManagementService.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SSS.EncryptionManagementService
{
    public class EncryptionManager : IEncryptionManager
    {
        #region fields

        internal EncryptionResult result;
        private string _key;

        #endregion fields

        public EncryptionManager()
        {
            _key = EncryptionConfig.DefaultEncryptionKey;
        }

        public string Key
        {
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// Decrypt the given string using the default key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <returns>The container containing the decrypted string and validation results.</returns>
        public EncryptionResult Decrypt(string strEncrypted)
        {
            result = new EncryptionResult();
            try
            {
                result.DecryptedText = Decrypt(strEncrypted, _key);
                result.IsDecrypted = true;
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                result.IsDecrypted = false;
                WriteToConsole(EncryptionManagerConstants.InvalidInput + e.Message);
                return result;
            }
        }

        public EncryptionResult Encrypt(EncryptionRequest encryptionRequest)
        {
            result = new EncryptionResult();
            try
            {
                using (FileManager fileManager = new FileManager())
                {
                    FileManagerResult fileManagerResult = fileManager.GetStreamReaderText(encryptionRequest.OriginalFilePath, encryptionRequest.TextEncoding);
                    if (fileManagerResult.IsFileStreamTextValid)
                    {
                        if (string.IsNullOrEmpty(_key))
                        {
                            _key = EncryptionConfig.DefaultEncryptionKey;
                        }
                        result.EncryptedText = Encrypt(fileManagerResult.FileStreamText, _key);
                        result.EncryptedToFilePath = encryptionRequest.EncryptToFilePath;
                        if (!string.IsNullOrEmpty(encryptionRequest.EncryptToFilePath))
                        {
                            SaveEncryptionToFile(encryptionRequest.EncryptToFilePath, result.EncryptedText, encryptionRequest.TextEncoding);
                            result.IsEncrypted = true;
                        }
                    }
                    else
                    {
                        result.IsEncrypted = false;
                        result.Errors.Add(EncryptionManagerConstants.InvalidFileStream);
                        WriteToConsole(EncryptionManagerConstants.InvalidFileStream);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                result.IsEncrypted = false;
                result.Exceptions.Add(e);
                WriteToConsole(EncryptionManagerConstants.InvalidInput + e.Message);
                return result;
            }
        }

        /// <summary>
        /// Encrypt the given string using the default key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <returns>The container containing the encrypted string and validation results.</returns>
        public EncryptionResult Encrypt(string strToEncrypt)
        {
            result = new EncryptionResult();
            try
            {
                if (string.IsNullOrEmpty(_key))
                {
                    _key = EncryptionConfig.DefaultEncryptionKey;
                }
                result.EncryptedText = Encrypt(strToEncrypt, _key);
                result.IsEncrypted = true;
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                result.IsEncrypted = false;
                WriteToConsole(EncryptionManagerConstants.InvalidInput + e.Message);
                return result;
            }
        }

        public string SetEncryptionKey(string key = null)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }
            else
            {
                return EncryptionConfig.DefaultEncryptionKey;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        private string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = strKey;

                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB;

                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;

                return strDecrypted;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(EncryptionManagerConstants.InvalidInput + e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        private string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = strKey;

                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB;

                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                WriteToConsole(EncryptionManagerConstants.InvalidInput + e.Message);
                return string.Empty;
            }
        }

        private void SaveEncryptionToFile(string filePath, string content, Encoding encoding)
        {
            try
            {
                using (FileManager fileManager = new FileManager())
                {
                    FileManagerResult fmr = fileManager.SaveText(filePath, content, encoding);
                    if (fmr.IsFileWritten)
                    {
                        result.EncryptedToFilePath = filePath;
                        result.IsEncryptedToFile = true;
                    }
                };
            }
            catch (Exception e)
            {
                result.IsEncryptedToFile = false;
                result.Exceptions.Add(e);
                WriteToConsole(EncryptionManagerConstants.EncryptionToFileFailure + e.Message);
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