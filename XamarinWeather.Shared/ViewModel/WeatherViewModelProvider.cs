using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public class WeatherViewModelProvider
    {
        private readonly IWeatherLocationProvider _locationProvider;
        private readonly IWeatherProvider _weatherProvider;

        public WeatherViewModelProvider(IWeatherLocationProvider locationProvider, 
            IWeatherProvider weatherProvider)
        {
            _locationProvider = locationProvider;
            _weatherProvider = weatherProvider;
        }

        public Task<Weather.Weather> GetWeather()
        {
            var location = GetLocation().Result;
            return _weatherProvider.GetWeather(location);
        }

        private Task<WeatherLocation> GetLocation()
        {
            var t = new TaskCompletionSource<WeatherLocation>();
            var location = _locationProvider.GetLastLocation().Result;
            if (location.HasValue)
            {
                t.SetResult(location.Value);
            }
            else
            {
                _locationProvider.GetLocationUpdates((weatherLocation) =>
                {
                    _locationProvider.CancelLocationUpdates();
                    t.SetResult(weatherLocation);
                });
            }

            return t.Task;
        }

    }
}
