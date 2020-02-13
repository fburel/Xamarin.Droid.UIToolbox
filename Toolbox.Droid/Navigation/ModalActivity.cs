using System;
using System.Collections.Generic;
using Android.App;
using Java.Lang;
using Toolbox.Droid.Fragments;

namespace Toolbox.Droid.Navigation
{
    public abstract class ModalActivity : PushPopActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            ModalActivityManager.RegisterCurrent(this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            ModalActivityManager.ClearReference(this);
        }

        protected override void OnDestroy()
        {
            ModalActivityManager.ClearReference(this);
            base.OnDestroy();
        }

        protected override void onActivityBack()
        {
            Dismiss();
        }

        protected void Dismiss()
        {
            Finish();
            OverridePendingTransition(Resource.Animation.StayPut, Resource.Animation.ModalExit);
        }

        public static class ModalActivityManager
        {
            private static readonly IList<ModalActivity> Activities = new List<ModalActivity>();

            public static void RegisterCurrent(ModalActivity activity)
            {
                ClearReference(activity);
                Activities.Add(activity);
            }

            public static void ClearReference(ModalActivity modalActivity)
            {
                Activities.Remove(modalActivity);
            }

            public static ModalActivity GetCurrentModalActivity()
            {
                try
                {
                    return Activities[Activities.Count - 1];
                }
                catch
                {
                    return null;
                }
            }

            public static void Dismiss()
            {
                GetCurrentModalActivity()?.Dismiss();
            }
        }
    }

    [Activity]
    public class AutoModalActivity : ModalActivity
    {
        public static readonly string ModalFragmentTypeKey = "ModalFragmentTypeKey";
        public static readonly string ExtrasKey = "ExtrasKey";

        protected override Type GetRootFragmentClass =>
            Type.GetType(Intent?.Extras?.GetString(ModalFragmentTypeKey) ?? throw new ClassCastException());

        protected override BaseFragment Reset()
        {
            var f = base.Reset();
            f.Arguments = Intent?.Extras?.GetBundle(ExtrasKey);
            return f;
        }
    }
}