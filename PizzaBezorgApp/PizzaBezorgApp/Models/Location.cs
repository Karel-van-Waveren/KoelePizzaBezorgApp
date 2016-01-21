using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public interface Location
    {
        int aantal { get; set; }
        BasicGeoposition Position { get; set; }
        string soort { get; set; }
    }
}
