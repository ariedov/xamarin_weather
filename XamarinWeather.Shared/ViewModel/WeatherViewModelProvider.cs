using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public interface IWeatherViewModelProvider
    {
        Task<Weather.Weather> GetWeather();
    }

    public class WeatherViewModelProvider : IWeatherViewModelProvider
    {
        private readonly IWeatherLocationProvider _locationProvider;
        private readonly IWeatherProvider _weatherProvider;

        public WeatherViewModelProvider(IWeatherLocationProvider locationProvider, 
            IWeatherProvider weatherProvider)
        {
            _locationProvider = locationProvider;
            _weatherProvider = weatherProvider;
        }

        public async Task<Weather.Weather> GetWeather()
        {
            var location = await GetLocation();
            return await _weatherProvider.GetWeather(location);
        }

        private async Task<WeatherLocation> GetLocation()
        {
            var location = await _locationProvider.GetLastLocation();
            if (location.HasValue)
            {
                return location.Value;
            }
            return await GetLocationFromCallback();
        }

        private Task<WeatherLocation> GetLocationFromCallback()
        {
            var t = new TaskCompletionSource<WeatherLocation>();
            _locationProvider.GetLocationUpdates((weatherLocation) =>
            {
                _locationProvider.CancelLocationUpdates();
                t.SetResult(weatherLocation);
            }, 
            () =>
            {
                t.SetException(new System.Exception());
            });
            return t.Task;
        }
    }
}
