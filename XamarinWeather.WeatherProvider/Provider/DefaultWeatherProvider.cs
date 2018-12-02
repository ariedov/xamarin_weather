using System.Net.Http;
using System.Json;
using System.Threading.Tasks;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.WeatherProvider.Provider
{
    public class DefaultWeatherProvider : IWeatherProvider
    {
        private const string API_KEY = "<insert your api key>";

        public async Task<Weather> GetWeather(WeatherLocation location)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={API_KEY}");
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JsonValue.Parse(responseString);

            var descriptionJson = json["weather"][0];
            var description = new Description(
                descriptionJson["id"], descriptionJson["main"], descriptionJson["description"], descriptionJson["icon"]
            );

            var mainJson = json["main"];
            var main = new Main(
                 mainJson["temp"], mainJson["pressure"], mainJson["humidity"]
             );

            return new Weather(json["name"], description, main);
        }
    }
}
