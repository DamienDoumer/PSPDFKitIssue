using System;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using PSPDFKitIssues.iOS;
using PSPDFKitIssues.Views;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(PDFModalPage), typeof(PDFViewerModalRenderer))]
namespace PSPDFKitIssues.iOS
{
    public class PDFViewerModalRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            var document = new PSPDFDocument(NSUrl.FromFilename($"{FileSystem.AppDataDirectory}/test.pdf"));
            var containerController = ViewController;
            var pdfController = new PSPDFViewController(document, PSPDFConfiguration.FromConfigurationBuilder((builder) => {
                builder.UseParentNavigationBar = true;
                builder.ShouldHideStatusBarWithUserInterface = true;
            }));

            var navController = new UINavigationController(pdfController);

            // Since we are using Xamarin Forms navigation we need to pop out using it, so we are using `Element` to tell X.F that we are done and pop us out.
            var menuItem = new UIBarButtonItem(UIBarButtonSystemItem.Done, async (sender, ev) => {
                // You can add some logic here before the controller is closed
                Console.WriteLine("Done with the document.");
                await Shell.Current.GoToAsync("//AboutPage");
                // Let PDFViewer object know that we want to pop
                //Element.Navigation.PopModalAsync();
            });
            pdfController.NavigationItem.LeftBarButtonItem = menuItem;

            containerController.AddChildViewController(navController);
            navController.View.Frame = containerController.View.Bounds; // make the controller fullscreen in your container controller
            navController.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight; // ensure the controller resizes along with your container controller
            containerController.View.AddSubview(navController.View);

            pdfController.DidMoveToParentViewController(navController);
        }

    }
}

