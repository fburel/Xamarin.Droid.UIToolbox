using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Xamarin.Droid.UIToolbox.List;

namespace Xamarin.Droid.UIToolbox.Fragments
{
    public abstract class SmartListFragment<T> : BaseFragment, List.SmartListAdapter<T>.DataProvider
    {
        private List.SmartListAdapter<T> _adapter;
        protected ListView ListView { get; private set; }
        protected SwipeRefreshLayout RefreshLayout { get; private set; }

        protected View EmptyView { get; set; }


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

            RefreshLayout.Refresh += OnRefresh;

            return RefreshLayout;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            try
            {
                _adapter = new List.SmartListAdapter<T>(this);
                
                _adapter.ItemSelected += OnItemSelected;
                
                ListView.Adapter = _adapter;
            }
            catch (Exception e)
            {
                
            }
           
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
        
        #endregion

        #region PullToRefresh

        protected virtual bool RefreshEnabled => false;

        /// <summary>
        /// Called by the pull to refresh trigger. This method gives you some times to perform an async task.
        /// Once done, implementation should call the `completion.SetResult(true);`method
        /// </summary>
        /// <param name="completion">The completion task source</param>
        protected virtual void RefreshDataSet(TaskCompletionSource<bool> completion)
        {
            completion.TrySetResult(true);
        }

        #endregion
    }
}