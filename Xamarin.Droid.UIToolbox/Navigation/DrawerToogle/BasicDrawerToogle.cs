using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.DrawerLayout.Widget;


namespace Xamarin.Droid.UIToolbox.Navigation.DrawerToogle
{
    public class BasicDrawerToogle : ActionBarDrawerToggle
    {
        private readonly DrawerActivity _activity;
        private readonly string _drawerTitle;

        public BasicDrawerToogle(DrawerActivity activity, DrawerLayout drawerLayout, string drawerTitle) : base(
            activity, drawerLayout, 0, 0)
        {
            _activity = activity;
            _drawerTitle = drawerTitle;
        }

        public override void OnDrawerClosed(View? drawerView)
        {
            base.OnDrawerClosed(drawerView);
            
            if (_activity.SupportActionBar != null)
                _activity.SupportActionBar.Title = _activity.Title; // Change the title
            
            _activity.InvalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
        }

        public override void OnDrawerOpened(View? drawerView)
        {
            base.OnDrawerOpened(drawerView);
            
            if (_activity.SupportActionBar != null)
                _activity.SupportActionBar.Title = _drawerTitle;
            
            _activity.InvalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
        }
    }
}