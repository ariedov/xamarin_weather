using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public class WeatherViewModel
    {
        public delegate void WeatherStateHandler(WeatherData data);
        public event WeatherStateHandler DataChanged;

        private readonly IWeatherLocationProvider _locationProvider;
        private readonly IWeatherProvider _weatherProvider;

        private Task activeTask;

        public WeatherViewModel(IWeatherLocationProvider locationProvider,
            IWeatherProvider weatherProvider)
        {
            _locationProvider = locationProvider;
            _weatherProvider = weatherProvider;
        }

        public void StartWeatherLoading()
        {
            activeTask = LoadWeather();
        }

        private async Task LoadWeather()
        {
            var location = await GetLocation();
            LoadWeather(location);
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

        private void LoadWeather(WeatherLocation location)
        {
            activeTask = LoadWeatherAsync(location);
        }

        private async Task LoadWeatherAsync(WeatherLocation location)
        {
            var weather = await _weatherProvider.GetWeather(location);
            DataChanged?.Invoke(new WeatherData(0.0, WeatherIcon.MIST));
        }

        public void Dispose()
        {
        }
    }

    public class WeatherData
    {
        public double Temp { get; }
        public WeatherIcon IconType { get; }

        public WeatherData(double temp, WeatherIcon iconType)
        {
            Temp = temp;
            IconType = iconType;
        }
    }
}
