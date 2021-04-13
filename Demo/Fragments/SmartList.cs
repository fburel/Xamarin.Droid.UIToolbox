using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Views;
using Android.Widget;
using Xamarin.Droid.UIToolbox.Fragments;
using Xamarin.Droid.UIToolbox.List;

namespace Demo.Fragments
{
    public class People
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public static IList<People> Everybody => new List<People>()
        {
            new People()
            {
                FirstName = "John",
                LastName = "Doe"
            },
            new People(){
                FirstName = "Paul",
                LastName = "Appleseed"
            },
            new People(){
                FirstName = "Chris",
                LastName = "Shutterman"
            }
        };
    }
    

    public class SmartList : SmartListFragment<People>
    {
        /// Provide here the collection of itme to display
        public override IList<People> Elements => People.Everybody;

        /// Provide the Layout resource you wish to use
        public override int GetCellResource(int position) => Android.Resource.Layout.SimpleListItem1;
        
        /// Once a cell is created, it gets associated with a view holder.
        /// Associate the cell's view to the view holder properties
        public override void RegisterCell(View cell, ViewHolder vh)
        {
            vh.Views["Text1"] = cell.FindViewById(Android.Resource.Id.Text1);
            vh.Views["Text2"] = cell.FindViewById(Android.Resource.Id.Text1);
        }
        
        public override void Bind(ViewHolder viewHolder, People item)
        {
            viewHolder.getView<TextView>("Text1").Text = item.FirstName;
            viewHolder.getView<TextView>("Text2").Text = item.LastName;
        }

        protected override void OnItemSelected(object sender, People e)
        {
            
        }
    }
}