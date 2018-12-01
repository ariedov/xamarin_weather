using System.Threading.Tasks;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.ViewModel
{
    public class WeatherViewModel
    {
        public delegate void WeatherStateHandler(WeatherData data);
        public event WeatherStateHandler DataChanged;

        private WeatherViewModelProvider _dataProvider;

        private Task activeTask;

        public WeatherViewModel(WeatherViewModelProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void StartWeatherLoading()
        {
            activeTask = Task.Run(LoadWeather);
        }

        private async Task LoadWeather()
        {
            var weather = await _dataProvider.GetWeather();
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
