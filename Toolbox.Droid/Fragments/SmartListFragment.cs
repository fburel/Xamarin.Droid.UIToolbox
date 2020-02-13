using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Toolbox.Droid.List;

namespace Toolbox.Droid.Fragments
{
    public abstract class SmartListFragment<T> : BaseFragment, SmartListAdapter<T>.DataProvider
    {
        private SmartListAdapter<T> _adapter;
        protected ListView ListView { get; private set; }
        protected SwipeRefreshLayout RefreshLayout { get; private set; }

        protected View EmptyView { get; set; }

        protected bool RefreshEnabled
        {
            get => RefreshLayout?.Enabled ?? true;
            set
            {
                if (RefreshLayout != null) RefreshLayout.Enabled = value;
            }
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RefreshLayout = new SwipeRefreshLayout(Activity)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent)
            };

            ListView = new ListView(Activity)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent)
            };

            RefreshLayout.AddView(ListView);

            _adapter = new SmartListAdapter<T>(this);

            _adapter.ItemSelected += OnItemSelected;

            ListView.Adapter = _adapter;

            RefreshLayout.Refresh += OnRefresh;

            return RefreshLayout;
        }

        protected void ReloadData()
        {
            Activity.RunOnUiThread(_adapter.NotifyDataSetChanged);
        }

        protected virtual async Task<bool> ForceRefresh()
        {
            var completion = new TaskCompletionSource<bool>();
            RefreshDataSet(completion);
            try
            {
                var unused = await completion.Task;
                RefreshLayout.Refreshing = false;
                _adapter.NotifyDataSetChanged();
                return true;
            }
            catch
            {
                RefreshLayout.Refreshing = false;
                return false;
            }
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            await ForceRefresh();
        }

        #region abstract members

        public abstract IList<T> Elements { get; }


        public abstract int GetCellResource(int position);

        public abstract void RegisterCell(View cell, ViewHolder vh);

        public abstract void Bind(ViewHolder viewHolde, T item);

        protected abstract void OnItemSelected(object sender, T e);

        protected abstract void RefreshDataSet(TaskCompletionSource<bool> completion);

        #endregion
    }
}