using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using PSPDFKitIssues.iOS;
using PSPDFKitIssues.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PDFPage), typeof(PDFViewerRenderer))]
namespace PSPDFKitIssues.iOS
{
	public class PDFViewerRenderer : PageRenderer
	{

		public static readonly string PdfFilePath = @"pdf/PSPDFKit QuickStart Guide.pdf";

		UIViewController pdfController;

		public override void DidMoveToParentViewController(UIViewController parent)
		{
			if (pdfController == null)
			{
				var containerController = parent;
				var document = new PSPDFDocument(NSUrl.FromFilename($"{FileSystem.AppDataDirectory}/test.pdf"));
				pdfController = new PSPDFViewController(document, PSPDFConfiguration.FromConfigurationBuilder((builder) => {
					builder.UseParentNavigationBar = true;
					builder.ShouldHideStatusBarWithUserInterface = true;
				}));

				containerController.AddChildViewController(pdfController);
				pdfController.View.Frame = containerController.View.Bounds; // make the controller fullscreen in your container controller
				pdfController.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight; // ensure the controller resizes along with your container controller
				containerController.View.AddSubview(pdfController.View);
				return;
			}

			base.DidMoveToParentViewController(parent);
		}
	}
}