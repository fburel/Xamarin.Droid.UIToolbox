using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Xamarin.Droid.UIToolbox.Form;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public class SwitchCell : Cell
    {
        private readonly string _hint;
        private SwitchCompat _checkBox;
        private bool _value;

        public SwitchCell(int tag, FormFragment form, string hint, bool initialValue) : base(tag, form)
        {
            _hint = hint;
            _value = initialValue;
        }

        public override View GetView(Context context)
        {
            var cell = LayoutInflater.From(context).Inflate(Resource.Layout.forms_cell_checkbox, null);
            var label = cell.FindViewById<TextView>(Resource.Id.label);
            _checkBox = cell.FindViewById<SwitchCompat>(Resource.Id.checkbox);
            _checkBox.SetTextColor(AppearanceTextColor);
            _checkBox.SetHighlightColor(AppearanceAccentColor);
            label.Text = _hint;
            _checkBox.Checked = _value;
            _checkBox.Enabled = Form.EditingEnabled;
            _checkBox.CheckedChange += OnCheckBoxClicked;

            return cell;
        }

        private void OnCheckBoxClicked(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            SetValue(e.IsChecked);
        }

        public override void OnGainingFocus(Context c)
        {
            SetValue(!_value);
        }

        private void SetValue(bool b)
        {
            if (b == _value) return;
            _value = b;
            if (_checkBox != null) _checkBox.Checked = _value;
            NotifyChanged(_value);
        }

        protected override void OnEditableStatusChanged(object sender, bool isEditable)
        {
            _checkBox.Enabled = Form.EditingEnabled;
        }
    }
}