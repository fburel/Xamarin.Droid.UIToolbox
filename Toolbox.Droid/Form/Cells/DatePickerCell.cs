using System;
using Android.App;
using Android.Content;
using Java.Util;
using TimeZone = Java.Util.TimeZone;

namespace Toolbox.Droid.Form.Cells
{
    public class DatePickerCell : PickerDialogCell<DateTime>
    {
        public DatePickerCell(int tag, FormFragment form, string hint, DateTime initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        public DateTime MaxDate { get; set; } = new DateTime(2070, 1, 1, 0, 0, 0);

        public DateTime MinDate { get; set; } = new DateTime(1970, 1, 1, 0, 0, 0);


        protected override string GetStringValue(DateTime value, Context context)
        {
            return value.ToString("dd/MM/yyyy");
        }

        protected override Dialog CreateDialog(Context c, DateTime value)
        {
            var year = value.Year;
            var month = value.Month - 1; // android start month at 0
            var day = value.Day;

            var calendar = Calendar.GetInstance(TimeZone.Default);
            calendar.Set(CalendarField.Year, MinDate.Year);
            calendar.Set(CalendarField.Month, MinDate.Month - 1);
            calendar.Set(CalendarField.DayOfMonth, MinDate.Day);
            var minDate = calendar.TimeInMillis;

            calendar.Set(CalendarField.Year, MaxDate.Year);
            calendar.Set(CalendarField.Month, MaxDate.Month - 1);
            calendar.Set(CalendarField.DayOfMonth, MaxDate.Day);
            var maxDate = calendar.TimeInMillis;

            var picker = new DatePickerDialog(c, OnDateChanged, year, month, day);
            picker.DatePicker.MinDate = minDate;
            picker.DatePicker.MaxDate = maxDate;
            return picker;
        }


        private void OnDateChanged(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            Value = e.Date;
            DismissDialog(true);
        }
    }
}