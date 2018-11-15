using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Java.Lang;

namespace Toolbox.Droid.Fragments
{
    public class BaseFragment : Fragment
    {
        
        protected int MatchParent = ViewGroup.LayoutParams.MatchParent;
        protected int WrapContent = ViewGroup.LayoutParams.WrapContent;
        
        protected ViewGroup.LayoutParams MakeLayoutParameters (int width, int height)
        {
            return new ViewGroup.LayoutParams(width, height);
        }
        
        private MenuButton RightMenuButton { get; set; }
        protected virtual string Title { get; set; }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            if (RightMenuButton != null)
            {
                menu.Clear();
                menu.Add(Menu.None, RightMenuButton.Tag, Menu.None, new String(RightMenuButton.Title))
                    .SetIcon(RightMenuButton.Icon)
                    .SetShowAsAction(ShowAsAction.Always);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.RightMenuButton)
            {
                item.SetEnabled(false);
                OnRightButtonItemClicked();
                Task.Delay(25).ContinueWith(t => Activity.RunOnUiThread(()=>{
                    item.SetEnabled(true);
                }));
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected virtual void OnRightButtonItemClicked()
        {
        }

        protected void SetRightButtonItem(string title, int icon)
        {
            HasOptionsMenu = true;
            RightMenuButton = new MenuButton(Resource.Id.RightMenuButton, icon, title);
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