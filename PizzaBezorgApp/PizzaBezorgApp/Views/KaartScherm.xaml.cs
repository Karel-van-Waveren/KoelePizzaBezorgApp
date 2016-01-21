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
            if (AppGlobal.Instance.BestellingController.LoadBestelling() != null)
            {
                foreach (Bestelling l in AppGlobal.Instance.BestellingController.LoadBestelling())
                {
                    // Create a MapIcon.
                    MapIcon icon = new MapIcon();
                    icon.Location = new Geopoint(l.Position);
                    icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/museum35.png"));
                    icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    MapControl1.MapElements.Add(icon);
                }
            }
        }

        private async void UpdateRouteOnMap()
        {

            List<Location> route = AppGlobal.Instance._CurrentSession.GetToFollowRoute();

            if (!route.Any())
            {
                // Route is finished
                AppGlobal.Instance._CurrentSession.CurrentRoute = null;
                AppGlobal.Instance._CurrentSession.FollowedRoute = null;
            }
            else
            {
                List<Location> firstRoute = new List<Location>();
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
                    currentFollowingPath.RouteColor = Colors.RoyalBlue;
                    currentFollowingPath.OutlineColor = Colors.RoyalBlue;

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
        }

        public async void RefreshMapLocation()
        {
            Geoposition pos = await AppGlobal.Instance._GeoUtil.GetGeoLocation();
            user.Location = pos.Coordinate.Point;
            user.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/icons/pin65.png"));
        }

    }
}
