using System.Threading.Tasks;
using XamarinWeather.Location;

namespace XamarinWeather.Weather
{
    public interface IWeatherProvider
    {
        Task<Weather> GetWeather(WeatherLocation location);
    }
}
