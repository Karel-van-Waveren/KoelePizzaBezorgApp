
using System.Collections.Generic;
using System.Linq;

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
            AddTestBestellingen();
        }

        public List<Bestelling> LoadBestelling()
        {
            var bestelQuery =
            from bestel in Bestellingen
            where bestel  is PizzaBestelling
            select bestel;
            return bestelQuery.ToList();
        }

        public void AddTestBestellingen()
        {
            //test
            Bestellingen.Add(new PizzaBestelling("karel", 4, "supreme","Breda","Bergdreef130"));
            //Bestellingen.Add(new PizzaBestelling("hans", 3, "salami"));
            //Bestellingen.Add(new PizzaBestelling("paul", 2, "kebab"));
            //Bestellingen.Add(new PizzaBestelling("paul", 4, "hawaii"));
            //Bestellingen.Add(new PizzaBestelling("paul", 1, "salami"));

            //LocationList.Add(new PizzaBestelling("hans", 4, "hawai", Position));
            //LocationList.Add(new PizzaBestelling("hans", 3, "salami", Position));
            //LocationList.Add(new PizzaBestelling("paul", 2, "kebab", Position));
            //LocationList.Add(new PizzaBestelling("paul", 4, "hawai", Position));
            //LocationList.Add(new PizzaBestelling("paul", 1, "salami", Position));
        }
    }
}
