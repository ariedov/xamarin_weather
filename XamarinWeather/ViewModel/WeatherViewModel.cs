using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using XamarinWeather.Location;
using XamarinWeather.Weather;

namespace XamarinWeather.ViewModel
{
    public class WeatherViewModel
    {
        public event PropertyChangedEventHandler DataChanged;

        private readonly IWeatherLocationProvider _locationProvider;
        private readonly IWeatherProvider _weatherProvider;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token;

        public WeatherViewModel(IWeatherLocationProvider locationProvider,
            IWeatherProvider weatherProvider)
        {
            _locationProvider = locationProvider;
            _weatherProvider = weatherProvider;

            token = tokenSource.Token;
        }

        public void StartWeatherLoading()
        {
            Task.Factory.StartNew(LoadWeather, token);
        }

        private async Task LoadWeather()
        {
            var location = await _locationProvider.getLastLocation();
            var weather = _weatherProvider.GetWeather(location);

            DataChanged?.Invoke(this, new PropertyChangedEventArgs(new WeatherData(0.0, "")));
        }

        public void Dispose()
        {
            tokenSource.Dispose();
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
