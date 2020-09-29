using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Xamarin.Droid.UIToolbox.Views;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public class ActivityItemCell : PickerDialogCell<int>
    {
        private readonly IList<ActivityDialog.ActivityItem> _possibleValues;

        public ActivityItemCell(int tag, FormFragment form, string hint,
            IList<ActivityDialog.ActivityItem> possibileValue, int initialValue) : base(tag, form, hint, initialValue)
        {
            _possibleValues = possibileValue;
        }

        private ActivityDialog.ActivityItem GetItem(int tag)
        {
            return _possibleValues.FirstOrDefault(x => x.Tag == tag);
        }

        protected override string GetStringValue(int value, Context context)
        {
            return context.GetString(GetItem(value).TitleRes);
        }

        protected override Dialog CreateDialog(Context c, int value)
        {
            var a = new ActivityDialog(c, _possibleValues, Hint, 3);
            a.ActivitySelected += OnActivitySelected;
            return a;
        }

        private void OnActivitySelected(object sender, ActivityDialog.ActivityItem e)
        {
            Value = e.Tag;
            DismissDialog(true);
        }
    }
}