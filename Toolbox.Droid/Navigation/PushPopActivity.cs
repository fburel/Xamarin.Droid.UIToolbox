using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Toolbox.Droid.Fragments;

namespace Toolbox.Droid.Navigation
{
    public abstract class PushPopActivity : DrawerActivity
    {
        private readonly IList<BaseFragment> _fragments = new List<BaseFragment>();

        protected override View OnCreateMainView(Bundle savedInstanceState)
        {
            var frameLayout = new FrameLayout(this);
            frameLayout.Id = Resource.Id.DrawerActivityFragmentLayout;
            frameLayout.Tag = "fragmentPlaceholder";

            return frameLayout;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = ScreenOrientation.Portrait;

            // Load 1st fragment if needed
            if (_fragments.Count == 0)
                Reset();
        }

        #region init

        protected virtual BaseFragment Reset()
        {
            _fragments.Clear();

            BaseFragment f = null;

            try
            {
                f = Activator.CreateInstance(GetRootFragmentClass) as BaseFragment;
            }
            catch
            {
                f = new BaseFragment();
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.DrawerActivityFragmentLayout, f)
                .Commit();
            _fragments.Add(f);

            return f;
        }

        protected abstract Type GetRootFragmentClass { get; }

        #endregion

        #region Push/pop Navigation

        public void Push<T>(Bundle extra = null) where T : BaseFragment, new()
        {
            var f = new T();
            Push(f, extra);
        }

        public void Push(BaseFragment f, Bundle extra = null)
        {
            f.Arguments = extra;
            
            SupportFragmentManager.BeginTransaction()
                .SetCustomAnimations(Resource.Animation.PushEntry, Resource.Animation.PushExit)
                .Replace(Resource.Id.DrawerActivityFragmentLayout, f)
                .Commit();
            _fragments.Add(f);
        }

        public void Pop()
        {
            _fragments.RemoveAt(_fragments.Count - 1);
            var previous = _fragments[_fragments.Count - 1];
            SupportFragmentManager.BeginTransaction()
                .SetCustomAnimations(Resource.Animation.PopEntry, Resource.Animation.PopExit)
                .Replace(Resource.Id.DrawerActivityFragmentLayout, previous)
                .Commit();
        }

        public override void OnBackPressed()
        {
            if (_fragments.Count > 1)
                Pop();
            else
                onActivityBack();
        }

        protected virtual void onActivityBack()
        {
            base.OnBackPressed();
        }

        #endregion

        #region Modal navigation

        public void PresentModally(Type baseFragment)
        {
            presentModally(baseFragment, new Bundle());
        }

        public void presentModally(Type baseFragment, Bundle extras)
        {
            var intent = new Intent(this, typeof(AutoModalActivity));
            intent.PutExtra(AutoModalActivity.ModalFragmentTypeKey, baseFragment.AssemblyQualifiedName);
            intent.PutExtra(AutoModalActivity.ExtrasKey, extras);
            StartActivity(intent);
            OverridePendingTransition(Resource.Animation.ModalEntry, Resource.Animation.StayPut);
        }

        public void PresentModally<T>() where T : ModalActivity
        {
            var intent = new Intent(this, typeof(T));
            StartActivity(intent);
            OverridePendingTransition(Resource.Animation.ModalEntry, Resource.Animation.StayPut);
        }

        public void DismissModalActivity()
        {
            ModalActivity.ModalActivityManager.Dismiss();
        }

        #endregion
    }
}