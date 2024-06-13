using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Icu.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using Xamarin.Droid.UIToolbox.List;

namespace Xamarin.Droid.UIToolbox.Views
{
    public class ActivityDialog : AlertDialog, SmartListAdapter<ActivityDialog.ActivityItem>.DataProvider
    {
        public SmartListAdapter<ActivityItem> Adapter;

        public ActivityDialog(Context context, IList<ActivityItem> items, string title, int nbColumns) : base(context)
        {
            // Prepare grid view
            var gridView = new GridView(context);
            Elements = items;
            Adapter = new SmartListAdapter<ActivityItem>(this);
            Adapter.ItemSelected += OnItemClicked;
            gridView.Adapter = Adapter;
            gridView.SetNumColumns(nbColumns);
            SetView(gridView);
            SetTitle(title);
        }

        public event EventHandler<ActivityItem> ActivitySelected;

        private void OnItemClicked(object sender, ActivityItem e)
        {
            ActivitySelected?.Invoke(this, e);
        }

        public class ActivityItem
        {
            public readonly int IconRes;
            public readonly int Tag;
            public readonly int TitleRes;
            public readonly string Title;

            public ActivityItem(int tag, int tileRes, int iconRes)
            {
                Tag = tag;
                TitleRes = tileRes;
                IconRes = iconRes;
            }
            public ActivityItem(int tag, string title, int iconRes)
            {
                Tag = tag;
                TitleRes = 0;
                Title = title;
                IconRes = iconRes;
            }
        }

        #region SmartListAdapter

        public IList<ActivityItem> Elements { get; set; }

        public void RegisterCell(View cell, ViewHolder vh)
        {
            vh.Views["imageView"] = cell.FindViewById(Resource.Id.imageView);
            vh.Views["textView"] = cell.FindViewById(Resource.Id.textView);
        }

        public void Bind(ViewHolder vh, ActivityItem item)
        {
            ((ImageView) vh.Views["imageView"]).SetImageResource(item.IconRes);

            if (item.TitleRes != 0)
            {
                ((TextView) vh.Views["textView"]).SetText(item.TitleRes);
            }
            else
            {
                ((TextView) vh.Views["textView"]).SetText(item.Title, TextView.BufferType.Normal);
            }
        }

        public int ViewTypeCount => 1;

        public int GetCellResource(int position)
        {
            return Resource.Layout.ActivityItemCell;
        }

        #endregion
    }
}