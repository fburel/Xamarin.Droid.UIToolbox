using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using ActionMenuView = Android.Support.V7.Widget.ActionMenuView;

namespace Toolbox.Droid.HUD
{
    public class HUD
    {
        public static int DURATION_DEFAULT = 2000;

        public static Task showError(Context context, string text, int duration)
        {
            var dialog = new Dialog(context);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
            dialog.SetContentView(Resource.Layout.HUDLayout);
            var frameLayout = dialog.FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);
            var textView = dialog.FindViewById<TextView>(Resource.Id.textViewStatus);
            textView.Text = text;

            var imageView = new ImageView(context);
            imageView.SetImageResource(Resource.Drawable.ic_errorstatus);
            frameLayout.AddView(imageView,
                new ActionMenuView.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent));

            dialog.Show();

            var tm = Task.Delay(duration).ContinueWith(t => { dialog.Dismiss(); });
            return tm;
        }

        public static Task showSuccess(Context context, string text, int duration)
        {
            var dialog = new Dialog(context);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
            dialog.SetContentView(Resource.Layout.HUDLayout);
            var frameLayout = dialog.FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);
            var textView = dialog.FindViewById<TextView>(Resource.Id.textViewStatus);
            textView.Text = text;

            var imageView = new ImageView(context);
            imageView.SetImageResource(Resource.Drawable.ic_successstatus);
            frameLayout.AddView(imageView,
                new ActionMenuView.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent));

            dialog.Show();

            var tm = Task.Delay(duration).ContinueWith(t => { dialog.Dismiss(); });
            return tm;
        }

        public static Task showSpinner(Context context, string text, Task dismissTask, bool cancelable = false)
        {
            var dialog = new Dialog(context);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
            dialog.SetContentView(Resource.Layout.HUDLayout);
            var frameLayout = dialog.FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);
            var textView = dialog.FindViewById<TextView>(Resource.Id.textViewStatus);
            textView.Text = text;

            dialog.SetCancelable(cancelable);
            
            //frameLayout.SetBackgroundColor(Color.Aqua);

            dialog.Show();

            return dismissTask.ContinueWith(t => { dialog.Dismiss(); });
        }
    }
}