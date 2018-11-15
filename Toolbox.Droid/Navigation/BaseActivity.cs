using Android.OS;
using Android.Support.V7.App;

namespace Toolbox.Droid.Navigation
{
    public class BaseActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AskForPermission();
        }

        protected virtual void AskForPermission()
        {
        }
    }
}