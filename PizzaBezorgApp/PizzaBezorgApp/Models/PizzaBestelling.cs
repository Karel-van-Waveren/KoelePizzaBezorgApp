using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling : Bestelling
    {

        public PizzaBestelling(string besteller, int aantal, string soort , string stad , string adres) : base(besteller, aantal, soort,stad,adres)
        {
        }

    }
}
