using System.IO;
using System.Text;
using SSS.FileManagementService.Models;

namespace SSS.FileManagementService
{
    public interface IFileManager
    {
        FileManagerResult GetFileStream(string filePath);
        FileManagerResult GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess);
        FileManagerResult GetStreamReaderText(string filePath, Encoding encoding);
        FileManagerResult SaveText(string filePath, string content, Encoding encoding);
    }
}