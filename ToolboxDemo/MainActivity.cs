using System;
using Android.App;
using ToolboxDemo.Fragments;
using Xamarin.Droid.UIToolbox.Navigation;

namespace ToolboxDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : PushPopActivity
    {
        protected override Type GetRootFragmentClass => typeof(CourseFragment);
    }
}