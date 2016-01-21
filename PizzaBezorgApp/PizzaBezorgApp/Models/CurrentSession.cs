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
            set { _currentRoute = value;}
        }

        public List<Location> GetToFollowRoute()
        {
            List<Location> ToFollow = new List<Location>();
            if (FollowedRoute == null)
            {
                FollowedRoute = new List<Location>();
                AppGlobal.Instance._CurrentSession.FollowedRoute.Add(AppGlobal.Instance._CurrentSession.CurrentRoute.Bestellingen.FirstOrDefault());//Weet niet waar deze regel voor is maar ik zag hem staan bij de click methode (click is overbodig geworden)

            }
            if (CurrentRoute != null && FollowedRoute.Any())
            {
                ToFollow = CurrentRoute.Bestellingen.Except(FollowedRoute).ToList();
            }

            return ToFollow;
        }
    }
}
