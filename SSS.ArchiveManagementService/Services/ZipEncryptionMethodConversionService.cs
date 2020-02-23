using Ionic.Zip;

namespace SSS.ArchiveManagementService
{
    public static class ZipEncryptionMethodConversionService
    {
        public static EncryptionAlgorithm GetZipEnriptionAlgorithm(ZipEncryptionMethod encryptionMethod)
        {
            switch (encryptionMethod)
            {
                case ZipEncryptionMethod.None:
                    return EncryptionAlgorithm.None;

                case ZipEncryptionMethod.PkZipWeak:
                    return EncryptionAlgorithm.PkzipWeak;

                case ZipEncryptionMethod.WinZipAes128:
                    return EncryptionAlgorithm.WinZipAes128;

                case ZipEncryptionMethod.WinZipAes256:
                    return EncryptionAlgorithm.WinZipAes256;

                case ZipEncryptionMethod.PkUnsupported:
                    return EncryptionAlgorithm.Unsupported;

                default:
                    return EncryptionAlgorithm.None;
            }
        }
    }
}