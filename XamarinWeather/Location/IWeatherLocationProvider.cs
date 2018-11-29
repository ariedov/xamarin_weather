using System.Threading.Tasks;

namespace XamarinWeather.Location
{
    public interface IWeatherLocationProvider
    {
        Task<WeatherLocation> getLastLocation();

        Task getLocationUpdates(IWeatherLocationCallback callback);
    }

    public interface IWeatherLocationCallback
    {
        void OnLocationChanged(WeatherLocation location);
    }
}
