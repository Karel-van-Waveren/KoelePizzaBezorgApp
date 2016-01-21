
using System.Collections.Generic;
using System.Linq;

namespace PizzaBezorgApp.Models
{
    public class BestellingController
    {
        public List<Bestelling> Bestellingen;
        public BestellingController()
        {
            Bestellingen = new List<Bestelling>();
            AddTestBestellingen();
        }

        //Geef een lijst terug van alle pizza bestellingen 
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
            Bestellingen.Add(new PizzaBestelling("Karel", 4, "supreme","Breda", "Bergdreef130"));
            Bestellingen.Add(new PizzaBestelling("Marnix", 4, "Hawaii", "Breda", "Sleutelbloem56"));
            Bestellingen.Add(new PizzaBestelling("Robert", 4, "Hawaii", "Terheijden", "Wilgenstraat3"));
            Bestellingen.Add(new PizzaBestelling("Jean-pierre", 4, "Champiogn", "Maastricht", "Hoogbrugstraat5"));
        }
    }
}
