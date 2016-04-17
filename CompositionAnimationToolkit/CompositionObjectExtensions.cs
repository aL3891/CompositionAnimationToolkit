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
    public static class CompositionObjectExtensions
    {
        public static TargetedCompositionAnimation CreateAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) where T : CompositionObject
        {
            var property = CompositionAnimationExtensions.ExpressionToPropertyName(expression);
            var animation = compositionObject.Compositor.CreateExpressionAnimation();
            var props = animation.ExpressionLambda(animationexpression);

            return new TargetedCompositionAnimation
            {
                Animation = animation,
                Properties = props,
                Target = compositionObject,
                TargetProperty = property
            };
        }

        public static TargetedCompositionAnimation StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) where T : CompositionObject
        {
            var res = compositionObject.CreateAnimation(expression, animationexpression);
            res.Start();
            return res;
        }

        public static TargetedCompositionAnimation CreateAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject
        {
            var res = new TargetedCompositionAnimation
            {
                Animation = animation,
                Target = compositionObject,
                TargetProperty = CompositionAnimationExtensions.ExpressionToPropertyName(expression)
            };

            compositionObject.StartAnimation(res.TargetProperty, animation);
            return res;
        }

        public static TargetedCompositionAnimation StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject
        {
            var res = compositionObject.CreateAnimation(expression, animation);
            res.Start();
            return res;
        }

        public static void StopAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression) where T : CompositionObject
        {
            compositionObject.StopAnimation(CompositionAnimationExtensions.ExpressionToPropertyName(expression));
        }

        public static TypeAnnotatedCompositionObject<R> AsAnnotated<R>(this CompositionObject input, R instance)
        {
            return new TypeAnnotatedCompositionObject<R> { Target = input };
        }

        public static TypeAnnotatedCompositionObject<T> AsAnnotated<T>(this CompositionObject input)
        {
            return new TypeAnnotatedCompositionObject<T> { Target = input };
        }
    }
}
