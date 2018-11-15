using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Toolbox.Droid.ViewPager
{
    public interface IDottedViewPagerDataSource<T>
    {
        IList<T> Items { get; }
        View ViewForItem(ViewGroup parent, T item);
    }

    public class DottedViewPager<T> : RelativeLayout
    {
        private readonly DottedViewPagerAdapter<T> _adapter = new DottedViewPagerAdapter<T>();

        private Android.Support.V4.View.ViewPager _pager;
        public EventHandler<T> PageChanged;

        public IDottedViewPagerDataSource<T> Datasource { get; set; }

        public int CurrentItem
        {
            get => _pager.CurrentItem;
            set => _pager.CurrentItem = value;
        }

        private void init()
        {
            // Create the pager
            _pager = new Android.Support.V4.View.ViewPager(Context);
            var @params = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            _pager.Adapter = _adapter;
            _adapter.ViewForItem = ViewForItem;
            AddView(_pager, @params);

//            var tab = new PagerTabStrip(Context);
//            tab.DrawFullUnderline = true;
//            tab.SetGravity((int) GravityFlags.Bottom);
//            tab.Background = ContextCompat.GetDrawable(Context, Resource.Drawable.tab_selector);
//            _pager.AddView(tab, new Gallery.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent));

//            // Create the tab dots
//            _tabLayout = LayoutInflater.From(Context).Inflate(Resource.Layout.tab, null) as TabLayout;
//            @params = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
//            @params.AddRule(LayoutRules.AlignParentBottom, (int) LayoutRules.True);
//            @params.AddRule(LayoutRules.CenterHorizontal, (int) LayoutRules.True);

//            AddView(_tabLayout, @params);
//            
//            _tabLayout.SetupWithViewPager(_pager);

            _pager.PageSelected += OnPageChanged;
        }

        private void OnPageChanged(object sender, Android.Support.V4.View.ViewPager.PageSelectedEventArgs e)
        {
            PageChanged?.Invoke(this, _adapter.Items[e.Position]);
        }

        private View ViewForItem(ViewGroup parent, T item)
        {
            if (Datasource == null)
                throw new InvalidDataSourceException();
            return Datasource.ViewForItem(parent, item);
        }

        public void ReloadData()
        {
            if (Datasource == null)
                throw new InvalidDataSourceException();

            _adapter.Items.Clear();

            foreach (var text in Datasource.Items) _adapter.Items.Add(text);

            _pager.Adapter = _adapter;

//            _tabLayout.SetupWithViewPager(_pager);
        }

        private class DottedViewPagerAdapter<T2> : LayoutViewPagerAdapter<T2>
        {
            public delegate View ViewBuilder(ViewGroup parent, T2 item);

            public ViewBuilder ViewForItem { get; set; }

            protected override View OnCreateLayout(ViewGroup parent, T2 item)
            {
                return ViewForItem(parent, item);
            }
        }

        public class InvalidDataSourceException : Exception
        {
        }

        #region inherited constructors

        protected DottedViewPager(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            init();
        }

        public DottedViewPager(Context context) : base(context)
        {
            init();
        }

        public DottedViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public DottedViewPager(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs,
            defStyleAttr)
        {
            init();
        }

        public DottedViewPager(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context,
            attrs, defStyleAttr, defStyleRes)
        {
            init();
        }

        #endregion
    }
}