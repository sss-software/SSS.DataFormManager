using System.ComponentModel;

public enum GoogleDriveEnumerators
{
    [Description("Folders that are Google apps or have the folder MIME type")]
    FoldersOnly = 1,

    [Description("Files that are not folders")]
    FilesOnly = 2
}