using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Toolbox.Droid.List
{
    public class SmartExpandableListAdapter<T, S> : BaseExpandableListAdapter
    {
        private readonly DataProvider _provider;

        public SmartExpandableListAdapter(DataProvider provider)
        {
            _provider = provider;
        }


        public override int GroupCount => _provider.SectionCount;

        public override bool HasStableIds => false;


        public override Object GetChild(int groupPosition, int childPosition)
        {
            return groupPosition * 1000 + childPosition;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return groupPosition * 1000 + childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return _provider.RowsInSection(groupPosition);
        }

        public override Object GetGroup(int groupPosition)
        {
            return groupPosition * 1000;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return _provider.IsItemSelectable(groupPosition, childPosition);
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var cell = convertView;

            var viewType = _provider.GetHeaderCellResource;

            if (cell == null || (cell.Tag as ViewHolder)?.ViewType != viewType)
            {
                cell = LayoutInflater.From(parent.Context).Inflate(viewType, parent, false);
                var vh = new ViewHolder(cell, null, viewType, false);
                _provider.RegisterCell(cell, vh, true);
                cell.Tag = vh;
            }

            var item = _provider.ElementForSectionHeader(groupPosition);

            var viewHolde = (ViewHolder) cell.Tag;

            viewHolde.Position = groupPosition;

            _provider.BindHeaderCell(viewHolde, item);

            return cell;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView,
            ViewGroup parent)
        {
            var cell = convertView;

            var viewType = _provider.GetElementCellResource;

            if (cell == null || (cell.Tag as ViewHolder)?.ViewType != viewType)
            {
                cell = LayoutInflater.From(parent.Context).Inflate(viewType, parent, false);
                var vh = new ViewHolder(cell, null, viewType);
                _provider.RegisterCell(cell, vh, false);
                cell.Tag = vh;
            }

            var item = _provider.ElementForIndex(groupPosition, childPosition);

            var viewHolde = (ViewHolder) cell.Tag;

            viewHolde.Position = childPosition;

            _provider.BindElementCell(viewHolde, item);

            return cell;
        }

        public interface DataProvider
        {
            int SectionCount { get; }

            int GetHeaderCellResource { get; }

            int GetElementCellResource { get; }

            int RowsInSection(int sectionID);

            T ElementForSectionHeader(int sectionID);

            S ElementForIndex(int sectionId, int rowID);

            void RegisterCell(View cell, ViewHolder vh, bool isHeaderCell);

            void BindHeaderCell(ViewHolder vh, T item);

            void BindElementCell(ViewHolder vh, S item);
            bool IsItemSelectable(int sectionID, int roxID);
        }
    }
}