using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
{
    public sealed class ButtonCell : Cell
    {
        public ButtonCell(int tag, FormFragment form, string buttonTile) : base(tag, form)
        {
            ButtonTitle = buttonTile;
        }

        private string ButtonTitle { get; }


        public override View GetView(Context context)
        {
            var cell = OnCreateLayout(context);

            var btn = cell.FindViewById<Button>(Resource.Id.button);

            btn.Click += OnClick;

            btn.Text = ButtonTitle;

            return cell;
        }

        private void OnClick(object sender, EventArgs e)
        {
            NotifyChanged(true);
        }

        public View OnCreateLayout(Context context)
        {
            return LayoutInflater.From(context).Inflate(Resource.Layout.ButtonCell, null);
        }
    }
}