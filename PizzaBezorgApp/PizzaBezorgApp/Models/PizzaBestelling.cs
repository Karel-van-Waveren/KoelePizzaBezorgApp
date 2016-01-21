using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling : Bestelling
    {

        public PizzaBestelling(int aantal, string soort, BasicGeoposition position) : base(aantal, soort, position)
        {
        }

    }
}
