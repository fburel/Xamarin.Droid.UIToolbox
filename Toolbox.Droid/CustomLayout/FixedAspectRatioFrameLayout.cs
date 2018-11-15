using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Security;

namespace Toolbox.Droid.CustomLayout
{
    /// <summary>
    /// This class provides a layout that will have a aspect ratio = 1:1
    /// Use this class to encapsulate your view and store this layout in a parent (LinearLayout, relativeLayout..)
    /// Thanks to @tail : https://stackoverflow.com/questions/7058507/fixed-aspect-ratio-view
    /// </summary>
    public class FixedAspectRatioFrameLayout : FrameLayout
    {
        
        #region ctor
        
        protected FixedAspectRatioFrameLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            
        }

        public FixedAspectRatioFrameLayout(Context context) : base(context)
        {
        }

        public FixedAspectRatioFrameLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public FixedAspectRatioFrameLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public FixedAspectRatioFrameLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }
        
        #endregion
        

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var minValue = Math.Min(MeasureSpec.GetSize(heightMeasureSpec), MeasureSpec.GetSize(widthMeasureSpec));

            base.OnMeasure(
                MeasureSpec.MakeMeasureSpec(minValue, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(minValue, MeasureSpecMode.Exactly)
            );
        }
    }
    
}