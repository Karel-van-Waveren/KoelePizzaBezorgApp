using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace adresstest
{
    class Program
    {
        private string stad;
        private string address;

        static void Main(string[] args)
        {
            new Program();
            Console.ReadLine();
        }


        public Program()
        {
            stad = "Breda";
            address = "bergdreef130";
            GetGeopoint();
        }
        private async Task<string> GetAdress()
        {
            string url = "http://dev.virtualearth.net/REST/v1/Locations?countryRegion=NL&locality=" + stad +
                         "&addressLine=" + address +
                         "&key=LmASekjs1bjQfxvA4OM3~V85W7tCphoOfYRlRHoYQZQ~Av6XwRGn0FrD0PhSTpCprZy12knFFh-UPKHGvEOnEISST7c5iHqwDbl-oN-TnTuY";


            using (HttpClient httpcl = new HttpClient())
            {
                var response = await httpcl.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async void GetGeopoint()
        {
            string cmd = await GetAdress();
            JObject allInfo = JObject.Parse(cmd);
           var info  = allInfo["resourceSets"].First["resources"].First["point"]["coordinates"];
            var lang = info.First.ToString();
            var ladi = info.Last.ToString();
                Console.WriteLine(lang);
            Console.WriteLine(ladi);
                Console.WriteLine( "adasdasdasd");
            }
        }
    }
