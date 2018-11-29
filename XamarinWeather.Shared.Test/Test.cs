using NUnit.Framework;
using Moq;
using XamarinWeather.Weather;
using XamarinWeather.Location;
using XamarinWeather.ViewModel;

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
                .ReturnsAsync(new WeatherLocation(0.0, 0.0));
            weatherProvider.Setup(x => x.GetWeather(It.IsAny<WeatherLocation>()))
                .ReturnsAsync(new Weather.Weather(null, null));

            weatherViewModel.StartWeatherLoading();

            Mock.Get(locationProvider.Object).Verify(x => x.GetLastLocation(), Times.Exactly(1));
            Mock.Get(weatherProvider.Object).Verify(x => x.GetWeather(It.IsAny<WeatherLocation>()), 
                Times.Exactly(1));
        }
    }
}
