using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling
    {
        public int aantal { get; set; }
        public string soort { get; }
        public BasicGeoposition position { get; }
        public Geofence fence { get; }

        public PizzaBestelling(int aantal, string soort, BasicGeoposition position)
        {
            this.aantal = aantal;
            this.soort = soort;
            this.position = position;
            AddFence();
        }

        public void AddFence()
        {
            // Replace if it already exists for this maneuver key
            var oldFence = GeofenceMonitor.Current.Geofences.Where(p => p.Id == soort.ToString()).FirstOrDefault();
            if (oldFence != null)
            {
                GeofenceMonitor.Current.Geofences.Remove(oldFence);
            }

            Geocircle geocircle = new Geocircle(position, 25);

            bool singleUse = true;

            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            var geofence = new Geofence(soort.ToString(), geocircle, mask, singleUse, TimeSpan.FromSeconds(1));
            GeofenceMonitor.Current.Geofences.Add(geofence);
        }
    }
}
