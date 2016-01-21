using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaBezorgApp.Models
{
    public class CurrentSession
    {

        public List<Location> FollowedRoute;
        public BestellingController _currentRoute;
         
        public CurrentSession()
        {
            FollowedRoute = new List<Location>();
        }

        public BestellingController CurrentRoute
        {
            get { return _currentRoute; }
            set { _currentRoute = value; RouteIsChanged(); }
        }
        
        public void SwitchRoute()
        {
            CurrentRoute = new BestellingController();
        }

        public static event EventHandler RouteChanged;
        public static void RouteIsChanged()
        {
            var handler = RouteChanged;
            if (handler != null)
            {
                handler(null, new EventArgs());
            }
        }

        public List<Location> GetToFollowRoute()
        {
            List<Location> ToFollow = new List<Location>();
            if (FollowedRoute == null)
            {
                FollowedRoute = new List<Location>();
            }
            if (CurrentRoute != null && FollowedRoute.Any())
            {
                ToFollow = CurrentRoute.Bestellingen.Except(FollowedRoute).ToList();
            }

            return ToFollow;
        }
    }
}
