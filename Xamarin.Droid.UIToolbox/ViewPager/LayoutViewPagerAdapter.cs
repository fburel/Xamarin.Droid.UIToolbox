using System.Collections.Generic;
using Android.Views;
using AndroidX.ViewPager.Widget;
using Java.Lang;

namespace Xamarin.Droid.UIToolbox.ViewPager
{
    public abstract class LayoutViewPagerAdapter<T> : PagerAdapter
    {
        public readonly IList<T> Items = new List<T>();

        #region Abstracts

        protected abstract View OnCreateLayout(ViewGroup parent, T item);

        #endregion

        #region Pager

        public override int Count => Items.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup collection, int position)
        {
            var layout = OnCreateLayout(collection, Items[position]);
            collection.AddView(layout);
            return layout;
        }


        public override void DestroyItem(ViewGroup collection, int position, Java.Lang.Object view)
        {
            collection.RemoveView(view as View);
        }

        #endregion
    }
}