using NUnit.Framework;
using Moq;
using XamarinWeather.Shared.Weather;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.ViewModel;
using XamarinWeather.Shared.Maybe;
using System.Threading.Tasks;

namespace XamarinWeather.Shared.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public async Task TestRecentLocationAvailable()
        {
            Mock<IWeatherLocationProvider> locationProvider = new Mock<IWeatherLocationProvider>();
            Mock<IWeatherProvider> weatherProvider = new Mock<IWeatherProvider>();

            WeatherViewModelProvider dataProvider = new WeatherViewModelProvider(
                locationProvider.Object, weatherProvider.Object);

            locationProvider.Setup(x => x.GetLastLocation())
                .ReturnsAsync(Maybe<WeatherLocation>.Some(new WeatherLocation(0.0, 0.0)));
            weatherProvider.Setup(x => x.GetWeather(It.IsAny<WeatherLocation>()))
                .ReturnsAsync(new Weather.Weather("", null, null));

            await dataProvider.GetWeather();

            Mock.Get(locationProvider.Object).Verify(x => x.GetLastLocation(), Times.Exactly(1));
            Mock.Get(weatherProvider.Object).Verify(x => x.GetWeather(It.IsAny<WeatherLocation>()),
                Times.Exactly(1));
            Mock.Get(locationProvider.Object).Verify(x => x.GetLocationUpdates(It.IsAny<System.Action<WeatherLocation>>()),
                Times.Never());
            Mock.Get(locationProvider.Object).Verify(x => x.CancelLocationUpdates(),
                Times.Never());
        }

        [Test]
        public async Task TestRecentLocationUnavailable()
        {
            Mock<IWeatherLocationProvider> locationProvider = new Mock<IWeatherLocationProvider>();
            Mock<IWeatherProvider> weatherProvider = new Mock<IWeatherProvider>();

            WeatherViewModelProvider dataProvider = new WeatherViewModelProvider(
                locationProvider.Object, weatherProvider.Object);

            locationProvider.Setup(x => x.GetLastLocation())
                .ReturnsAsync(Maybe<WeatherLocation>.None);
            locationProvider.Setup(x => x.GetLocationUpdates(It.IsAny<System.Action<WeatherLocation>>()))
                .Callback<System.Action<WeatherLocation>> (callback => callback(new WeatherLocation(0.0, 0.0)));
            weatherProvider.Setup(x => x.GetWeather(It.IsAny<WeatherLocation>()))
                .ReturnsAsync(new Weather.Weather("", null, null));

            await dataProvider.GetWeather();

            Mock.Get(locationProvider.Object).Verify(x => x.GetLastLocation(), Times.Exactly(1));
            Mock.Get(locationProvider.Object).Verify(x => x.GetLocationUpdates(It.IsAny<System.Action<WeatherLocation>>()),
                Times.Exactly(1));
            Mock.Get(locationProvider.Object).Verify(x => x.CancelLocationUpdates(),
                Times.Exactly(1));

            Mock.Get(weatherProvider.Object).Verify(x => x.GetWeather(It.IsAny<WeatherLocation>()),
                Times.Exactly(1));
        }
    }
}
