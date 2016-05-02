using CompositionAnimationToolkit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public static class CompositionAnimationExtensions
    {
        public static CompositionAnimationPropertyCollection ExpressionLambda(this ExpressionAnimation animation, Expression<Func<ExpressionContext, object>> expression) =>
            ExpressionHelper.ExpressionLambda(animation, expression);

        public static CompositionAnimationPropertyCollection ExpressionLambda<TProperty, TTarget>(this ExpressionAnimation animation, Expression<Func<ExpressionContext<TProperty, TTarget>, object>> expression) =>
            ExpressionHelper.ExpressionLambda(animation, expression);

        public static KeyFrameAnimation InsertExpressionLambdaKeyFrame(this KeyFrameAnimation animation, float normalizedProgressKey, Expression<Func<ExpressionContext, object>> expression) =>
            ExpressionHelper.InsertExpressionLambdaKeyFrame(animation, normalizedProgressKey, expression);
    }
}
