using PizzaBezorgApp.Models;
using PizzaBezorgApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
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
            DataContext = new KaartSchermViewModel();
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
            if (AppGlobal.Instance._CurrentSession.GetToFollowRoute().Any())
                UpdateRouteOnMap();
              
        }

        public async void CenterMap()
        {
            Geoposition pos = await AppGlobal.Instance._GeoUtil.GetGeoLocation();
            MapControl1.Center = pos.Coordinate.Point;
        }

        private void SetPushpins()
        {
            if (AppGlobal.Instance._CurrentSession.CurrentRoute != null)
            {
                foreach (Bestelling b in AppGlobal.Instance._CurrentSession.CurrentRoute.Bestellingen)
                {
                    // Create a MapIcon.
                    MapIcon icon = new MapIcon();
                    icon.Location = new Geopoint(b.Position);
                    icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/museum35.png"));
                    icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    MapControl1.MapElements.Add(icon);
                }
            }
        }

        private async void UpdateRouteOnMap()
        {
            List<Bestelling> route = AppGlobal.Instance._CurrentSession.GetToFollowRoute();

            if (!route.Any())
            {
                // Route is finished
                AppGlobal.Instance._CurrentSession.CurrentRoute = null;
                AppGlobal.Instance._CurrentSession.FollowedRoute = null;
            }
            else
            {
                List<Bestelling> firstRoute = new List<Bestelling>();
                firstRoute.Add(route.ElementAt(0));
                // Get the route between the points.
                MapRouteFinderResult routePoints = await AppGlobal.Instance._GeoUtil.GetRoutePoint2Point(route);
                MapRouteFinderResult currentPath = await AppGlobal.Instance._GeoUtil.GetRoutePoint2Point(firstRoute);

                if (routePoints.Status == MapRouteFinderStatus.Success)
                {
                    // Use the route to initialize a MapRouteView.
                    MapRouteView viewOfRoute = new MapRouteView(routePoints.Route);
                    viewOfRoute.RouteColor = Colors.LightGray;
                    viewOfRoute.OutlineColor = Colors.LightGray;

                    MapRouteView currentFollowingPath = new MapRouteView(currentPath.Route);
                    currentFollowingPath.RouteColor = Colors.Crimson;
                    currentFollowingPath.OutlineColor = Colors.Crimson;

                    // Add the new MapRouteView to the Routes collection
                    // of the MapControl.
                    MapControl1.Routes.Clear();
                    MapControl1.Routes.Add(viewOfRoute);
                    MapControl1.Routes.Add(currentFollowingPath);
                }

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetPushpins();
            timer.Start();
            GeofenceMonitor.Current.GeofenceStateChanged += GeofenceStateChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();
            GeofenceMonitor.Current.GeofenceStateChanged -= GeofenceStateChanged;
            MapControl1.MapElements.Clear();
        }

        private void GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            throw new NotImplementedException();
        }

        public async void RefreshMapLocation()
        {
            Geoposition pos = await AppGlobal.Instance._GeoUtil.GetGeoLocation();
            user.Location = pos.Coordinate.Point;
            user.NormalizedAnchorPoint = new Point(0.5, 0.5);
            user.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/pin65.png"));
        }

        private void Bestelling_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BestelScherm));
        }
    }
}
