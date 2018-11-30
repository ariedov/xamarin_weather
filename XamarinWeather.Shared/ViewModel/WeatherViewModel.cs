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
            var location = await _locationProvider.GetLastLocation();
            if (location.HasValue)
            {
                LoadWeather(location.Value);
            }
            else
            {
                var locationCallback = new WeatherLocationCallback(_weatherProvider, _locationProvider);
                locationCallback.callback += LoadWeather;
                activeTask = _locationProvider.GetLocationUpdates(locationCallback);
            }

        }

        private void LoadWeather(WeatherLocation location)
        {
            activeTask = LoadWeatherAsync(location);
        }

        private async Task LoadWeatherAsync(WeatherLocation location)
        {
            var weather = await _weatherProvider.GetWeather(location);
            DataChanged?.Invoke(new WeatherData(0.0, ""));
        }


        public void Dispose()
        {
            activeTask?.Dispose();
        }

        class WeatherLocationCallback : IWeatherLocationCallback
        {
            public delegate void LocationUpdated(WeatherLocation location);
            public LocationUpdated callback;

            private readonly IWeatherProvider _weatherProvider;
            private readonly IWeatherLocationProvider _locationProvider;

            public WeatherLocationCallback(IWeatherProvider weatherProvider, 
                IWeatherLocationProvider locationProvider)
            {
                _weatherProvider = weatherProvider;
                _locationProvider = locationProvider;
            }

            public void OnLocationChanged(WeatherLocation location)
            {
                _locationProvider.CancelLocationUpdates();
                callback?.Invoke(location);
            }
        }
    }

    public class WeatherData
    {
        public double Temp { get; }
        public string IconType { get; }

        public WeatherData(double temp, string iconType)
        {
            Temp = temp;
            IconType = iconType;
        }
    }
}
