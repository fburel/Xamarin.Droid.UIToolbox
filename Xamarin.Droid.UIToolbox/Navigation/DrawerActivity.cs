using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.Navigation;
using Xamarin.Droid.UIToolbox.Navigation.DrawerToogle;

namespace Xamarin.Droid.UIToolbox.Navigation
{
    public abstract class DrawerActivity : BaseActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private DrawerLayout _drawer;

        private ActionBarDrawerToggle _drawerToggle;

        #region NavigationView.IOnNavigationItemSelectedListener

        bool NavigationView.IOnNavigationItemSelectedListener.OnNavigationItemSelected(IMenuItem menuItem)
        {
            OnSideMenuItemSelected(menuItem);

            _drawer.CloseDrawers();

            return true;
        }

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create the UI
            var v = OnCreateMainView(savedInstanceState);
            _drawer = new DrawerLayout(this);
            _drawer.AddView(v,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            AddContentView(_drawer,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));

            // prep drawer if needed
            var navigationView = new NavigationView(this);
            var menu = navigationView.Menu;
            if (OnPrepareSideMenu(menu))
            {
                navigationView.SetNavigationItemSelectedListener(this);
                _drawer.AddView(navigationView,
                    new DrawerLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                        ViewGroup.LayoutParams.MatchParent, (int) GravityFlags.Start));

                // set the header
                var header = OnCreateMenuHeaderView(LayoutInflater.From(this));
                if (header != null) navigationView.AddHeaderView(header);

                // fetch the theme... 
                var a = new TypedValue();
                Theme.ResolveAttribute(global::Android.Resource.Attribute.ColorBackground, a, true);
                if (a.Type >= DataType.FirstColorInt && a.Type <= DataType.LastColorInt)
                    navigationView.SetBackgroundColor(new global::Android.Graphics.Color(a.Data));

                // notify when drawer opens / closes
                _drawerToggle = OnCreateDrawerToggle(this, _drawer);
                _drawer.AddDrawerListener(_drawerToggle);

                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        /* Called whenever we call invalidateOptionsMenu() */
        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            // If the nav drawer is open, hide action items related to the content view
            if (_drawerToggle != null && _drawer.IsDrawerOpen((int) GravityFlags.Start)) // 0x00800003 === Gravity.START
                menu.Clear();

            return base.OnPrepareOptionsMenu(menu);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            // Sync the toggle state after onRestoreInstanceState has occurred.
            _drawerToggle?.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            _drawerToggle?.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Pass the event to ActionBarDrawerToggle, if it returns
            // true, then it has handled the app icon touch event
            if (_drawerToggle != null && _drawerToggle.OnOptionsItemSelected(item)) return true;
            // Handle your other action bar items...
            return base.OnOptionsItemSelected(item);
        }


        #region override for custumization

        /// <summary>
        ///     Override to return the main view of the activity
        /// </summary>
        protected abstract View OnCreateMainView(Bundle savedInstanceState);

        /// <summary>
        ///     The DrawerToogle class that will be used with de drawerLAyout. Default is a BasicDrawerToogle
        /// </summary>
        protected virtual ActionBarDrawerToggle OnCreateDrawerToggle(DrawerActivity activity, DrawerLayout drawer)
        {
            return new BasicDrawerToogle(this, drawer, "Menu");
        }

        /// <summary>
        ///     Override if you want to customize the header view of the menu. Default is null
        /// </summary>
        protected virtual View OnCreateMenuHeaderView(LayoutInflater inflater)
        {
            return null;
        }

        /// <summary>
        ///     Prepare the Side menu
        /// </summary>
        /// <returns>true if the sideMenu should appear, false otherwise. Default is false</returns>
        protected virtual bool OnPrepareSideMenu(IMenu menu)
        {
            return false;
        }

        protected virtual void OnSideMenuItemSelected(IMenuItem item)
        {
        }

        #endregion
    }
}