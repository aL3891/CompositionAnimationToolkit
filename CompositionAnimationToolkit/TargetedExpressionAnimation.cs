using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TargetedExpressionAnimation : TargetedCompositionAnimation
    {
        private Expression animationexpression;
        private CompositionObject compositionObject;
        private Expression expression;

        public TargetedExpressionAnimation(CompositionObject compositionObject, Expression expression, Expression animationexpression)
        {
            this.compositionObject = compositionObject;
            this.expression = expression;
            this.animationexpression = animationexpression;
        }
    }
}
