using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace Turismo.Library
{
    public static class GeofenceMonitorExtentions
    {
        public static IList<IList<Geopoint>> GetFenceGeometries(this GeofenceMonitor monitor)
        {
            return monitor.Geofences.Select(p => p.ToCirclePoints()).ToList();
        }
    }

    public static class GeofenceExtentions
    {
        public static IList<Geopoint> ToCirclePoints(this Geofence fence, int nrOfPoints = 50)
        {
            var geoCircle = fence.Geoshape as Geocircle;
            if (geoCircle != null)
            {
                return geoCircle.Center.GetCirclePoints(geoCircle.Radius, nrOfPoints);
            }
            return null;
        }
    }
}
