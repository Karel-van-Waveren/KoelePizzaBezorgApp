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
        public BestellingController BestellingController { get; set; }
        public GeoUtil _GeoUtil;

        public AppGlobal()
        {
            _GeoUtil = new GeoUtil();
            BestellingController = new BestellingController();
            BestellingController.AddTestBestellingen();
            BestellingController.LoadBestelling();
        }
    }
}
