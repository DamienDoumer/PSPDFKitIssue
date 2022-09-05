using PSPDFKitIssues.ViewModels;
using PSPDFKitIssues.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PSPDFKitIssues
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
