using Android.Content;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
{
    public class SliderCell : TextValueCell<float>
    {
        public SliderCell(int tag, FormFragment form, string hint, float initialValue) : base(tag, form, hint,
            initialValue)
        {
        }

        public float Min { get; set; } = 0;

        public float Max { get; set; } = 100;

        protected override string GetStringValue(float value, Context context)
        {
            return "tot";
        }

        public override View OnCreateLayout(Context context)
        {
            var v = LayoutInflater.From(context).Inflate(Resource.Layout.SliderCellLayout, null);

            var seekbar = v.FindViewById<SeekBar>(Resource.Id.seekBar);

            seekbar.Progress = (int) ((Value - Min) * 100 / (Max - Min));

            seekbar.ProgressChanged += OnProgressChanged;

            return v;
        }


        private void OnProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            Value = Min + e.Progress * (Max - Min) / 100;
            NotifyChanged(Value);
        }
    }
}