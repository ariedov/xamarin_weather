using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Gms.Location;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Graphics.Drawable;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using XamarinWeather.Droid.Location;
using XamarinWeather.Shared.ViewModel;
using XamarinWeather.Shared.Weather;
using XamarinWeather.WeatherProvider.Provider;

namespace XamarinWeather.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.SensorPortrait, MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        FusedLocationProviderClient locationProvider;

        WeatherViewModel viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var iconView = FindViewById<ImageView>(Resource.Id.icon);
            var nameView = FindViewById<TextView>(Resource.Id.name);
            var temperatureView = FindViewById<TextView>(Resource.Id.temperature);

            locationProvider = LocationServices.GetFusedLocationProviderClient(this);

            var provider = new WeatherViewModelProvider(new LocationProvider(), new DefaultWeatherProvider());
            var iconConverter = new WeatherIconConverter();
            viewModel = new WeatherViewModel(provider, iconConverter);
            viewModel.DataChanged += data =>
            {
                nameView.Text = data.Name;
                temperatureView.Text = $"{Convert.ToInt32(data.Temp)}\u2103";

                Drawable icon = null;
                switch (data.IconType)
                {
                    case WeatherIcon.CLEAR_SKY_DAY:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.d01, null);
                        break;
                    case WeatherIcon.CLEAR_SKY_NIGHT:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.n01, null);
                        break;
                    case WeatherIcon.FEW_CLOUDS_DAY:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.d02, null);
                        break;
                    case WeatherIcon.FEW_CLOUDS_NIGHT:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.n02, null);
                        break;
                    case WeatherIcon.SCATTERED_CLOUDS:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m03, null);
                        break;
                    case WeatherIcon.BROKEN_CLOUDS:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m04, null);
                        break;
                    case WeatherIcon.SHOWER_RAIN:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m09, null);
                        break;
                    case WeatherIcon.RAIN_DAY:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.d10, null);
                        break;
                    case WeatherIcon.RAIN_NIGHT:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.n10, null);
                        break;
                    case WeatherIcon.THUNDERSTORM:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m11, null);
                        break;
                    case WeatherIcon.SNOW:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m13, null);
                        break;
                    case WeatherIcon.MIST:
                        icon = VectorDrawableCompat.Create(Resources, Resource.Drawable.m50, null);
                        break;
                }
                iconView.SetImageDrawable(icon);

            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            viewModel.StartWeatherLoading();
        }
    }
}
