using System;
using System.Collections.Generic;
using Android.Text;
using Demo.ViewModel;
using Xamarin.Droid.UIToolbox.Form;
using Xamarin.Droid.UIToolbox.Form.Cells;
using Xamarin.Droid.UIToolbox.Views;

namespace Demo.Fragments
{
    
    public class TaskDetailFragment : FormFragment
    {
        public enum CellTags
        {
            Description,
            Status
        }
        private ToDoViewModel ViewModel => ToDoViewModel.Instance;

        #region FormFragmentoverrides

        /// [1] in this methods you should prepare the form
        /// use methode such as AddRow / InsertHeader / InsertFooter
        /// When adding cell, provide them with original value
        protected override void ConfigureForm()
        {
            // Inset a simple header 
            InsertHeader("Edit Task infos");
            
            // Add a text input row with the "description" label
            AddRow(new InputCell((int)CellTags.Description, this, "Description", ViewModel.EditingTask.Description,
                InputTypes.ClassText));
            
            // This footer will be like a hint below the description input
            InsertFooter("Provide a short description for your task");
            
            // This is one of our select cell, it present a modal view that provide choice between the given ActivityItem
            AddRow(new ActivityItemCell((int)CellTags.Status, this, "Status", new List<ActivityDialog.ActivityItem>()
            {
                new ActivityDialog.ActivityItem(0, Resource.String.StatusOpen, Android.Resource.Drawable.IcLockIdleLock),
                new ActivityDialog.ActivityItem(0, Resource.String.StatusProgress, Android.Resource.Drawable.IcLockIdleLock),
                new ActivityDialog.ActivityItem(0, Resource.String.StatusHold, Android.Resource.Drawable.IcLockIdleLock),
                new ActivityDialog.ActivityItem(0, Resource.String.StatusDone, Android.Resource.Drawable.IcLockIdleLock)
            }, (int) ViewModel.EditingTask.Status));
            
        }

        /// [2] This method is called when a vell value change.
        /// Here we ask the wiewModel to update the object, but you can set a temporary buffer with a save button
        public override void OnCellDataChanged(Cell cell, object newValue)
        {
            switch ((CellTags)cell.Tag)
            {
                case CellTags.Description:
                    ViewModel.EditingTask.Description = (string) newValue;
                    break;
                case CellTags.Status:
                    ViewModel.EditingTask.Status =  (Todo.TaskStatus) newValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
       
    }
}