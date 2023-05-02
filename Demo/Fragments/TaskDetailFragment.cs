using System;
using Android.OS;
using Android.Text;
using Demo.ViewModel;
using Xamarin.Droid.UIToolbox.Form;
using Xamarin.Droid.UIToolbox.Form.Cells;
using EditableField = Demo.ViewModel.ToDoViewModel.EditableField;

namespace Demo.Fragments
{
    
    public class TaskDetailFragment : FormFragment
    {
        private ToDoViewModel ViewModel => ToDoViewModel.Instance;

        #region Adding a button to the navBar

        // [1] use the SetRightButtonItem, provide an icon.
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetRightButtonItem("save", Android.Resource.Drawable.IcMenuSave);
        }

        // [2] the `OnRightButtonItemClicked`will be called when the button is clicked
        protected override void OnRightButtonItemClicked()
        {
            ViewModel.Commit();
            
            // will navigate back (same as pressing the back button)
            Back();
        }

        #endregion
        
        #region FormFragmentoverrides

        /// [1] in this methods you should prepare the form
        /// use methode such as AddRow / InsertHeader / InsertFooter
        /// When adding cell, provide them with original value
        protected override void ConfigureForm()
        {
            // Inset a simple header 
            InsertHeader("Edit Task infos");
            
            // Add a text input row with the "description" label
            AddRow(new InputCell((int)ToDoViewModel.EditableField.Description, this, "Description", (string) ViewModel.EditingValues[EditableField.Description.ToString()],
                InputTypes.ClassText));
            
            // This footer will be like a hint below the description input
            InsertFooter("Provide a short description for your task");
            
            // This is one of our select cell, it present a modal view that provide choice between the given a list of choice
            // when a string is selected, the value that will be reported is its postion in the array.
            // If you want to have your choice illustrated with images, look at the ActivityItemCell
            AddRow(new DatePickerCell((int)EditableField.DeadLine,
                this,
                "Deadline",
                (DateTime) ViewModel.EditingValues[EditableField.DeadLine.ToString()]
            ));

            // This is a on / off selector
            AddRow(new SwitchCell((int)EditableField.Important, this, "important",
                (bool)ViewModel.EditingValues[EditableField.Important.ToString()]));
         

            // This is one of our select cell, it present a modal view that provide choice between the given a list of choice
            // when a string is selected, the value that will be reported is its postion in the array.
            // If you want to have your choice illustrated with images, look at the ActivityItemCell
            AddRow(new ListPickerCell((int)EditableField.Status,
                this,
                "Status",
                new string[]
                {
                    GetString(Resource.String.StatusOpen),  // = 0
                    GetString(Resource.String.StatusProgress), // = 1
                    GetString(Resource.String.StatusHold), // = 2
                    GetString(Resource.String.StatusDone), // = 3
                },
                (int) ViewModel.EditingValues[EditableField.Status.ToString()]));
        }

        /// [2] This method is called when a vell value change.
        /// Here we save the value in a temporary buffer
        public override void OnCellDataChanged(Cell cell, object newValue)
        {
            var tag = (EditableField)cell.Tag;
            ViewModel.EditingValues[tag.ToString()] = newValue;
        }
        

        #endregion
       
    }
}