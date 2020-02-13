using Android.Content;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
{
    public class InputCell : Cell
    {
        private readonly string _hint;

        private readonly InputTypes _inputType;
        private string _value;

        private EditText _input;

        public InputCell(int tag, FormFragment form, string hint, string value, InputTypes inputType) : base(tag, form)
        {
            _hint = hint;
            _value = value;
            _inputType = inputType;
        }

        public override View GetView(Context context)
        {
            _input = new EditText(context);
            _input.InputType = _inputType;
            _input.Hint = _hint;
            _input.Text = _value;

            //editText.LayoutParameters = new TextInputLayout.LayoutParams(0, TextInputLayout.LayoutParams.MatchParent);

            var textInputLayout = new TextInputLayout(context);
            textInputLayout.AddView(_input);

            _input.TextChanged += OnTextChanged;
            _input.Enabled = Form.EditingEnabled;
            return textInputLayout;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _value = string.Concat(e.Text);
            NotifyChanged(_value);
        }

        protected override void OnEditableStatusChanged(object sender, bool isEditable)
        {
            _input.Enabled = Form.EditingEnabled;
        }
    }
}