using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Security;
using Xamarin.Droid.UIToolbox.Fragments;
using Xamarin.Droid.UIToolbox.List;

namespace ToolboxDemo.Fragments
{
    public class Course
    {
        public string Name { get; private set; }
        public int Duration { get; private set; }

        public Course(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
    }
    public sealed class DataProvider
    {
        public IList<Course> Data { get; private set; }

        private DataProvider()
        {
            this.Data = new List<Course>()
            {
                new Course("Xamarin 101", 5),
                new Course("Introduction to Nuxt3", 5),
                new Course("Nuxt3 Advanced Course", 5),
                new Course("Advanced Xamarin Course", 5),
                new Course("MongoDB", 5),
                new Course("Introduction to Machine learning", 5),
                new Course("Creating your own Neural Networks", 5),
            };
        }
        
        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static DataProvider _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static DataProvider GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataProvider();
            }
            return _instance;
        }

    }
    public class CourseFragment : SmartListFragment<Course>
    {
        protected override string Title => "Courses";
        public override IList<Course> Elements => DataProvider.GetInstance().Data;
        
        public override int GetCellResource(int position)
        {
            return Android.Resource.Layout.SimpleListItem1;
        }
        
        public override void RegisterCell(View cell, ViewHolder vh)
        {
            vh.Views.Add("text", cell.FindViewById<TextView>(Android.Resource.Id.Text1));
        }
        
        public override void Bind(ViewHolder viewHolde, Course item)
        {
            viewHolde.getView<TextView>("text").Text = item.Name;
        }

        protected override void OnItemSelected(object sender, Course e)
        {
            
        }

       

        
    }
}