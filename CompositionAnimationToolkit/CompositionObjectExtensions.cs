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
        public static TargetedCompositionAnimation StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) where T : CompositionObject
        {
            var property = CompositionAnimationExtensions.ExpressionToPropertyName(expression);
            var animation = compositionObject.Compositor.CreateExpressionAnimation();
            var props = animation.ExpressionLambda(animationexpression);

            compositionObject.StartAnimation(property, animation);

            return new TargetedCompositionAnimation
            {
                Animation = animation,
                Properties = props,
                Target = compositionObject,
                TargetProperty = property
            };
        }


        public static void StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject
        {

            compositionObject.StartAnimation(CompositionAnimationExtensions.ExpressionToPropertyName(expression), animation);
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
