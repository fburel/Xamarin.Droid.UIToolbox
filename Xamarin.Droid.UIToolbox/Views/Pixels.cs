using Android.Content.Res;
using Android.Util;

namespace Xamarin.Droid.UIToolbox.Views
{
    public static class Pixels
    {
        public static int FromDP(int dp, Resources resources)
        {
            return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, resources.DisplayMetrics);
        }
    }
}