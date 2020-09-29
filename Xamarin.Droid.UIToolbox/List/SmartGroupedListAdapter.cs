using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Xamarin.Droid.UIToolbox.List
{
    public class SmartGroupedListAdapter<T> : BaseSmartAdapter<T>
    {
        private readonly DataProvider Provider;

        private readonly IList<int> SectionHeadersPositions = new List<int>();

        public SmartGroupedListAdapter(DataProvider provider)
        {
            Provider = provider;
        }

        #region Helper

        private int FindSectionIdx(int position)
        {
            for (var i = SectionHeadersPositions.Count; i > 0; i--)
            {
                var value = SectionHeadersPositions[i - 1];

                if (position >= value) return i - 1;
            }

            return -1;
        }

        #endregion

        public interface DataProvider
        {
            int NumberOfSections { get; }

            int ItemCell { get; }

            int NumberOfElements(int section);

            T Element(int section, int position);

            void RegisterCell(View cell, ViewHolder vh);

            void Bind(ViewHolder viewHolde, T item);

            string SectionTitle(int i);
        }

        #region Adapter

        public override bool IsEnabled(int position)
        {
            return !SectionHeadersPositions.Contains(position);
        }

        public override int ViewTypeCount => 2;

        public override int Count
        {
            get
            {
                SectionHeadersPositions.Clear();
                var count = 0;
                for (var i = 0; i < Provider.NumberOfSections; i++)
                {
                    SectionHeadersPositions.Add(count);
                    count++;
                    count += Provider.NumberOfElements(i);
                }

                return count;
            }
        }

        public override T this[int position]
        {
            get
            {
                var section = FindSectionIdx(position);
                var sectionHeaderPosition = SectionHeadersPositions[section];
                return sectionHeaderPosition == position
                    ? default
                    : Provider.Element(section, position - sectionHeaderPosition - 1);
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int GetItemViewType(int position)
        {
            return SectionHeadersPositions.Contains(position) ? 0 : 1;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var cell = convertView;

            if (SectionHeadersPositions.Contains(position))
            {
                var title = Provider.SectionTitle(SectionHeadersPositions.IndexOf(position));

                if (cell == null) cell = OnCreateHeaderViewCell(parent.Context);

                OnSetSectionHeaderTitle(cell, title);

                return cell;
            }

            if (cell == null)
            {
                cell = LayoutInflater.From(parent.Context).Inflate(Provider.ItemCell, null);


                var vh = new ViewHolder(cell, this);
                Provider.RegisterCell(cell, vh);
                cell.Tag = vh;
            }

            var item = this[position];

            ((ViewHolder) cell.Tag).Position = position;

            Provider.Bind((ViewHolder) cell.Tag, item);

            return cell;
        }

        #endregion

        #region overrides & default behavior

        public virtual View OnCreateHeaderViewCell(Context context)
        {
            var view = LayoutInflater.From(context).Inflate(global::Android.Resource.Layout.SimpleListItem1, null);
            return view;
        }

        public virtual void OnSetSectionHeaderTitle(View cell, string title)
        {
            cell.FindViewById<TextView>(global::Android.Resource.Id.Text1).Text = title;
        }

        #endregion
    }
}