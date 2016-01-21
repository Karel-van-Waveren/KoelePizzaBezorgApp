using PizzaBezorgApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaBezorgApp.ViewModels
{
    class BestelSchermViewModel
    {
        public string Title { get; }
        public BestelSchermViewModel()
        {
            Title = "Hier zijn uw bezorgingen";
        }
    }
}
