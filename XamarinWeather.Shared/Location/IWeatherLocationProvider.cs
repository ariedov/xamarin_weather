using System;
using System.Threading.Tasks;
using XamarinWeather.Shared.Maybe;

namespace XamarinWeather.Shared.Location
{
    public interface IWeatherLocationProvider
    {
        Task<Maybe<WeatherLocation>> GetLastLocation();

        Task GetLocationUpdates(Action<WeatherLocation> action);

        void CancelLocationUpdates();
    }
}
