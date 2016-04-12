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
        public static void StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject
        {
            var property = ((expression as LambdaExpression)?.Body as MemberExpression)?.Member.Name;
            if (property == null)
                throw new ArgumentException();

            compositionObject.StartAnimation(property, animation);
        }

        public static void StopAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression) where T : CompositionObject
        {
            var property = ((expression as LambdaExpression)?.Body as MemberExpression)?.Member.Name;
            if (property == null)
                throw new ArgumentException();

            compositionObject.StopAnimation(property);
        }

    }
}
