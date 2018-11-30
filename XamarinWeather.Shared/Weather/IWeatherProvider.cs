using System.Threading.Tasks;
using XamarinWeather.Shared.Location;

namespace XamarinWeather.Shared.Weather
{
    public interface IWeatherProvider
    {
        Task<Weather> GetWeather(WeatherLocation location);
    }
}
