using PizzaBezorgApp.Models;
using PizzaBezorgApp.ViewModels;
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
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class BestelScherm : Page
    {

        private List<Bestelling> bestellingen;
        public BestelScherm()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(KaartScherm));
            };
            DataContext = new BestelSchermViewModel();
            bestellingen = AppGlobal.Instance.BestellingController.LoadBestelling();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppGlobal.Instance._CurrentSession.SwitchRoute();
            if (AppGlobal.Instance._CurrentSession.FollowedRoute != null && !AppGlobal.Instance._CurrentSession.FollowedRoute.Any())
            {
                AppGlobal.Instance._CurrentSession.FollowedRoute.Add(AppGlobal.Instance._CurrentSession.CurrentRoute.Bestellingen.FirstOrDefault());
            }
            Frame.Navigate(typeof(KaartScherm));
            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }
    }
}
