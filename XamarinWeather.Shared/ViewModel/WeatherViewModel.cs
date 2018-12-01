using System.Threading;
using System.Threading.Tasks;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public class WeatherViewModel
    {
        public delegate void WeatherStateHandler(WeatherData data);
        public event WeatherStateHandler DataChanged;

        private WeatherViewModelProvider _dataProvider;
        private WeatherIconConverter _iconConverter;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public WeatherViewModel(WeatherViewModelProvider dataProvider, 
            WeatherIconConverter iconConverter)
        {
            _dataProvider = dataProvider;
            _iconConverter = iconConverter;
        }

        public async void StartWeatherLoading()
        {
            var weather = await Task.Run(() =>
            {
                return _dataProvider.GetWeather().Result;
            }, _tokenSource.Token);

            DataChanged?.Invoke(new WeatherData(weather.Name, weather.Main.Temp,
                    _iconConverter.Convert(weather.Description.Icon)));
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
