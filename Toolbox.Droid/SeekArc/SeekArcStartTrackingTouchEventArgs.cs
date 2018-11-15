using System;

namespace Toolbox.Droid.SeekArc
{
    public class SeekArcTrackingTouchEventArgs : EventArgs
    {
        public SeekArcTrackingTouchEventArgs(SeekArc seekArc)
        {
            SeekArc = seekArc;
        }

        public SeekArc SeekArc { get; }
    }
}