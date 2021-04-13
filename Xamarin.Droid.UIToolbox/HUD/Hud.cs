using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace Xamarin.Droid.UIToolbox.HUD
{
    public class Hud
    {
        public static int DurationDefault = 2000;

        public static Task ShowError(Context context, string text, int duration)
        {
            var dialog = new Dialog(context);
            dialog.Window.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
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

        public static Task ShowSuccess(Context context, string text, int duration)
        {
            var dialog = new Dialog(context);
            dialog.Window?.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
            dialog.SetContentView(Resource.Layout.HUDLayout);
            var frameLayout = dialog.FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);
            var textView = dialog.FindViewById<TextView>(Resource.Id.textViewStatus);
            if (textView != null) textView.Text = text;

            var imageView = new ImageView(context);
            imageView.SetImageResource(Resource.Drawable.ic_successstatus);
            frameLayout?.AddView(imageView,
                new ActionMenuView.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent));

            dialog.Show();

            var tm = Task.Delay(duration).ContinueWith(t => { dialog.Dismiss(); });
            return tm;
        }

        public interface IProgress
        {
            float Current { get; }
            event EventHandler<float> Updated;

        }
        public static Task<T> ShowSpinner<T>(Context context, string text, Task<T> dismissTask, bool cancelable = false, IProgress progress = null)
        {
            var dialog = new Dialog(context);
            dialog.Window?.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
            dialog.SetContentView(Resource.Layout.HUDLayout);

            var frameLayout = dialog.FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);


            var textView = dialog.FindViewById<TextView>(Resource.Id.textViewStatus);
            if (textView != null) textView.Text = text;

            dialog.SetCancelable(cancelable);

            //frameLayout.SetBackgroundColor(Color.Aqua);

            dialog.Show();

            var imageView = new ImageView(context);
            imageView.SetImageResource(Resource.Drawable.ic_successstatus);


            var pg = new ProgressBar(context);
            if(progress == null)
             pg.Indeterminate = true;
            else
            {
                pg.Indeterminate = false;
                pg.Progress = (int) (100 * progress.Current);
                pg.Min = 0;
                pg.Max = 100;
                progress.Updated += (sender, f) =>
                {
                    pg.Progress = (int) (100 * f);
                };
            }

            frameLayout?.AddView(pg,
                new ActionMenuView.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent));

            return dismissTask.ContinueWith(t =>
            {
                dialog.Dismiss();
                return t.Result;
            });
        }
    }
}