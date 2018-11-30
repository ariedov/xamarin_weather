namespace XamarinWeather.Shared.Location
{
    public class WeatherLocation
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public WeatherLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
