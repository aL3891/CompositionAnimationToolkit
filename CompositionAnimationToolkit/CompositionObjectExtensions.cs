using CompositionAnimationToolkit.Internal;
using System;
using System.Linq.Expressions;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public static class CompositionObjectExtensions
    {
        public static TargetedCompositionAnimation CreateAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject => new TargetedCompositionAnimation(compositionObject, expression, animation);

        public static TargetedExpressionAnimation CreateAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) where T : CompositionObject => new TargetedExpressionAnimation(compositionObject, expression, animationexpression);

        public static TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, float>> expression, params float[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation>(compositionObject, expression, c => c.CreateScalarKeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public static TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, Vector2>> expression, params Vector2[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation>(compositionObject, expression, c => c.CreateVector2KeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public static TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, Vector3>> expression, params Vector3[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation>(compositionObject, expression, c => c.CreateVector3KeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public static TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, Vector4>> expression, params Vector4[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation>(compositionObject, expression, c => c.CreateVector4KeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public static TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, Quaternion>> expression, params Quaternion[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation>(compositionObject, expression, c => c.CreateQuaternionKeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public static TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation> CreateAnimation<T>(this T compositionObject, Expression<Func<T, Color>> expression, params Color[] values) where T : CompositionObject => new TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation>(compositionObject, expression, c => c.CreateColorKeyFrameAnimation(), (a, t, v) => a.InsertKeyFrame(t, v), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);


        public static TargetedCompositionAnimation StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, CompositionAnimation animation) where T : CompositionObject => CreateAnimation(compositionObject, expression, animation).Start();

        public static TargetedExpressionAnimation StartAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) where T : CompositionObject => (TargetedExpressionAnimation)CreateAnimation(compositionObject, expression, animationexpression).Start();

        public static TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, float>> expression, params float[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        public static TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, Vector2>> expression, params Vector2[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        public static TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, Vector3>> expression, params Vector3[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        public static TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, Vector4>> expression, params Vector4[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        public static TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, Quaternion>> expression, params Quaternion[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        public static TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation> StartAnimation<T>(this T compositionObject, Expression<Func<T, Color>> expression, params Color[] values) where T : CompositionObject => (TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation>)CreateAnimation(compositionObject, expression, values).Start();
        
        public static void StopAnimation<T, R>(this T compositionObject, Expression<Func<T, R>> expression) where T : CompositionObject
        {
            compositionObject.StopAnimation(ExpressionHelper.ExpressionToPropertyName(expression));
        }

        public static TypeAnnotatedCompositionObject<R> AsAnnotated<R>(this CompositionObject input, R instance) => new TypeAnnotatedCompositionObject<R> { Target = input };
        public static TypeAnnotatedCompositionObject<T> AsAnnotated<T>(this CompositionObject input) => new TypeAnnotatedCompositionObject<T> { Target = input };
    }
}
