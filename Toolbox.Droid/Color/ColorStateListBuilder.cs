using System.Collections.Generic;
using System.Linq;
using Android.Content.Res;

namespace Toolbox.Droid.Color
{
    public class ColorStateListBuilder
    {
        private readonly IList<int> Colors = new List<int>();
        private readonly IList<int[]> States = new List<int[]>();


        public ColorStateListBuilder AddColorForState(int state, int color)
        {
            Colors.Add(color);
            States.Add(new[] {state});
            return this;
        }

        public ColorStateList Build()
        {
            return new ColorStateList(States.ToArray(), Colors.ToArray());
        }
    }
}