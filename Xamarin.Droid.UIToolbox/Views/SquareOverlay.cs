using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace Xamarin.Droid.UIToolbox.Views
{
    public class SquareOverlay : View
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            var p = new Paint();
            p.Color = global::Android.Graphics.Color.AliceBlue;
            canvas.DrawText("Preview", canvas.Width / 2, canvas.Height / 2, p);
        }

        #region constructor

        protected SquareOverlay(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public SquareOverlay(Context context) : base(context)
        {
        }

        public SquareOverlay(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public SquareOverlay(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs,
            defStyleAttr)
        {
        }

        public SquareOverlay(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context,
            attrs, defStyleAttr, defStyleRes)
        {
        }

        #endregion
    }
}