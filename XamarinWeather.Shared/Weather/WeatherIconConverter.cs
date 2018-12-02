using System;
namespace XamarinWeather.Shared.Weather
{
    public class WeatherIconConverter
    {
        private const string CLEAR_SKY_DAY = "01d";
        private const string CLEAR_SKY_NIGHT = "01n";
        private const string FEW_CLOUDS_DAY = "02d";
        private const string FEW_CLOUDS_NIGHT = "02n";
        private const string SCATTERED_CLOUDS_DAY = "03d";
        private const string SCATTERED_CLOUDS_NIGHT = "03n";
        private const string BROKEN_CLOUDS_DAY = "04d";
        private const string BROKEN_CLOUDS_NIGHT = "04n";
        private const string SHOWER_RAIN_DAY = "09d";
        private const string SHOWER_RAIN_NIGHT = "09n";
        private const string RAIN_DAY = "10d";
        private const string RAIN_NIGHT = "10n";
        private const string THUNDERSTORM_DAY = "11d";
        private const string THUNDERSTORM_NIGHT = "11n";
        private const string SNOW_DAY = "13d";
        private const string SNOW_NIGHT = "13n";
        private const string MIST_DAY = "50d";
        private const string MIST_NIGHT = "50n";

        public WeatherIcon Convert(string code)
        {
            switch (code)
            {
                case CLEAR_SKY_DAY:
                    return WeatherIcon.CLEAR_SKY_DAY;
                case CLEAR_SKY_NIGHT:
                    return WeatherIcon.CLEAR_SKY_NIGHT;
                case FEW_CLOUDS_DAY:
                    return WeatherIcon.FEW_CLOUDS_DAY;
                case FEW_CLOUDS_NIGHT:
                    return WeatherIcon.FEW_CLOUDS_NIGHT;
                case SCATTERED_CLOUDS_DAY:
                case SCATTERED_CLOUDS_NIGHT:
                    return WeatherIcon.SCATTERED_CLOUDS;
                case BROKEN_CLOUDS_DAY:
                case BROKEN_CLOUDS_NIGHT:
                    return WeatherIcon.BROKEN_CLOUDS;
                case SHOWER_RAIN_DAY:
                case SHOWER_RAIN_NIGHT:
                    return WeatherIcon.SHOWER_RAIN;
                case RAIN_DAY:
                    return WeatherIcon.RAIN_DAY;
                case RAIN_NIGHT:
                    return WeatherIcon.RAIN_NIGHT;
                case THUNDERSTORM_DAY:
                case THUNDERSTORM_NIGHT:
                    return WeatherIcon.THUNDERSTORM;
                case SNOW_DAY:
                case SNOW_NIGHT:
                    return WeatherIcon.SNOW;
                case MIST_DAY:
                case MIST_NIGHT:
                    return WeatherIcon.MIST;
                default:
                    return WeatherIcon.MIST;
            }
        }
    }
}
