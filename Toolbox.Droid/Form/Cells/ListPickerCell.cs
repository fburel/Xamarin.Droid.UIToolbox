using Android.App;
using Android.Content;

namespace Toolbox.Droid.Form.Cells
{
    public class ListPickerCell : PickerDialogCell<int>
    {
        private readonly string[] _item;

        public ListPickerCell(int tag, FormFragment form, string hint, string[] possibleValues, int initialValuePos) :
            base(tag, form, hint, initialValuePos)
        {
            _item = possibleValues;
        }

        protected override string GetStringValue(int value, Context context)
        {
            return _item[value];
        }

        protected override Dialog CreateDialog(Context c, int value)
        {
            return new AlertDialog.Builder(c)
                .SetItems(_item, OnItemSelected)
                .Create();
        }

        private void OnItemSelected(object sender, DialogClickEventArgs e)
        {
            Value = e.Which;
            DismissDialog(true);
        }
    }
}