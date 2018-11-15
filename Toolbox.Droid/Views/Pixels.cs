using Android.Content.Res;
using Android.Util;

namespace Toolbox.Droid.Views
{
    public static class Pixels
    {
        public static int FromDP(int dp, Resources resources)
        {
            return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, resources.DisplayMetrics);
        }
    }
}