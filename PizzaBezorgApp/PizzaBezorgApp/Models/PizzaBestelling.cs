using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling : Bestelling
    {
        public string SoortPizza { get; set; }

        public PizzaBestelling(int aantal, string soortPizza, BasicGeoposition position) : base(aantal, position)
        {
            this.SoortPizza = soortPizza;
        }

    }
}
