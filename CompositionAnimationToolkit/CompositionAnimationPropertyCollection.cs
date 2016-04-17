using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class CompositionAnimationPropertyCollection : Dictionary<string, object>
    {

        public CompositionObject Get(string property)
        {
            return  (CompositionObject)this[property] ;
        }

        public TypeAnnotatedCompositionObject<R> Get<R>(string property)
        {
            return new TypeAnnotatedCompositionObject<R> { Target = (CompositionObject)this[property] };
        }

        public TypeAnnotatedCompositionObject<R> Get<R>(Expression<Func<R>> expression)
        {
            return new TypeAnnotatedCompositionObject<R> { Target = (CompositionObject)this[CompositionAnimationExtensions.ExpressionToPropertyName(expression)] };
        }

        public R Get<T, R>(T target, Expression<Func<T, R>> expression)
        {
            return (R)this[CompositionAnimationExtensions.ExpressionToPropertyName(expression)];
        }
    }

    public class TypeAnnotatedCompositionObject<T>
    {
        public CompositionObject Target { get; set; }

        public void StartAnimation<R>(Expression<Func<T, R>> expression, CompositionAnimation animation)
        {
            Target.StartAnimation(CompositionAnimationExtensions.ExpressionToPropertyName(expression), animation);
        }

        public TargetedCompositionAnimation StartAnimation<R>(Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression)
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

        public void StopAnimation<T, R>(Expression<Func<T, R>> expression) where T : CompositionObject
        {
            Target.StopAnimation(CompositionAnimationExtensions.ExpressionToPropertyName(expression));
        }
    }
}
