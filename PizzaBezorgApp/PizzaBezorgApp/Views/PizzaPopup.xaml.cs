using PizzaBezorgApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PizzaBezorgApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PizzaPopup : Page
    {
        public PizzaPopup()
        {
            this.InitializeComponent();
            Naam.Text = "Naam: "+AppGlobal.Instance._CurrentSession.CurrentBestelling.besteller;
            Soort.Text = "Soort: "+AppGlobal.Instance._CurrentSession.CurrentBestelling.soort;
            Aantal.Text = "Aantal: "+AppGlobal.Instance._CurrentSession.CurrentBestelling.aantal.ToString();
            Adres.Text = "Adres: "+AppGlobal.Instance._CurrentSession.CurrentBestelling.adres;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }
    }
}
