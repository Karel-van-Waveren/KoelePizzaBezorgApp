using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class BestellingController
    {
        public List<Bestelling> Bestellingen;
        public List<Location> LocationList;
        public BestellingController()
        {
            Bestellingen = new List<Bestelling>();
            LocationList = new List<Location>();
        }

        public List<Bestelling> LoadBestelling()
        {
            List<Bestelling> pizzabestellingen = new List<Bestelling>();

            var bestelQuery =
            from bestel in Bestellingen
            where bestel  is PizzaBestelling
            select bestel;
            foreach (var num in bestelQuery)
            {
                pizzabestellingen.Add(num);
            }
            return pizzabestellingen;
        }

        public void AddTestBestellingen()
        {
            BasicGeoposition Position = new BasicGeoposition();
            Position.Latitude = 51.58878;
            Position.Longitude = 4.77549;
            //test
            Bestellingen.Add(new PizzaBestelling(4, "hawai", Position));
            Bestellingen.Add(new PizzaBestelling(3, "salami", Position));
            Bestellingen.Add(new PizzaBestelling(2, "kebab", Position));
            Bestellingen.Add(new PizzaBestelling(4, "hawai", Position));
            Bestellingen.Add(new PizzaBestelling(1, "salami", Position));
        }
    }
}
