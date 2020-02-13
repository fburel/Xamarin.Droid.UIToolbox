using System;
using System.Threading.Tasks;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Java.Lang;
using Exception = Java.Lang.Exception;

namespace Toolbox.Droid.Fragments
{
    public class BaseFragment : Fragment
    {
        public static int MatchParent = ViewGroup.LayoutParams.MatchParent;
        protected int WrapContent = ViewGroup.LayoutParams.WrapContent;

        private MenuButton RightMenuButton { get; set; }
        protected virtual string Title { get; set; }

        protected ViewGroup.LayoutParams MakeLayoutParameters(int width, int height)
        {
            return new ViewGroup.LayoutParams(width, height);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            menu.Clear();
            
            if (RightMenuButton == null) return;
            
            menu.Add((int) Menu.None, RightMenuButton.Tag, (int) Menu.None, (ICharSequence) new Java.Lang.String(RightMenuButton.Title))
                .SetIcon(RightMenuButton.Icon)
                .SetShowAsAction(ShowAsAction.Always);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.RightMenuButton)
            {
                item.SetEnabled(false);
                OnRightButtonItemClicked();
                Task.Delay(25).ContinueWith(t => Activity.RunOnUiThread(() => { item.SetEnabled(true); }));
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected virtual void OnRightButtonItemClicked()
        {
        }

        protected void RemoveRightButtonItem()
        {
            RightMenuButton = null;
            try
            {
                Activity.InvalidateOptionsMenu();
            }
            catch (Exception)
            {
            }
        }
        
        protected void SetRightButtonItem(string title, int icon)
        {
            HasOptionsMenu = true;
            RightMenuButton = new MenuButton(Resource.Id.RightMenuButton, icon, title);
            try
            {
                Activity.InvalidateOptionsMenu();
            }
            catch (Exception)
            {
            }
        }


        public override void OnStart()
        {
            base.OnStart();
            if (Title != null) Activity.Title = Title;
        }

        public void Back()
        {
            Activity.OnBackPressed();
        }

        protected new string GetString(int resId)
        {
            return Activity.GetString(resId);
        }

        protected Android.Graphics.Color GetColor(int colorRes)
        {
            return new Android.Graphics.Color(ContextCompat.GetColor(Activity, colorRes));
//            return Activity.Resources.GetColor(colorRes);
        }


        private class MenuButton
        {
            public readonly int Icon;
            public readonly int Tag;
            public readonly string Title;

            public MenuButton(int tag, int icon, string title)
            {
                Tag = tag;
                Icon = icon;
                Title = title;
            }
        }
    }
}