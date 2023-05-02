using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Demo.ViewModel;
using Xamarin.Droid.UIToolbox.Fragments;
using Xamarin.Droid.UIToolbox.List;

namespace Demo.Fragments
{
    public class ToDoListFragment : SmartListFragment<Todo>
    {
        public interface ITodoSelectedHandler
        {
            public void TodoSelected(object sender, Todo item);
        }
        private ToDoViewModel ViewModel => ToDoViewModel.Instance;

        #region SmartList overrides

        /// [1] Provide here the collection of item to display
        public override IList<Todo> Elements => ViewModel.Todos;

        /// [2] Provide the layout template you want to use to reprensent the item at the given position
        /// Here, we decide to use the same template for all the record, but you can make a different choice
        public override int GetCellResource(int position) => Android.Resource.Layout.SimpleListItem1;
        
        /// [3] Once a cell is created, it gets associated with a view holder.
        /// Associate the cell's pertinent views to the view holder properties
        public override void RegisterCell(View cell, ViewHolder vh)
        {
            vh.Views["Text1"] = cell.FindViewById(Android.Resource.Id.Text1);
            vh.Views["Text2"] = cell.FindViewById(Android.Resource.Id.Text2);
        }
        
        /// [4] This method is called when a cell is ready to render a member of the list
        /// you receive the item that is to be displayed and the cell viewHolder.
        /// Map the data in the view you saved in [3]
        public override void Bind(ViewHolder viewHolder, Todo item)
        {
            viewHolder.getView<TextView>("Text1").Text = item.Description;
            viewHolder.getView<TextView>("Text2").Text = item.Status.ToString();
        }

        
        /// you can react to an item being selected. For example by notifying the parent activity
        /// Here, for simplicity purpose we send a notification to the parent activity so it can trigger a navigation event, and set store the selected item in the viewModel
        protected override void OnItemSelected(object sender, Todo e)
        {
            //ViewModel.SetEditedTodo(e);
            //((ITodoSelectedHandler)Activity)?.TodoSelected(this, e);
        }

        #endregion
       
    }
}