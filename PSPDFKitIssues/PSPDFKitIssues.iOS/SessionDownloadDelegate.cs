using System;
using System.Diagnostics;
using System.IO;
using Foundation;
using Xamarin.Forms;

namespace PSPDFKitIssues.iOS
{
    public class SessionDownloadDelegate : NSUrlSessionDownloadDelegate
    {
        private NSUrlSession _session;
        private NSUrlSessionDownloadTask _streamingTask;
        private float _progress;
        private string _finalPath;

        public SessionDownloadDelegate()
        {
            _session = NSUrlSession.FromConfiguration(NSUrlSessionConfiguration.DefaultSessionConfiguration,
                this, NSOperationQueue.MainQueue);
            //read about nsurl download https://www.ralfebert.de/ios-examples/networking/urlsession-background-downloads/
        }

        public void StartStreaming(string downloadUrl,
            string destinationFilePath)
        {
            string downloadTaskId = string.Empty;

            try
            {
                _finalPath = destinationFilePath;

                var nsUrl = NSUrl.FromString(downloadUrl);
                var request = new NSMutableUrlRequest(nsUrl,
                    cachePolicy: NSUrlRequestCachePolicy.ReloadIgnoringLocalCacheData,
                    timeoutInterval: 20);//TODO: Check timeout intervals and to find the best one.

                request.HttpMethod = "GET";

                _streamingTask = _session.CreateDownloadTask(request: request);
                downloadTaskId = _streamingTask.TaskIdentifier.ToString();
                _streamingTask.Resume();

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error while downloading: {e.Message}");
            }
        }

        public override async void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
        {
            var filePath = location.ToString().Replace("file:///", "/");
            if (System.IO.File.Exists(filePath))
            {
                if (System.IO.File.Exists(_finalPath))
                    System.IO.File.Delete(_finalPath);
                System.IO.File.Move(filePath, _finalPath);
                var info = new FileInfo(_finalPath);
                var size = info.Length;
                MessagingCenter.Instance.Send<object>(this, "DownloadCompleted");
            }
        }

        public override async void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
        {
            _progress = ((float)(new decimal(totalBytesWritten) / new decimal(totalBytesExpectedToWrite))) * 100;
            Debug.WriteLine($"Download Progress: {_progress}");
        }

        public override void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
        {
            if (error != null)
            {
                Debug.WriteLine("Error while downloading");
            }
        }
    }
}