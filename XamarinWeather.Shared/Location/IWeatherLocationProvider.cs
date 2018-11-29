using System.Threading.Tasks;

namespace XamarinWeather.Location
{
    public interface IWeatherLocationProvider
    {
        Task<WeatherLocation> GetLastLocation();

        Task GetLocationUpdates(IWeatherLocationCallback callback);

        void CancelLocationUpdates();
    }

    public interface IWeatherLocationCallback
    {
        void OnLocationChanged(WeatherLocation location);
    }
}
