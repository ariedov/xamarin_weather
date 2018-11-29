using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace XamarinWeather.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.SensorPortrait, MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        FusedLocationProviderClient locationProvider;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            locationProvider = LocationServices.GetFusedLocationProviderClient(this);
        }
    }
}
