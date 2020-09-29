using Android.Content;
using Android.Views;
using AndroidX.AppCompat.Widget;
using Xamarin.Droid.UIToolbox.Form;

namespace Xamarin.Droid.UIToolbox.Form.Cells
{
    public interface ITextFormatter<T>
    {
        string GetStringValue(T value, Context context);
    }

    /// <summary>
    ///     This class represent a cell that will display both a hint phrase and a value as text
    /// </summary>
    public abstract class TextValueCell<T> : Cell
    {
        protected readonly string Hint;
        private Context _context;
        private T _value;
        public ITextFormatter<T> TextFormatter;

        protected TextValueCell(int tag, FormFragment form, string hint, T initialValue) : base(tag, form)
        {
            Hint = hint;
            Value = initialValue;
        }

        private ViewHolder Holder { get; set; }

        protected T Value
        {
            get => _value;
            set
            {
                _value = value;
                if (Holder != null && _context != null)
                    Holder.ValueLabel.Text = TextFormatter == null
                        ? GetStringValue(_value, _context)
                        : TextFormatter.GetStringValue(_value, _context);
            }
        }

        protected abstract string GetStringValue(T value, Context context);

        public override View GetView(Context context)
        {
            _context = context;

            var cell = OnCreateLayout(context);

            Holder = new ViewHolder(
                cell.FindViewById<AppCompatTextView>(Resource.Id.hintTextView),
                cell.FindViewById<AppCompatTextView>(Resource.Id.valueTextView)
            );

            Holder.TextLabel.SetTextColor(AppearanceTextColor);
            Holder.ValueLabel.SetTextColor(AppearanceAccentColor);

            Holder.TextLabel.Text = Hint;
            Holder.ValueLabel.Text = TextFormatter == null
                ? GetStringValue(Value, context)
                : TextFormatter.GetStringValue(Value, context);

            return cell;
        }

        protected virtual View OnCreateLayout(Context context)
        {
            return LayoutInflater.From(context).Inflate(Resource.Layout.forms_cell_value, null);
        }

        private class ViewHolder
        {
            internal readonly AppCompatTextView TextLabel;
            internal readonly AppCompatTextView ValueLabel;

            internal ViewHolder(AppCompatTextView textLabel, AppCompatTextView valueLabel)
            {
                TextLabel = textLabel;
                ValueLabel = valueLabel;
            }
        }
    }
}