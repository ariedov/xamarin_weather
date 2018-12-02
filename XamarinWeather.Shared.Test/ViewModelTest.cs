using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using XamarinWeather.Shared.ViewModel;
using XamarinWeather.Shared.Weather;

#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace XamarinWeather.Shared.Test
{
    [TestFixture]
    public class ViewModelTest
    {
        [Test]
        public async Task TestValidWeather()
        {
            var weatherProvider = new Mock<IWeatherViewModelProvider>();
            weatherProvider.Setup(x => x.GetWeather())
                .ReturnsAsync(new Weather.Weather("name", null, null));

            var viewModel = new WeatherViewModel(weatherProvider.Object, new WeatherIconConverter());

            WeatherData result = null;
            viewModel.DataChanged += (data) =>
            {
                result = data;
            };

            await viewModel.StartWeatherLoading();

            Assert.AreEqual("name", result.Name);
        }

        [Test]
        public async Task TestError()
        {
            var weatherProvider = new Mock<IWeatherViewModelProvider>();
            weatherProvider.Setup(x => x.GetWeather())
                .Throws(new Exception());

            var viewModel = new WeatherViewModel(weatherProvider.Object, new WeatherIconConverter());

            WeatherData result = null;
            viewModel.DataChanged += (data) =>
            {
                result = data;
            };

            await viewModel.StartWeatherLoading();

            Assert.AreEqual("Error", result.Name);
        }
    }
}
