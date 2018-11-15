using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Toolbox.Droid.Form;
using Toolbox.Droid.Form.Cells;
using Toolbox.Droid.Views;

namespace Toolbox.Droid.Login
{
    public interface ILoginFragmentDelegate
    {
        Drawable Logo { get; }
        bool CheckLoginInput(string email, string password);

        /// <summary>
        ///     Perform login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>a task with the error code value. success = 0, any other value will be interpretated as a failure</returns>
        Task<int> LoginWithCredentials(string email, string password);

        string GetAuthenticationErrorText(int errorCode);

        void OnLoginSuccess();

        void onCreateAccountRequest();

        void onForgottenPasswordRequest();
    }

    public class LoginFragment : FormFragment
    {
        public enum Fields
        {
            Email,
            Password
        }

        private readonly Dictionary<int, string> ValueHolder = new Dictionary<int, string>();

        public AppCompatButton LoginButton { get; set; }

        protected override void ConfigureForm()
        {
            var logo = new ImageView(Activity);
            logo.SetImageDrawable((Activity as ILoginFragmentDelegate).Logo);
            logo.LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, Pixels.FromDP(72, Resources))
                {
                    Gravity = GravityFlags.CenterHorizontal,
                    BottomMargin = Pixels.FromDP(24, Resources)
                };
            InsertCustomView(logo);

            AddRow(new InputCell((int) Fields.Email, this, "email", "", InputTypes.ClassText));

            AddRow(new InputCell((int) Fields.Password, this, "password", "", InputTypes.TextVariationPassword));

            var forgottenPassword = new TextView(Activity);
            forgottenPassword.Gravity = GravityFlags.Center;
            forgottenPassword.Text = "Forgot your password";
            forgottenPassword.TextSize = 14;
            forgottenPassword.LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = GravityFlags.CenterHorizontal,
                    BottomMargin = Pixels.FromDP(24, Resources)
                };
            forgottenPassword.Click += OnForgottenPassword;
            InsertCustomView(forgottenPassword);

            /*
                android:padding="12dp"
                android:text="Login"/>
             */

            LoginButton = new AppCompatButton(Activity);
            LoginButton.Gravity = GravityFlags.Center;
            LoginButton.Text = "Log in";
            LoginButton.TextSize = 16;
            LoginButton.SetPadding(Pixels.FromDP(12, Resources), Pixels.FromDP(12, Resources),
                Pixels.FromDP(12, Resources), Pixels.FromDP(12, Resources));
            LoginButton.LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = GravityFlags.CenterHorizontal,
                    BottomMargin = Pixels.FromDP(24, Resources),
                    TopMargin = Pixels.FromDP(24, Resources)
                };
            LoginButton.Click += OnLoginRequest;
            InsertCustomView(LoginButton);

            var createAccount = new TextView(Activity);
            createAccount.Gravity = GravityFlags.Center;
            createAccount.Text = "No account yet? Create one!";
            createAccount.TextSize = 14;
            createAccount.LayoutParameters =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent)
                {
                    Gravity = GravityFlags.CenterHorizontal,
                    BottomMargin = Pixels.FromDP(24, Resources)
                };
            createAccount.Click += OnSignUpRequest;
            InsertCustomView(createAccount);
        }

        public override void OnCellDataChanged(Cell cell, object newValue)
        {
            ValueHolder[cell.Tag] = (string) newValue;
        }


        private void OnForgottenPassword(object sender, EventArgs e)
        {
            ((ILoginFragmentDelegate) Activity).onForgottenPasswordRequest();
        }


        private void OnSignUpRequest(object sender, EventArgs e)
        {
            ((ILoginFragmentDelegate) Activity).onCreateAccountRequest();
        }

        private async void OnLoginRequest(object sender, EventArgs e)
        {
            var inputOK = (Activity as ILoginFragmentDelegate)?.CheckLoginInput(ValueHolder[(int) Fields.Email],
                              ValueHolder[(int) Fields.Password]) ?? false;

            if (!inputOK) return;

            LoginButton.Enabled = false;

            var task = (Activity as ILoginFragmentDelegate)?.LoginWithCredentials(ValueHolder[(int) Fields.Email],
                ValueHolder[(int) Fields.Password]);


            HUD.HUD.showSpinner(Activity, "Authenticating", task);

            var result = await task;

            if (result == 0)
                OnLoginSuccess();
            else
                OnLoginError(result);
        }


        private void OnLoginError(int errorCode)
        {
            HUD.HUD.showError(Activity,
                (Activity as ILoginFragmentDelegate)?.GetAuthenticationErrorText(errorCode) ?? "",
                HUD.HUD.DURATION_DEFAULT);
        }

        private void OnLoginSuccess()
        {
            (Activity as ILoginFragmentDelegate)?.OnLoginSuccess();
        }
    }
}