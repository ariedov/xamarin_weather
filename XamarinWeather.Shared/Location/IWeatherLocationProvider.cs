using System.Threading.Tasks;
using XamarinWeather.Shared.Maybe;

namespace XamarinWeather.Shared.Location
{
    public interface IWeatherLocationProvider
    {
        Task<Maybe<WeatherLocation>> GetLastLocation();

        Task GetLocationUpdates(IWeatherLocationCallback callback);

        void CancelLocationUpdates();
    }

    public interface IWeatherLocationCallback
    {
        void OnLocationChanged(WeatherLocation location);
    }
}
