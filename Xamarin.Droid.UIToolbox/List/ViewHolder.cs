using System;
using System.Collections.Generic;
using Android.Views;
using Object = Java.Lang.Object;

namespace Xamarin.Droid.UIToolbox.List
{
    public class ViewHolder : Object
    {
        private readonly CallBack _callback;
        private readonly View _cell;

        public readonly Dictionary<View, ISet<CallBack>> _extras = new Dictionary<View, ISet<CallBack>>();

        public readonly Dictionary<string, View> Views = new Dictionary<string, View>();

        public readonly int ViewType;

        public ViewHolder(View cell, CallBack callBack = null, int viewType = 1, bool registerForClick = true)
        {
            _cell = cell;
            _callback = callBack;
            if (registerForClick) cell.Click += OnClick;
            ViewType = viewType;
        }

        public int Position { get; set; }

        public T getView<T>(string identifier) where T : View
        {
            return Views[identifier] as T;
        }

        public void RegisterOnClickListener(string viewKey, CallBack callback)
        {
            var v = Views[viewKey];
            v.Click += OnClick;
            var extra = _extras.GetValueOrDefault(v, new HashSet<CallBack>());
            extra.Add(callback);
            _extras[v] = extra;
        }

        public void RemoveExtraListeners(string viewKey)
        {
            var v = Views[viewKey];
            _extras[v] = null;
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (((View) sender).Tag?.Equals(this) ?? false)
                _callback?.OnClick(Position, _cell);
            else
                foreach (var callBack in _extras.GetValueOrDefault((View) sender, new HashSet<CallBack>()))
                    callBack.OnClick(Position, (View) sender);
        }

        public interface CallBack
        {
            void OnClick(int position, View sender);
        }

        public class EventCallBack : CallBack
        {
            public EventCallBack(EventHandler<int> eventName)
            {
                Triggered += eventName;
            }

            public void OnClick(int position, View sender)
            {
                OnTriggered(position);
            }

            public event EventHandler<int> Triggered;

            protected virtual void OnTriggered(int e)
            {
                Triggered?.Invoke(this, e);
            }
        }
    }
}