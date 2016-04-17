using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TargetedCompositionAnimation
    {
        public CompositionObject Target { get; set; }
        public string TargetProperty { get; set; }
        public CompositionAnimation Animation { get; set; }
        public CompositionAnimationPropertyCollection Properties { get; set; }

        public void Start()
        {
            Target.StartAnimation(TargetProperty, Animation);
        }
    }
}
