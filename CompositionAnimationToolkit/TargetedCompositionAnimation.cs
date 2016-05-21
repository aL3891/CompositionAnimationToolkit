using System.Linq.Expressions;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TargetedCompositionAnimation
    {
        public CompositionObject Target { get; set; }
        public string TargetProperty { get; set; }
        public CompositionAnimation Animation { get; set; }
        public CompositionAnimationPropertyCollection Properties { get; set; }

        public TargetedCompositionAnimation(CompositionObject compositionObject, Expression expression, CompositionAnimation animation)
        {
            Target = compositionObject;
            TargetProperty = ExpressionHelper.ExpressionToPropertyName(expression);
            Animation = animation;
        }

        public TargetedCompositionAnimation()
        {

        }

        public virtual void EnsureAnimationCreated()
        {

        }
    }

    public static class TargetedCompositionAnimationExtensions
    {
        public static T Start<T>(this T animation) where T : TargetedCompositionAnimation
        {
            animation.EnsureAnimationCreated();
            animation.Target.StartAnimation(animation.TargetProperty, animation.Animation);
            return animation;
        }
    }
}
