using PizzaBezorgApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;

namespace PizzaBezorgApp.Models
{
    public class AppGlobal
    {
        public static AppGlobal _Instance;
        public static AppGlobal Instance { get { return _Instance ?? (_Instance = new AppGlobal()); } }
        public BestellingController BestellingController { get; set; }
        public GeoUtil _GeoUtil;
        public CurrentSession _CurrentSession;
        public static ApplicationData APP_DATA = ApplicationData.Current;
        public static ApplicationDataContainer LOCAL_SETTINGS = APP_DATA.LocalSettings;

        public AppGlobal()
        {
            LOCAL_SETTINGS.Values["BingKey"] = "LmASekjs1bjQfxvA4OM3~V85W7tCphoOfYRlRHoYQZQ~Av6XwRGn0FrD0PhSTpCprZy12knFFh-UPKHGvEOnEISST7c5iHqwDbl-oN-TnTuY";
            BestellingController = new BestellingController();
            _GeoUtil = new GeoUtil();
            _CurrentSession = new CurrentSession();
        }
    }
}
