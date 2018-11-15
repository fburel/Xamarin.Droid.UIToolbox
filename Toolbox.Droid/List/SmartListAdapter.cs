using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Android.Views;
using Java.Util;

namespace Toolbox.Droid.List
{
    public class SmartListAdapter<T> : BaseSmartAdapter<T>
    {
        private readonly DataProvider _provider;
        private IList<int> _viewTypes;
        
        private IList<T> _data;

        public SmartListAdapter(DataProvider provider)
        {
            _provider = provider;
            _data = provider.Elements;
            if (_data.GetType() == typeof(ObservableCollection<T>))
            {
                ((ObservableCollection<T>) _data).CollectionChanged += OnCollectionChanged;
            }
            _viewTypes = _provider.Elements
                .Select(x => _provider.GetCellResource(_provider.Elements.IndexOf(x))).Distinct().ToList();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _viewTypes = _data
                .Select(x => _provider.GetCellResource(_provider.Elements.IndexOf(x))).Distinct().ToList();
            base.NotifyDataSetChanged();
        }

        public override void NotifyDataSetChanged()
        {
            _data = _provider.Elements;
            _viewTypes = _provider.Elements
                .Select(x => _provider.GetCellResource(_provider.Elements.IndexOf(x))).Distinct().ToList();
            base.NotifyDataSetChanged();
        }

        public interface DataProvider
        {
            /// <summary>
            ///     Should return an IList of the model object to be displayed
            /// </summary>
            IList<T> Elements { get; }

            /// <summary>
            ///     Use this methode to store all the view of the cell in the viewHolder.
            ///     Use vh.ViewType to determine what kind of cell is passed.
            /// </summary>
            void RegisterCell(View cell, ViewHolder vh);

            /// <summary>
            ///     Use this method to fill the viewHolder's registered view with the data from the model object.
            ///     Use vh.ViewType to determine what kind of cell is attached to the viewHolder.
            /// </summary>
            void Bind(ViewHolder vh, T item);

            int GetCellResource(int position);
        }

        #region BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override T this[int position] => _data[position];

        public override int Count => _data?.Count ?? 0;

        public override int ViewTypeCount => (_viewTypes.Count < 1) ? 1 : _viewTypes.Count;

        public override int GetItemViewType(int position)
        {
            return _viewTypes.IndexOf(_provider.GetCellResource(position));
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var cell = convertView;
            
            var viewType = _provider.GetCellResource(position);
           
            if (cell == null || (cell.Tag as ViewHolder)?.ViewType != viewType)
            {
                cell = LayoutInflater.From(parent.Context).Inflate(viewType, null);
                var vh = new ViewHolder(cell, this, viewType);
                _provider.RegisterCell(cell, vh);
                cell.Tag = vh;
            }

            var item = this[position];
            var viewHolde = (ViewHolder) cell.Tag;
            viewHolde.Position = position;

            _provider.Bind(viewHolde, item);

            return cell;
        }

        #endregion
    }
}