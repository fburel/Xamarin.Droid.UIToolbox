using System;
using Android.App;
using Android.Content;

namespace Toolbox.Droid.Form.Cells
{
    public class DatePickerCell : PickerDialogCell<DateTime>
    {
        public DatePickerCell(int tag, FormFragment form, string hint, DateTime initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        public DateTime MaxDate { get; set; } = DateTime.MinValue;

        public DateTime MinDate { get; set; } = DateTime.MaxValue;


        protected override string GetStringValue(DateTime value, Context context)
        {
            return value.ToString("dd/MM/yyyy");
        }

        protected override Dialog CreateDialog(Context c, DateTime value)
        {
            var year = value.Year;
            var month = value.Month;
            var day = value.Day;

            var picker = new DatePickerDialog(c);
            picker.DatePicker.MinDate = MinDate.Millisecond;
            picker.DatePicker.MaxDate = MaxDate.Millisecond;
            picker.DatePicker.UpdateDate(year, month, day);
            picker.DateSet += OnDateChanged;
            return picker;
        }


        private void OnDateChanged(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            Value = e.Date;
            DismissDialog(true);
        }
    }
}