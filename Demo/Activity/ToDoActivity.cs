using System;
using Android.App;
using Android.OS;
using Demo.Fragments;
using Demo.ViewModel;
using Xamarin.Droid.UIToolbox.Navigation;

namespace Demo
{
    /// <summary>
    /// The PushPop Activity class allows a fragment to fragment navigation.
    /// The purpose of the activity is to switch from one fragment to another.
    /// </summary>
    [Activity(Label = "Demo", MainLauncher = true)]
    public class ToDoActivity : PushPopActivity
    {
        protected override Type GetRootFragmentClass => typeof(ToDoListFragment);

        //public void TodoSelected(object sender, Todo item)
        //{
        //    Push<TaskDetailFragment>();
        //}
    }
    
}