using System;
using Android.App;
using Android.Content;
using Android.Widget;

namespace Xamarin.Droid.UIToolbox.Views
{
    public class NumberPickerDialog : Dialog
    {
        public NumberPickerDialog(Context context) : base(context)
        {
            SetContentView(Resource.Layout.number_picker_dialog);

            var _cancelButton = FindViewById<Button>(Resource.Id.buttonCancel);
            var _okButton = FindViewById<Button>(Resource.Id.buttonSet);
            Picker = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            Picker.WrapSelectorWheel = false;

            _cancelButton.Click += (sender, args) => { Dismiss(); };

            _okButton.Click += (sender, args) =>
            {
                ValueSelected?.Invoke(this, Value);
                Dismiss();
            };
            Picker.ValueChanged += (sender, args) => { Value = args.NewVal; };
        }

        public int Value { get; set; }
        public NumberPicker Picker { get; set; }
        public event EventHandler<int> ValueSelected;
    }
}