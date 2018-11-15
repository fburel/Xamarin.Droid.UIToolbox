using Android.Content;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
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
        public readonly string Hint;
        public Context _context;
        private T _value;
        public ITextFormatter<T> TextFormatter;

        public TextValueCell(int tag, FormFragment form, string hint, T initialValue) : base(tag, form)
        {
            Hint = hint;
            Value = initialValue;
        }

        private ViewHolder Holder { get; set; }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                if (Holder != null && _context != null) Holder.ValueLabel.Text = TextFormatter == null ? GetStringValue(_value, _context) : TextFormatter.GetStringValue(_value, _context);
            }
        }

        protected abstract string GetStringValue(T value, Context context);

        public override View GetView(Context context)
        {
            _context = context;

            var cell = OnCreateLayout(context);

            Holder = new ViewHolder(
                cell.FindViewById<TextView>(Resource.Id.hintTextView),
                cell.FindViewById<TextView>(Resource.Id.valueTextView)
            );

            Holder.TextLabel.Text = Hint;
            Holder.ValueLabel.Text = TextFormatter == null ? GetStringValue(Value, context) : TextFormatter.GetStringValue(Value, context);

            return cell;
        }

        public virtual View OnCreateLayout(Context context)
        {
            return LayoutInflater.From(context).Inflate(Resource.Layout.forms_cell_value, null);
        }

        private class ViewHolder
        {
            internal readonly TextView TextLabel;
            internal readonly TextView ValueLabel;

            internal ViewHolder(TextView textLabel, TextView valueLabel)
            {
                TextLabel = textLabel;
                ValueLabel = valueLabel;
            }
        }
    }
}