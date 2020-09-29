using System;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;

namespace Xamarin.Droid.UIToolbox.Views
{
    public class ImageSelectorSwitch
    {
        private readonly Dictionary<ImageView, ImageRes> _imageViews = new Dictionary<ImageView, ImageRes>();

        private ImageView _selected;

        public int SelectedIndex
        {
            get => _imageViews[_selected].Index;
            set => OnImageClicked(_imageViews.Keys.First(x => _imageViews[x].Index == value), null);
        }

        public event EventHandler<int> SelectedViewChanged;

        public int AddChoice(ImageView view, int selectedImageResource, int unselectedImageResource)
        {
            view.Click += OnImageClicked;

            _imageViews[view] = new ImageRes
            {
                Index = _imageViews.Count,
                Selected = selectedImageResource,
                Unselected = unselectedImageResource
            };

            return _imageViews.Count;
        }

        private void OnImageClicked(object sender, EventArgs e)
        {
            if (sender == _selected) return;
            _selected?.SetImageResource(_imageViews[_selected].Unselected);
            _selected = sender as ImageView;
            _selected?.SetImageResource(_imageViews[_selected].Selected);
            SelectedViewChanged?.Invoke(this, _imageViews[_selected].Index);
        }

        public struct ImageRes
        {
            public int Index { get; set; }
            public int Selected { get; set; }
            public int Unselected { get; set; }
        }
    }
}