using PizzaBezorgApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class AppGlobal
    {
        public static AppGlobal _Instance;
        public static AppGlobal Instance { get { return _Instance ?? (_Instance = new AppGlobal()); } }
        public GeoUtil _GeoUtil;
        public List<PizzaBestelling> PizzaBestellingen;

        public AppGlobal()
        {
            _GeoUtil = new GeoUtil();
            PizzaBestellingen = new List<PizzaBestelling>();
            BasicGeoposition Position = new BasicGeoposition();
            Position.Latitude = 51.58878;
            Position.Longitude = 4.77549;
            //test
            PizzaBestellingen.Add(new PizzaBestelling(4,"hawai", Position));
            PizzaBestellingen.Add(new PizzaBestelling(3,"salami", Position));
            PizzaBestellingen.Add(new PizzaBestelling(2,"kebab", Position));
            PizzaBestellingen.Add(new PizzaBestelling(4,"hawai", Position));
            PizzaBestellingen.Add(new PizzaBestelling(1,"salami", Position));
        }
    }
}
