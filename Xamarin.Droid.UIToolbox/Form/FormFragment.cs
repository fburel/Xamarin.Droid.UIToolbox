using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Droid.UIToolbox.Fragments;

namespace Xamarin.Droid.UIToolbox.Form
{
    public abstract class FormFragment : BaseFragment
    {
        private const int DefaultRowHeight = 80;
        private const int DefaultSeparatorGap = 4;
        private const int DefaultSectionHeaderHeight = 44;

        private readonly Dictionary<View, Cell> _cellMap = new Dictionary<View, Cell>();
        private LinearLayout _contentLayout;

        private bool _editingEnabled = true;

        private Cell _focusedCell;
        private ScrollView _scrollview;

        public bool EditingEnabled
        {
            get => _editingEnabled;
            protected set
            {
                _editingEnabled = value;
                EditableStatusChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        ///     Return the size of the section header
        /// </summary>
        protected virtual int SectionHeaderHeight { get; } = DefaultSectionHeaderHeight;

        /// <summary>
        ///     Returns the Gap between to cell
        /// </summary>
        protected virtual int CellSeparatorGap { get; } = DefaultSeparatorGap;

        public event EventHandler<bool> EditableStatusChanged;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _scrollview = new ScrollView(Activity);
            _scrollview.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);

            _contentLayout = new LinearLayout(Activity) {Orientation = Orientation.Vertical};
            _contentLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            _scrollview.AddView(_contentLayout);

            ConfigureForm();

            return _scrollview;
        }

        protected virtual ViewGroup AddRow(Cell cell)
        {
            var view = cell.GetView(Activity);
            _cellMap[view] = cell;
            view.Click += OnCellClicked;

            // Add a small padding
            var l = new LinearLayout(Activity) {Orientation = Orientation.Vertical};
            l.AddView(view,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ToPixel(GetRowHeight(cell.Tag))));
            l.AddView(new View(Activity),
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ToPixel(CellSeparatorGap)));
            _contentLayout.AddView(l);

            return l;
        }

        protected void InsertFooter(int textRes)
        {
            InsertFooter(Activity.GetString(textRes));
        }

        protected void InsertFooter(string text)
        {
            var view = OnCreateFooter(Activity, text);
            _contentLayout.AddView(view);
        }

        protected void InsertHeader(int textRes)
        {
            InsertHeader(Activity.GetString(textRes));
        }

        protected void InsertHeader(string text)
        {
            var view = OnCreateHeader(Activity, text);
            var @params = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ToPixel(SectionHeaderHeight));
            _contentLayout.AddView(view, @params);
        }

        protected void InsertCustomView(View view, int height)
        {
            var @params = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ToPixel(height));
            _contentLayout.AddView(view, @params);
        }

        protected void InsertCustomView(View view)
        {
            _contentLayout.AddView(view);
        }

        protected virtual void Reload()
        {
            _scrollview.RemoveView(_contentLayout);
            _contentLayout = null;
            _contentLayout = new LinearLayout(Activity) {Orientation = Orientation.Vertical};
            _contentLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);
            _scrollview.AddView(_contentLayout);

            ConfigureForm();
        }

        protected void SetCellVisibile(int cellTag, bool b)
        {
            var view = _cellMap.Where(kvp => kvp.Value.Tag == cellTag).Select(x => x.Key).First();
            if (view != null)
                view.Visibility = b ? ViewStates.Visible : ViewStates.Gone;
        }

        private void OnCellClicked(object sender, EventArgs e)
        {
            _focusedCell = _cellMap[sender as View];

            foreach (var cell in _cellMap.Values.ToList())
                if (cell != _focusedCell)
                    cell.OnLoosingFocus(Activity);

            _focusedCell.OnGainingFocus(Activity);
        }

        /// <summary>
        ///     Create a default footer view
        /// </summary>
        /// <param name="context"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual View OnCreateFooter(Context context, string text)
        {
            var tv = new TextView(context);
            tv.Text = text;
            tv.TextSize = 16;
            tv.TextAlignment = TextAlignment.TextStart;

            var view = new LinearLayout(context) {Orientation = Orientation.Vertical};
            var center =
                new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            center.SetMargins(ToPixel(8), ToPixel(8), ToPixel(8), ToPixel(22));
            view.AddView(tv, center);
            return view;
        }

        /// <summary>
        ///     Create a default header view
        /// </summary>
        /// <param name="context"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual View OnCreateHeader(Context context, string text)
        {
            var view = LayoutInflater.From(context).Inflate(global::Android.Resource.Layout.SimpleListItem1, null);
            var tv = view.FindViewById<TextView>(global::Android.Resource.Id.Text1);
            tv.Text = text;
            tv.SetAllCaps(true);
            tv.SetTextColor(global::Android.Graphics.Color.LightGray);
            return view;
        }

        /// <summary>
        ///     Returns the default height of the given row
        /// </summary>
        /// <param name="cellTag"></param>
        /// <returns></returns>
        protected virtual int GetRowHeight(int cellTag)
        {
            return DefaultRowHeight;
        }

        /// <summary>
        ///     Override this method to configure the form (i.e: add the row)
        /// </summary>
        protected abstract void ConfigureForm();

        /// <summary>
        ///     This method is triggered when the value of a cell has been changed
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="newValue"></param>
        public abstract void OnCellDataChanged(Cell cell, object newValue);

        #region Tools

        protected int ToPixel(int dp)
        {
            return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Activity.Resources.DisplayMetrics);
        }

        #endregion
    }
}