using System;
using Android.Content;
using Android.Renderscripts;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.Form.Cells
{
    public class InputCell : Cell
    {
        
        private InputTypes _inputType;
        private string _hint;
        private string _value;

        private EditText Input;

        public InputCell(int tag, FormFragment form, string hint, string value, InputTypes inputType) : base(tag, form)
        {
            _hint = hint;
            _value = value;
            _inputType = inputType;
        }

        public override View GetView(Context context)
        {
            Input = new EditText(context);
            Input.InputType = _inputType;
            Input.Hint = _hint;
            Input.Text = _value;
            
            //editText.LayoutParameters = new TextInputLayout.LayoutParams(0, TextInputLayout.LayoutParams.MatchParent);
            
            var textInputLayout = new TextInputLayout(context);
            textInputLayout.AddView(Input);

            Input.TextChanged += OnTextChanged;
            Input.Enabled = Form.EditingEnabled;
            return textInputLayout;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _value = string.Concat(e.Text);
            NotifyChanged(_value);
        }
        protected virtual void OnEditableStatusChanged(object sender, bool isEditable)
        {
            Input.Enabled = Form.EditingEnabled;
        }
    }
}