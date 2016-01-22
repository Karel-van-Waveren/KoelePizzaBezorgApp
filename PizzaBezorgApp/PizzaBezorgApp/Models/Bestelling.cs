using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Media.DialProtocol;

namespace PizzaBezorgApp.Models
{
    public class Bestelling
    {
        public string besteller { get; set; }
        public int aantal { get; set; }
        public BasicGeoposition Position { get; set; }
        public Geofence fence { get; set; }
        public string soort { get; set; }
        public string stad { get; set; }
        public string adres { get; set; }

        public Bestelling(string besteller, string soort, string stad , string adres)
        {
            this.besteller = besteller;
            this.aantal = 1;
            this.soort = soort;
            this.stad = stad;
            this.adres = adres;
            AddFence();
            GetGeopoint();
        }

        public void AddFence()
        {
            // Replace if it already exists for this maneuver key
            var oldFence = GeofenceMonitor.Current.Geofences.Where(p => p.Id == soort.ToString()).FirstOrDefault();
            if (oldFence != null)
            {
                GeofenceMonitor.Current.Geofences.Remove(oldFence);
            }

            Geocircle geocircle = new Geocircle(Position, 25);

            bool singleUse = true;

            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            var geofence = new Geofence(soort.ToString(), geocircle, mask, singleUse, TimeSpan.FromSeconds(1));
            GeofenceMonitor.Current.Geofences.Add(geofence);
            fence = geofence;
        }

        private async Task<string> GetAdres()
        {
            string url = "http://dev.virtualearth.net/REST/v1/Locations?countryRegion=NL&locality=" + stad +
                         "&addressLine=" + adres +
                         "&key=LmASekjs1bjQfxvA4OM3~V85W7tCphoOfYRlRHoYQZQ~Av6XwRGn0FrD0PhSTpCprZy12knFFh-UPKHGvEOnEISST7c5iHqwDbl-oN-TnTuY";


            using (HttpClient httpcl = new HttpClient())
            {
                var response = await httpcl.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async void GetGeopoint()
        {
            string cmd = await GetAdres();
            JObject allInfo = JObject.Parse(cmd);
            var info = allInfo["resourceSets"].First["resources"].First["point"]["coordinates"];
            var lati = info.First.ToString();
            var longi = info.Last.ToString();

            Position = new BasicGeoposition
            {
                Latitude = Convert.ToDouble(lati),
                Longitude = Convert.ToDouble(longi)
            };
        }


    }
}
