﻿using System;
using Android.Views;
using Android.Widget;

namespace Xamarin.Droid.UIToolbox.List
{
    public abstract class BaseSmartAdapter<T> : BaseAdapter<T>, ViewHolder.CallBack
    {
        public void OnClick(int position, View sender)
        {
            ItemSelected?.Invoke(this, this[position]);
        }

        public event EventHandler<T> ItemSelected;
    }
}