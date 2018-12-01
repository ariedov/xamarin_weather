using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using XamarinWeather.Droid.Location;
using XamarinWeather.Shared.ViewModel;
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

            var temperatureView = FindViewById<TextView>(Resource.Id.temperature);

            locationProvider = LocationServices.GetFusedLocationProviderClient(this);

            var provider = new WeatherViewModelProvider(new LocationProvider(), new DefaultWeatherProvider());
            viewModel = new WeatherViewModel(provider);
            viewModel.DataChanged += data =>
            {
                temperatureView.Text = data.Temp.ToString();
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            viewModel.StartWeatherLoading();
        }
    }
}
