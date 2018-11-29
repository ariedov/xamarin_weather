using System.Threading.Tasks;
using XamarinWeather.Location;
using XamarinWeather.Weather;

namespace XamarinWeather.ViewModel
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
            activeTask = Task.Run(LoadWeather);
        }

        private async Task LoadWeather()
        {
            var location = await _locationProvider.GetLastLocation();
            if (location != null)
            {
                LoadWeather(location);
            }
            else
            {
                var locationCallback = new WeatherLocationCallback(_weatherProvider);
                locationCallback.callback += LoadWeather;
                await _locationProvider.GetLocationUpdates(locationCallback);
            }

        }

        private void LoadWeather(WeatherLocation location)
        {
            _locationProvider.CancelLocationUpdates();

            activeTask = Task.Run(() => LoadWeatherAsync(location));
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

            public WeatherLocationCallback(IWeatherProvider weatherProvider)
            {
                _weatherProvider = weatherProvider;
            }

            public void OnLocationChanged(WeatherLocation location)
            {
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
