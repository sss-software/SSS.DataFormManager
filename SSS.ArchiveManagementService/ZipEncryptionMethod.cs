namespace SSS.ArchiveManagementService
{
    public enum ZipEncryptionMethod
    {
        None = 0,
        PkZipWeak = 1,
        WinZipAes128 = 2,
        WinZipAes256 = 3,
        PkUnsupported = 4
    }
}