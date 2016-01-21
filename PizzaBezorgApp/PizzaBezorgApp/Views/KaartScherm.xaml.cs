using PizzaBezorgApp.Models;
using PizzaBezorgApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class KaartScherm : Page
    {
        MapIcon user = new MapIcon();
        DispatcherTimer timer = new DispatcherTimer();

        public KaartScherm()
        {
            this.InitializeComponent();
            MapControl1.MapElements.Add(user);
            //Settings for timer
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            CenterMap();
            RefreshMapLocation();
        }

        private void timer_Tick(object sender, object e)
        {
            RefreshMapLocation();

        }

        public async void CenterMap()
        {
            Geoposition pos = await AppGlobal.Instance._GeoUtil.GetGeoLocation();
            MapControl1.Center = pos.Coordinate.Point;
        }

        private void SetPushpins()
        {
            if (AppGlobal.Instance.BestellingController.LoadBestelling() != null)
            {
                foreach (Bestelling l in AppGlobal.Instance.BestellingController.LoadBestelling())
                {
                    // Create a MapIcon.
                    MapIcon icon = new MapIcon();
                    icon.Location = new Geopoint(l.position);
                    icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/museum35.png"));
                    icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    MapControl1.MapElements.Add(icon);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetPushpins();
            timer.Start();
        }

        public async void RefreshMapLocation()
        {
            Geoposition pos = await AppGlobal.Instance._GeoUtil.GetGeoLocation();
            user.Location = pos.Coordinate.Point;
            user.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/pin65.png"));
        }

    }
}
