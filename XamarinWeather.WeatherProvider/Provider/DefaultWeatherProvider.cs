using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.WeatherProvider.Provider
{
    public class DefaultWeatherProvider : IWeatherProvider
    {
        public Task<Weather> GetWeather(WeatherLocation location)
        {
            return Task.Run(() => 
            {
                var description = new Description(
                    0, "cloudy", "cloudy", "04n"
                );
                var main = new Main(
                    18.0, 1000, 85
                );
                return new Weather("Kyiv", description, main);
            });
        }
    }
}
