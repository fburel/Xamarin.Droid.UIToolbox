using System;

namespace Toolbox.Droid.SeekArc
{
    public class SeekArcProgressChangedEventArgs : EventArgs
    {
        public SeekArcProgressChangedEventArgs(SeekArc seekArc, int progress, bool fromUser)
        {
            SeekArc = seekArc;
            Progress = progress;
            FromUser = fromUser;
        }

        public SeekArc SeekArc { get; set; }
        public int Progress { get; set; }
        public bool FromUser { get; set; }
    }
}