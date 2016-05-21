using CompositionAnimationToolkit.Internal;
using System;
using System.Linq.Expressions;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TypeAnnotatedCompositionObject<T>
    {
        public CompositionObject Target { get; set; }

        public TargetedCompositionAnimation CreateAnimation<R>(Expression<Func<T, R>> expression, CompositionAnimation animation) => new TargetedCompositionAnimation(Target, expression, animation);

        public TargetedCompositionAnimation CreateAnimation<R>(Expression<Func<T, R>> expression, Expression<Func<ExpressionContext<R, T>, object>> animationexpression) => new TargetedExpressionAnimation(Target, expression, animationexpression);

        public TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation> CreateAnimation(Expression<Func<T, float>> expression, params KeyFrame<float>[] values) => new TargetedKeyFrameAnimation<float, ScalarKeyFrameAnimation>(Target, expression, c => c.CreateScalarKeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation> CreateAnimation(Expression<Func<T, Vector2>> expression, params KeyFrame<Vector2>[] values) => new TargetedKeyFrameAnimation<Vector2, Vector2KeyFrameAnimation>(Target, expression, c => c.CreateVector2KeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation> CreateAnimation(Expression<Func<T, Vector3>> expression, params KeyFrame<Vector3>[] values) => new TargetedKeyFrameAnimation<Vector3, Vector3KeyFrameAnimation>(Target, expression, c => c.CreateVector3KeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation> CreateAnimation(Expression<Func<T, Vector4>> expression, params KeyFrame<Vector4>[] values) => new TargetedKeyFrameAnimation<Vector4, Vector4KeyFrameAnimation>(Target, expression, c => c.CreateVector4KeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation> CreateAnimation(Expression<Func<T, Quaternion>> expression, params KeyFrame<Quaternion>[] values) => new TargetedKeyFrameAnimation<Quaternion, QuaternionKeyFrameAnimation>(Target, expression, c => c.CreateQuaternionKeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);
        public TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation> CreateAnimation(Expression<Func<T, Color>> expression, params KeyFrame<Color>[] values) => new TargetedKeyFrameAnimation<Color, ColorKeyFrameAnimation>(Target, expression, c => c.CreateColorKeyFrameAnimation(), (a, t, v, e) => a.InsertKeyFrame(t, v, e), values);

        public void StopAnimation<R>(Expression<Func<T, R>> expression)
        {
            Target.StopAnimation(ExpressionHelper.ExpressionToPropertyName(expression));
        }
    }
}
