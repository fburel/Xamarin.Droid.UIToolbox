﻿using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace Toolbox.Droid.Navigation.DrawerToogle
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

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            _activity.SupportActionBar.Title = _activity.Title; // Change the title
            _activity.InvalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            _activity.SupportActionBar.Title = _drawerTitle;
            _activity.InvalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
        }
    }
}