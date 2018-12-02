using NUnit.Framework;
using System;
using XamarinWeather.Shared.Weather;

namespace XamarinWeather.Shared.Test
{
    [TestFixture]
    public class WeatherIconConverterTest
    {
        WeatherIconConverter Converter { get; set; }

        [SetUp]
        public void SetUp()
        {
            Converter = new WeatherIconConverter();
        }

        [Test]
        public void Test()
        {
            Assert.AreEqual(WeatherIcon.CLEAR_SKY_DAY, Converter.Convert("01d"));
            Assert.AreEqual(WeatherIcon.CLEAR_SKY_NIGHT, Converter.Convert("01n"));
            Assert.AreEqual(WeatherIcon.FEW_CLOUDS_DAY, Converter.Convert("02d"));
            Assert.AreEqual(WeatherIcon.FEW_CLOUDS_NIGHT, Converter.Convert("02n"));
            Assert.AreEqual(WeatherIcon.SCATTERED_CLOUDS, Converter.Convert("03d"));
            Assert.AreEqual(WeatherIcon.SCATTERED_CLOUDS, Converter.Convert("03n"));
            Assert.AreEqual(WeatherIcon.BROKEN_CLOUDS, Converter.Convert("04d"));
            Assert.AreEqual(WeatherIcon.BROKEN_CLOUDS, Converter.Convert("04n"));
            Assert.AreEqual(WeatherIcon.SHOWER_RAIN, Converter.Convert("09d"));
            Assert.AreEqual(WeatherIcon.SHOWER_RAIN, Converter.Convert("09n"));
            Assert.AreEqual(WeatherIcon.RAIN_DAY, Converter.Convert("10d"));
            Assert.AreEqual(WeatherIcon.RAIN_NIGHT, Converter.Convert("10n"));
            Assert.AreEqual(WeatherIcon.THUNDERSTORM, Converter.Convert("11d"));
            Assert.AreEqual(WeatherIcon.THUNDERSTORM, Converter.Convert("11n"));
            Assert.AreEqual(WeatherIcon.SNOW, Converter.Convert("13d"));
            Assert.AreEqual(WeatherIcon.SNOW, Converter.Convert("13n"));
            Assert.AreEqual(WeatherIcon.MIST, Converter.Convert("50d"));
            Assert.AreEqual(WeatherIcon.MIST, Converter.Convert("50n"));
        }
    }
}
