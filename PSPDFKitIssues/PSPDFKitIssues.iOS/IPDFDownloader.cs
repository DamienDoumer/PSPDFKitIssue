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
    public interface IPDFDownloader
    {
        Task DownloadPDF();
    }
}