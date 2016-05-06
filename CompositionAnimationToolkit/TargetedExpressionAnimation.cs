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

        private Expression expression;

        public TargetedExpressionAnimation(CompositionObject compositionObject, Expression expression, Expression animationexpression)
        {
            Target = compositionObject;
            this.expression = expression;
            this.animationexpression = animationexpression;
        }

        public override TargetedCompositionAnimation Start()
        {
            TargetProperty = ExpressionHelper.ExpressionToPropertyName(expression);
            var animation = Target.Compositor.CreateExpressionAnimation();
            Animation = animation;
            var ce = ExpressionHelper.ExpressionToCompositionExpression(animationexpression);
            Properties = ce.Parameters;
            animation.Expression = ce.Expression;
            ExpressionHelper.ApplyParameters(Animation, ce.Parameters);
            return base.Start();
        }
    }
}
