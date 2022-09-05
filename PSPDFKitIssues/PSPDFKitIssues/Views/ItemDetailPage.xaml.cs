using PSPDFKitIssues.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PSPDFKitIssues.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}