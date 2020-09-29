using Android.App;
using Android.Content;
using Xamarin.Droid.UIToolbox.Form;
using Xamarin.Droid.UIToolbox.Views;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public class NumberPickerCell : PickerDialogCell<int>
    {
        public NumberPickerCell(int tag, FormFragment form, string hint, int initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 100;

        protected override string GetStringValue(int value, Context context)
        {
            return $"{value}";
        }

        protected override Dialog CreateDialog(Context c, int value)
        {
            var dialog = new NumberPickerDialog(c)
            {
                Picker = {MaxValue = Max, MinValue = Min, Value = Value}
            };



            dialog.ValueSelected += (sender, i) =>
            {
                Value = i;
                DismissDialog(true);
            };

            return dialog;
        }
    }
}