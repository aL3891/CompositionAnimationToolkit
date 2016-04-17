using CompositionAnimationToolkit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TypeAnnotatedCompositionObject<T>
    {
        public CompositionObject Target { get; set; }
        
        public TargetedCompositionAnimation CreateAnimation<R>(Expression<Func<T, R>> expression, CompositionAnimation animation)
        {
            return new TargetedCompositionAnimation
            {
                Animation = animation,
                Target = Target,
                TargetProperty = CompositionAnimationExtensions.ExpressionToPropertyName(expression)
            };
        }

        public TargetedCompositionAnimation StartAnimation<R>(Expression<Func<T, R>> expression, CompositionAnimation animation)
        {
            var res = CreateAnimation(expression, animation);
            res.Start();
            return res;
        }

        public TargetedCompositionAnimation CreateAnimation<R>(Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression)
        {
            var property = CompositionAnimationExtensions.ExpressionToPropertyName(expression);

            var animation = Target.Compositor.CreateExpressionAnimation();
            var props = animation.ExpressionLambda(animationexpression);

            Target.StartAnimation(property, animation);

            return new TargetedCompositionAnimation
            {
                Animation = animation,
                Properties = props,
                Target = Target,
                TargetProperty = property
            };
        }

        public TargetedCompositionAnimation StartAnimation<R>(Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression)
        {
            var res = CreateAnimation(expression, animationexpression);
            res.Start();
            return res;
        }

        public void StopAnimation<T, R>(Expression<Func<T, R>> expression) where T : CompositionObject
        {
            Target.StopAnimation(CompositionAnimationExtensions.ExpressionToPropertyName(expression));
        }
    }
}
