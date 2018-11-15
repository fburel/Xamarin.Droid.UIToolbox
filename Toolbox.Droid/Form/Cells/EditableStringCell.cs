using Android.App;
using Android.Content;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
{
    public class EditableStringCell : PickerDialogCell<string>
    {
        public EditableStringCell(int tag, FormFragment form, string hint, string initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        protected override string GetStringValue(string value, Context context)
        {
            return value;
        }

        protected override Dialog CreateDialog(Context c, string value)
        {
            var builder = new AlertDialog.Builder(c);

            var input = new EditText(c) {Text = value};
            builder.SetView(input);

            builder.SetPositiveButton("OK", (s, e) =>
            {
                Value = input.Text;
                DismissDialog(true);
            });

            builder.SetNegativeButton("Cancel", (s, e) => { DismissDialog(false); });

            return builder.Create();
        }
    }
}