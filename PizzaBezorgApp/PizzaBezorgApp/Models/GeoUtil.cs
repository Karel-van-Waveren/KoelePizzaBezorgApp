using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace PizzaBezorgApp.Models
{
    public class GeoUtil
    {
        private CancellationTokenSource _cts;
        private Geolocator geoLocator;
        public GeolocationAccessStatus AccessStatus;


        public GeoUtil()
        {
            geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
        }

        public async void RequestAccess()
        {
            AccessStatus = await Geolocator.RequestAccessAsync();
        }

        public async Task<Geoposition> GetGeoLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    try
                    {
                        //Get cancellation token
                        _cts = new CancellationTokenSource();
                        CancellationToken token = _cts.Token;

                        //Carry out the operation
                        return await geoLocator.GetGeopositionAsync().AsTask(token);

                    }
                    catch (TaskCanceledException)
                    {
                        Debug.WriteLine("geen gps gevonden");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        // If there are no location sensors GetGeopositionAsync() 
                        // will timeout -- that is acceptable. 
                        const int WaitTimeoutHResult = unchecked((int)0x80070102);

                        if (ex.HResult == WaitTimeoutHResult) // WAIT_TIMEOUT 
                        {
                            Debug.WriteLine("geen gps gevonden");
                        }
                        else
                        {
                            //Om te testen. Moet nog anders. 
                            var dialog = new Windows.UI.Popups.MessageDialog(ex.ToString());
                        }
                        return null;
                    }
                    finally
                    {
                        _cts = null;
                    }

                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("App heeft geen toegang tot uw locatie");
                    return null;

                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("App heeft geen toegang ");
                    return null;
            }

            return null;
        }
    }
}
