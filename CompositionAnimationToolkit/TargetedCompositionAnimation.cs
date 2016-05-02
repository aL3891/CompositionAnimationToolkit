using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TargetedCompositionAnimation
    {
       
        public TargetedCompositionAnimation(CompositionObject compositionObject, Expression expression, CompositionAnimation animation)
        {
            Target = compositionObject;
            TargetProperty  = ExpressionHelper.ExpressionToPropertyName(expression);
            Animation = animation;
        }

        public TargetedCompositionAnimation()
        {

        }

        public CompositionObject Target { get; set; }
        public string TargetProperty { get; set; }
        public CompositionAnimation Animation { get; set; }
        public CompositionAnimationPropertyCollection Properties { get; set; }

        public virtual TargetedCompositionAnimation Start()
        {
            Target.StartAnimation(TargetProperty, Animation);
            return this;
        }
    }
}
