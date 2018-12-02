
using System;
using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Maybe;

namespace XamarinWeather.Droid.Location
{
    public class LocationProvider: IWeatherLocationProvider
    {
        public LocationProvider()
        {
        }

        public void CancelLocationUpdates()
        {

        }

        public async Task<Maybe<WeatherLocation>> GetLastLocation()
        {
            return await Task.Run(() =>
            {
                return Maybe<WeatherLocation>.Some(new WeatherLocation(
                    35.0, 139.0
                ));
            });

        }

        public async Task GetLocationUpdates(Action<WeatherLocation> action)
        {
        }
    }
}
