using System;
using System.Threading;
using System.Threading.Tasks;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public class WeatherViewModel
    {
        public delegate void WeatherStateHandler(WeatherData data);
        public event WeatherStateHandler DataChanged;

        private IWeatherViewModelProvider _dataProvider;
        private WeatherIconConverter _iconConverter;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public WeatherViewModel(IWeatherViewModelProvider dataProvider,
            WeatherIconConverter iconConverter)
        {
            _dataProvider = dataProvider;
            _iconConverter = iconConverter;
        }

        public async Task StartWeatherLoading()
        {
            var weather = await Task.Run(() =>
            {
                try
                {
                    return _dataProvider.GetWeather().Result;
                }
                catch (Exception)
                {
                    return new Weather.Weather("Error", null, null);
                }
            }, _tokenSource.Token);

            if (weather.Main == null || weather.Description == null)
            {
                DataChanged?.Invoke(new WeatherData(weather.Name, -273.0,
                    WeatherIcon.THUNDERSTORM));
            }
            else
            {
                DataChanged?.Invoke(new WeatherData(weather.Name, weather.Main.Temp,
                    _iconConverter.Convert(weather.Description.Icon)));
            }
        }

        public void Dispose()
        {
            _tokenSource.Dispose();
        }
    }

    public class WeatherData
    {
        public string Name { get; }
        public double Temp { get; }
        public WeatherIcon IconType { get; }

        public WeatherData(string name, double temp, WeatherIcon iconType)
        {
            Name = name;
            Temp = temp;
            IconType = iconType;
        }
    }
}
