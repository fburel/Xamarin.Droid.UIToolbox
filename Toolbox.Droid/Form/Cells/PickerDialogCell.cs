using System;
using Android.App;
using Android.Content;

namespace Toolbox.Droid.Form.Cells
{
    public abstract class PickerDialogCell<T> : TextValueCell<T>
    {
        protected PickerDialogCell(int tag, FormFragment form, string hint, T initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        private Dialog Dialog { get; set; }

        protected abstract Dialog CreateDialog(Context c, T value);

        public override void OnGainingFocus(Context c)
        {
            base.OnGainingFocus(c);

            if (Form.EditingEnabled == false) return;

            Dialog = CreateDialog(c, Value);
            Dialog.SetTitle(Hint);
            Dialog.DismissEvent += OnDismiss;
            Dialog.Show();
        }

        protected void DismissDialog(bool notifySuccessfullChange)
        {
            Dialog.Dismiss();
            if (notifySuccessfullChange) NotifyChanged(Value);
        }

        private void OnDismiss(object sender, EventArgs e)
        {
            Dialog = null;
        }
    }
}