using Android.App;
using Android.Content;
using Xamarin.Droid.UIToolbox.Form;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public struct TimeComponent
    {
        public int Hours;
        public int Minutes;
    }

    public class TimePickerCell : PickerDialogCell<TimeComponent>
    {
        public TimePickerCell(int tag, FormFragment form, string hint, TimeComponent initialValue) : base(tag, form,
            hint, initialValue)
        {
        }

        protected override string GetStringValue(TimeComponent value, Context context)
        {
            return $"{value.Hours:00}:{value.Minutes:00}";
        }


        protected override Dialog CreateDialog(Context c, TimeComponent value)
        {
            Dialog dialog = new TimePickerDialog(c, OnValueChanged, value.Hours, value.Minutes, true);
            return dialog;
        }

        private void OnValueChanged(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            Value = new TimeComponent {Hours = e.HourOfDay, Minutes = e.Minute};
            DismissDialog(true);
        }
    }
}