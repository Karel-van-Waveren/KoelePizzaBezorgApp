using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling : Bestelling
    {

        public PizzaBestelling(string besteller, int aantal, string soort, BasicGeoposition position) : base(besteller, aantal, soort, position)
        {
        }

    }
}
