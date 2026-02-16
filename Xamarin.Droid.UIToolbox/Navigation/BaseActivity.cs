using Android.OS;
using AndroidX.AppCompat.App;

namespace Xamarin.Droid.UIToolbox.Navigation
{
    public class BaseActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AskForPermission();
        }

        protected virtual void AskForPermission()
        {
        }
    }
}