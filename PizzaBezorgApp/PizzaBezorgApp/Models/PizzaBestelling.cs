using System;
using System.Collections.Generic;

namespace PizzaBezorgApp.Models
{
    public class PizzaBestelling : Bestelling
    {
        public List<String> Pizzas;
        public PizzaBestelling(string besteller, string soort , string stad , string adres, List<String> pizza) : base(besteller, soort,stad,adres)
        {
            Pizzas = pizza;
            aantal = Pizzas.Count;
        }

    }
}
