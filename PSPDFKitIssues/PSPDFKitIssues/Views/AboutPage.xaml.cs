using PSPDFKitIssues.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSPDFKitIssues.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DependencyService.Get<IPDFDownloader>().DownloadPDF();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new PDFPage());
        }
    }
}