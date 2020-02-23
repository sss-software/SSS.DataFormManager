using SSS.FileManagementService.Constants;
using SSS.FileManagementService.Models;
using System;
using System.IO;
using System.Text;

namespace SSS.FileManagementService
{
    public class FileManager : IDisposable, IFileManager
    {
        #region fields

        internal FileManagerResult result;

        #endregion fields

        public FileManagerResult GetFileStream(string filePath)
        {
            result = new FileManagerResult();
            try
            {
                if (File.Exists(filePath))
                {
                    result.FileStreamObj = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                }
                else
                {
                    result.Errors.Add(FileManagerServiceConstants.FilePathDoesNotExist);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        public FileManagerResult GetFileStream(string filePath, FileMode fileMode, FileAccess fileAccess)
        {
            result = new FileManagerResult();
            try
            {
                if (File.Exists(filePath))
                {
                    result.FileStreamObj = new FileStream(filePath, fileMode, fileAccess);
                }
                else
                {
                    result.Errors.Add(FileManagerServiceConstants.FilePathDoesNotExist);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        public FileManagerResult GetStreamReaderText(string filePath, Encoding encoding)
        {
            result = new FileManagerResult();
            try
            {
                if (File.Exists(filePath))
                {
                    using (var streamReader = new StreamReader(filePath, encoding))
                    {
                        result.FileStreamText = streamReader.ReadToEnd();
                    }
                }
                else
                {
                    result.Errors.Add(FileManagerServiceConstants.FilePathDoesNotExist);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        public FileManagerResult SaveText(string filePath, string content, Encoding encoding)
        {
            result = new FileManagerResult();
            try
            {
                File.WriteAllText(filePath, content, encoding);
                result.IsFileWritten = true;
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}