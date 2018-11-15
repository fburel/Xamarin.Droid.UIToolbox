using System;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Toolbox.Droid.List;

namespace Toolbox.Droid.Fragments
{
    internal interface IGroupAdapterBinder
    {
        void OnSetSectionHeaderTitle(View cell, string title);

        View OnCreateHeaderViewCell(Context context);
    }

    internal abstract class Adapter<T> : SmartGroupedListAdapter<T>
    {
        private readonly IGroupAdapterBinder Binder;

        protected Adapter(DataProvider provider, IGroupAdapterBinder binder) : base(provider)
        {
            Binder = binder;
        }

        public override void OnSetSectionHeaderTitle(View cell, string title)
        {
            Binder.OnSetSectionHeaderTitle(cell, title);
        }

        public override View OnCreateHeaderViewCell(Context context)
        {
            return Binder.OnCreateHeaderViewCell(context);
        }
    }

    public abstract class SmartGroupedListFragment<T> : BaseFragment, SmartGroupedListAdapter<T>.DataProvider,
        IGroupAdapterBinder
    {
        private SmartGroupedListAdapter<T> adapter;

        protected ListView ListView { get; private set; }

        protected SwipeRefreshLayout RefreshLayout { get; private set; }

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
            RefreshLayout = new SwipeRefreshLayout(Activity);
            RefreshLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);

            ListView = new ListView(Activity);
            ListView.LayoutParameters =
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            RefreshLayout.AddView(ListView);

            adapter = new SmartGroupedListAdapter<T>(this);

            adapter.ItemSelected += OnItemSelected;

            ListView.Adapter = adapter;

            RefreshLayout.Refresh += OnRefresh;

            return RefreshLayout;
        }

        protected async Task<bool> ForceRefresh()
        {
            var completion = new TaskCompletionSource<bool>();
            RefreshDataSet(completion);
            try
            {
                var unused = await completion.Task;
                RefreshLayout.Refreshing = false;
                adapter.NotifyDataSetChanged();
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


        #region Overrides & Defaults

        protected abstract void OnItemSelected(object sender, T e);

        protected abstract void RefreshDataSet(TaskCompletionSource<bool> completion);

        public abstract int NumberOfSections { get; }

        public abstract int NumberOfElements(int section);

        public abstract T Element(int section, int position);

        public abstract int ItemCell { get; }

        public abstract void RegisterCell(View cell, ViewHolder vh);

        public abstract void Bind(ViewHolder viewHolde, T item);

        public abstract string SectionTitle(int i);

        public virtual void OnSetSectionHeaderTitle(View cell, string title)
        {
            cell.FindViewById<TextView>(Android.Resource.Id.Text1).Text = title;
        }

        public virtual View OnCreateHeaderViewCell(Context context)
        {
            return LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleListItem1, null);
        }

        #endregion
    }
}