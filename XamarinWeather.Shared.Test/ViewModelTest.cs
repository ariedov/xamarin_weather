using NUnit.Framework;
using Moq;
using XamarinWeather.Shared.Weather;
using XamarinWeather.Shared.Location;
using XamarinWeather.Shared.ViewModel;
using XamarinWeather.Shared.Maybe;

namespace XamarinWeather.Shared.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestRecentLocationAvailable()
        {
            Mock<IWeatherLocationProvider> locationProvider = new Mock<IWeatherLocationProvider>();
            Mock<IWeatherProvider> weatherProvider = new Mock<IWeatherProvider>();

            WeatherViewModel weatherViewModel = new WeatherViewModel(
                locationProvider.Object, weatherProvider.Object);

            locationProvider.Setup(x => x.GetLastLocation())
                .ReturnsAsync(Maybe<WeatherLocation>.Some(new WeatherLocation(0.0, 0.0)));
            weatherProvider.Setup(x => x.GetWeather(It.IsAny<WeatherLocation>()))
                .ReturnsAsync(new Weather.Weather("", null, null));

            weatherViewModel.StartWeatherLoading();

            Mock.Get(locationProvider.Object).Verify(x => x.GetLastLocation(), Times.Exactly(1));
            Mock.Get(weatherProvider.Object).Verify(x => x.GetWeather(It.IsAny<WeatherLocation>()),
                Times.Exactly(1));

            Mock.Get(locationProvider.Object).Verify(x => x.GetLocationUpdates(It.IsAny<IWeatherLocationCallback>()),
                Times.Never());
            Mock.Get(locationProvider.Object).Verify(x => x.CancelLocationUpdates(),
                Times.Never());
        }

        [Test]
        public void TestRecentLocationUnavailable()
        {
            Mock<IWeatherLocationProvider> locationProvider = new Mock<IWeatherLocationProvider>();
            Mock<IWeatherProvider> weatherProvider = new Mock<IWeatherProvider>();

            WeatherViewModel weatherViewModel = new WeatherViewModel(
                locationProvider.Object, weatherProvider.Object);

            locationProvider.Setup(x => x.GetLastLocation())
                .ReturnsAsync(Maybe<WeatherLocation>.None);
            locationProvider.Setup(x => x.GetLocationUpdates(It.IsAny<IWeatherLocationCallback>()))
                .Callback<IWeatherLocationCallback>(callback => callback.OnLocationChanged(new WeatherLocation(0.0, 0.0)));
            weatherProvider.Setup(x => x.GetWeather(It.IsAny<WeatherLocation>()))
                .ReturnsAsync(new Weather.Weather("", null, null));

            weatherViewModel.StartWeatherLoading();

            Mock.Get(locationProvider.Object).Verify(x => x.GetLastLocation(), Times.Exactly(1));
            Mock.Get(locationProvider.Object).Verify(x => x.GetLocationUpdates(It.IsAny<IWeatherLocationCallback>()),
                Times.Exactly(1));
            Mock.Get(locationProvider.Object).Verify(x => x.CancelLocationUpdates(),
                Times.Exactly(1));

            Mock.Get(weatherProvider.Object).Verify(x => x.GetWeather(It.IsAny<WeatherLocation>()),
                Times.Exactly(1));
        }
    }
}
