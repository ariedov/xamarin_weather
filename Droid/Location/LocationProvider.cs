
using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Gms.Location;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Maybe;

namespace XamarinWeather.Droid.Location
{
    public class LocationProvider: IWeatherLocationProvider
    {
        private readonly FusedLocationProviderClient _locationProvider;
        private LocationCallback _locationCallback;

        public LocationProvider(FusedLocationProviderClient locationProvider)
        {
            _locationProvider = locationProvider;
        }

        public async Task CancelLocationUpdates()
        {
            await _locationProvider.RemoveLocationUpdatesAsync(_locationCallback);
        }

        public async Task<Maybe<WeatherLocation>> GetLastLocation()
        {

            var location = await _locationProvider.GetLastLocationAsync();
            if (location != null)
            {
                var weather = new WeatherLocation(location.Latitude, location.Longitude);
                return Maybe<WeatherLocation>.Some(weather);
            }
            return Maybe<WeatherLocation>.None;
        }

        public async Task GetLocationUpdates(Action<WeatherLocation> action, Action error)
        {
            var locationRequest = new LocationRequest()
                                  .SetPriority(LocationRequest.PriorityLowPower)
                                  .SetInterval(60 * 1000 * 5)
                                  .SetFastestInterval(60 * 1000 * 2);

            _locationCallback = new FusedLocationProviderCallback(action, error);
            await _locationProvider.RequestLocationUpdatesAsync(locationRequest, _locationCallback);
        }
    }

    public class FusedLocationProviderCallback : LocationCallback
    {
        readonly Action<WeatherLocation> _callback;
        readonly Action _errorCallback;

        public FusedLocationProviderCallback(Action<WeatherLocation> callback, 
            Action errorCallback)
        {
            _callback = callback;
            _errorCallback = errorCallback;
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
            if (!locationAvailability.IsLocationAvailable)
            {
                _errorCallback?.Invoke();
            }
        }

        public override void OnLocationResult(LocationResult result)
        {
            if (result.Locations.Any())
            {
                var location = result.Locations.First();
                _callback?.Invoke(new WeatherLocation(location.Latitude, location.Longitude));
            }
            else
            {
                _errorCallback?.Invoke();
            }
        }
    }
}
