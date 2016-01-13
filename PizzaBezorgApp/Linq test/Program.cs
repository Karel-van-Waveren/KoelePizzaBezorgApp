using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_test
{
    class Program
    {
        List<Bestelling> bestellingen;
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            bestellingen = new List<Bestelling>
            {
                new Bestelling(10, "kaas", "Breda"),
                new Bestelling(13, "worst", "Groningen"),
                new Bestelling(15, "peperoni", "Breda"),
                new Bestelling(18, "Mozzer", "Spanje")
            };



            var bestelQuery =
                from bestel in bestellingen
                where bestel.Stad == "Breda"
                select bestel;

            foreach (var num in bestelQuery)
            {
                Console.WriteLine(num);
            }

            Console.ReadLine();
        }


    }

    class Bestelling
    {
        private readonly int _positie;
        private readonly String _soortPizza;
        public string Stad;

        public Bestelling(int positie, String soortPizza, string stad)
        {
            _positie = positie;
            _soortPizza = soortPizza;
            Stad = stad;
        }


        public override string ToString()
        {
            return _positie + " "  + _soortPizza + " " + Stad;
        }
    }
}
