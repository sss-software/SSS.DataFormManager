using Microsoft.WindowsAPICodePack.Shell;
using SSS.FileManagementService.Constants;
using SSS.FileManagementService.Models;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SSS.FileManagementService
{
    public class FileManager : IDisposable, IFileManager
    {
        #region fields

        internal FileManagerResult result;
        private System.Windows.Media.ImageSource icon;

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

        public FileManagerResult GetFileIcon(string filePath)
        {
            result = new FileManagerResult();
            try
            {
                if (icon == null && System.IO.File.Exists(filePath))
                {
                    using (System.Drawing.Icon sysicon = System.Drawing.Icon.ExtractAssociatedIcon(filePath))
                    {
                        icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                                  sysicon.Handle,
                                  System.Windows.Int32Rect.Empty,
                                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    }
                    result.ImageSourceObj = icon;
                    return result;
                }
                else
                {
                    result.ImageSourceObj = null;
                    result.Errors.Add(string.Empty);
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
            }
            return result;
        }

        public FileManagerResult GetFileThumbnailImage(string filePath, int width, int height)
        {
            result = new FileManagerResult();
            try
            {
                Image thumbnail = ShellFile.FromFilePath(filePath).Thumbnail.Bitmap.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                result.ImageSourceObj = BitmapToImageSource(new Bitmap(thumbnail, width, height));
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public ImageSource GetImageThumbnail(string filePath)
        {
            Image image = Image.FromFile(filePath);
            Size thumbnailSize = GetThumbnailSize(image);
            Image thumbnail = image.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
            return BitmapToImageSource(new Bitmap(thumbnail, thumbnailSize.Width, thumbnailSize.Height));
        }

        public bool SaveImageThumbnail(string filePath, string outPath)
        {
            try
            {
                Image image = Image.FromFile(filePath);
                Size thumbnailSize = GetThumbnailSize(image);
                Image thumbnail = image.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
                thumbnail.Save(outPath);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private Size GetThumbnailSize(Image original)
        {
            const int maxPixels = 40;
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

        private Image.GetThumbnailImageAbort Callback => new Image.GetThumbnailImageAbort(ThumbnailCallback);

        private bool ThumbnailCallback()
        {
            return false;
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