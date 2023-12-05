using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testapp.Services
{
    public interface IImageService
    {
        void DownloadImage(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public class DownloadEventArgs : EventArgs
        {
            public bool FileSaved = false;
            public DownloadEventArgs(bool fileSaved)
            {
                FileSaved = fileSaved;
            }
        }
    }
}
