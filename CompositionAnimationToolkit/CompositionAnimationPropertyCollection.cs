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
    public class CompositionAnimationPropertyCollection : Dictionary<string, object>
    {

        public CompositionObject Get(string property)
        {
            return (CompositionObject)this[property];
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

}
