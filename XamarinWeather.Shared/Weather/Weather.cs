using System;
namespace XamarinWeather.Shared.Weather
{
    public class Weather
    {
        public string Name { get; }
        public Description Description { get; }
        public Main Main { get; }

        public Weather(string name, Description description, Main main)
        {
            Name = name;
            Description = description;
            Main = main;
        }
    }

    public class Description
    {
        public int Id { get; }
        public string Main { get; }
        public string Details { get; }
        public string Icon { get; }

        public Description(int id, string main, string details, string icon)
        {
            Id = id;
            Main = main;
            Details = details;
            Icon = icon;
        }
    }

    public class Main
    {
        public double Temp { get; }
        public double Pressure { get; }
        public double Humidity { get; }

        public Main(double temp, double pressure, double humidity)
        {
            Temp = temp;
            Pressure = pressure;
            Humidity = humidity;
        }
    }
}
