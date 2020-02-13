using System.Collections.Generic;
using System.Linq;
using Android.Content.Res;

namespace Toolbox.Droid.Color
{
    public class ColorStateListBuilder
    {
        private readonly IList<int> _colors = new List<int>();
        private readonly IList<int[]> _states = new List<int[]>();
        
        
        /// <summary>
        ///     Match an Android.Resource.Attribute.State to a Resource.Color
        /// </summary>
        /// <param name="state"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public ColorStateListBuilder AddColorForState(int state, int color)
        {
            _colors.Add(color);
            _states.Add(new[] {state});
            return this;
        }

        public ColorStateList Build()
        {
            return new ColorStateList(_states.ToArray(), _colors.ToArray());
        }
    }
}