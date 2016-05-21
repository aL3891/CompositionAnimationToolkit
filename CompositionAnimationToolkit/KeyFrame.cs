using CompositionAnimationToolkit.Internal;
using System;
using System.Linq.Expressions;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class KeyFrame
    {
        public static KeyFrame<T> Create<T>(T value) => new KeyFrame<T> { Value = value };
        public static KeyFrame<T> CreateInferred<T>(Expression<Func<ExpressionContext, T>> value) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value) };
        public static KeyFrame<T> Create<T>(Expression<Func<ExpressionContext<T, CompositionObject>, T>> value) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value) };
        public static KeyFrame<T> Create<R, T>(Expression<Func<ExpressionContext<R, T>, T>> value) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value) };
        public static KeyFrame<T> Create<T>(double timestamp, T value, CompositionEasingFunction easing = null) => new KeyFrame<T> { Value = value, TimeStamp = (float)timestamp, Easeing = easing };
        public static KeyFrame<T> CreateInferred<T>(double timestamp, Expression<Func<ExpressionContext, T>> value, CompositionEasingFunction easing = null) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value), TimeStamp = (float)timestamp, Easeing = easing };
        public static KeyFrame<T> Create<T>(double timestamp, Expression<Func<ExpressionContext<T, CompositionObject>, T>> value, CompositionEasingFunction easing = null) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value), TimeStamp = (float)timestamp, Easeing = easing };
        public static KeyFrame<T> Create<R, T>(double timestamp, Expression<Func<ExpressionContext<R, T>, T>> value, CompositionEasingFunction easing = null) => new KeyFrame<T> { Expression = ExpressionHelper.ExpressionToCompositionExpression(value), TimeStamp = (float)timestamp, Easeing = easing };
    }

    public class KeyFrame<TValue>
    {
        private float timeStamp;

        public CompositionExpression Expression { get; set; }
        public TValue Value { get; set; }

        public CompositionEasingFunction Easeing { get; set; }
        public bool Floating { get; set; } = true;

        public float TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
                Floating = false;
            }
        }

        internal KeyFrame()
        {

        }

        public static implicit operator KeyFrame<TValue>(TValue value) => new KeyFrame<TValue> { Value = value, timeStamp = -1 };
    }
}
