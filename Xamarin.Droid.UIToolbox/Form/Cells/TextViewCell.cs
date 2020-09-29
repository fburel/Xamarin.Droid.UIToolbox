using Android.Content;
using Android.Text;
using Android.Views;
using Android.Widget;
using Xamarin.Droid.UIToolbox.Form;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public class TextViewCell : Cell
    {
        private readonly string _value;

        public TextViewCell(int tag, FormFragment form, string text) : base(tag, form)
        {
            _value = text;
        }

        public bool Editable { get; set; } = true;

        public override View GetView(Context context)
        {
            var cell = OnCreateLayout(context);

            var editText = cell.FindViewById<EditText>(Resource.Id.EditText);

            editText.AfterTextChanged += AfterTextChanged;

            editText.Enabled = Editable;

            editText.Text = _value;

            return cell;
        }

        private void AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            NotifyChanged(e.Editable.ToString());
        }

        private View OnCreateLayout(Context context)
        {
            return LayoutInflater.From(context).Inflate(Resource.Layout.TextViewCell, null);
        }
    }
}