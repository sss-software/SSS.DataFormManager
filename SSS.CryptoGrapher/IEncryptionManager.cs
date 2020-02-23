using SSS.EncryptionManagementService.Models;

namespace SSS.EncryptionManagementService
{
    public interface IEncryptionManager
    {
        string Key { set; }

        EncryptionResult Decrypt(string strEncrypted);
        EncryptionResult Encrypt(EncryptionRequest encryptionRequest);
        EncryptionResult Encrypt(string strToEncrypt);
        string SetEncryptionKey(string key = null);
    }
}