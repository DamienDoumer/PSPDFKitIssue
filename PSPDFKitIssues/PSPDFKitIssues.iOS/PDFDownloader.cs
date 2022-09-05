using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;

namespace PSPDFKitIssues.iOS
{
    public class PDFDownloader : IPDFDownloader
    {
        public Task DownloadPDF()
        {
            var downloadDelegate = new SessionDownloadDelegate();
            // HEre is the file URL on Google cloud. You can get its download link here too if it expires. "https://drive.google.com/file/d/1ULrU9rRLOD_zlpxQnsECkReSKuJuX4II/view?usp=sharing"
            downloadDelegate.StartStreaming("https://drive.google.com/u/2/uc?id=1ULrU9rRLOD_zlpxQnsECkReSKuJuX4II&export=download",
                $"{FileSystem.AppDataDirectory}/test.pdf");

            return Task.CompletedTask;
        }
    }
}