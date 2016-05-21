using System.Linq.Expressions;
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

        public override void EnsureAnimationCreated()
        {
            if (Animation == null)
            {
                TargetProperty = ExpressionHelper.ExpressionToPropertyName(expression);
                var animation = Target.Compositor.CreateExpressionAnimation();

                var ce = ExpressionHelper.ExpressionToCompositionExpression(animationexpression);
                Properties = ce.Parameters;
                animation.Expression = ce.Expression;
                ExpressionHelper.ApplyParameters(Animation, ce.Parameters);

                Animation = animation;
            }
        }

    }
}
