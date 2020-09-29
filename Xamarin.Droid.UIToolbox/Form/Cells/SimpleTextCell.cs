using Android.Content;
using Xamarin.Droid.UIToolbox.Form;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public class SimpleTextCell : TextValueCell<string>
    {
        public SimpleTextCell(int tag, FormFragment form, string hint, string initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        protected override string GetStringValue(string value, Context context)
        {
            return value;
        }
    }
}