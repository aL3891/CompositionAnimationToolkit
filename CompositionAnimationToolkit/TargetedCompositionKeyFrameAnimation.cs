using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class TargetedKeyFrameAnimation<TValue, Tanimation> : TargetedCompositionAnimation where Tanimation : KeyFrameAnimation
    {
        Func<Compositor, Tanimation> createAnimation;
        Action<Tanimation, float, TValue> insertKey;
        Action<Tanimation, float, TValue, CompositionEasingFunction> insertEasingKey;

        Expression expression;
        int duration = 500;
        int count = 1;
        CompositionEasingFunction easeIn;
        CompositionEasingFunction easeOut;
        public TValue[] values;

        internal TargetedKeyFrameAnimation(CompositionObject compositionObject, Expression expression, Func<Compositor, Tanimation> createAnimation, Action<Tanimation, float, TValue> insertKey, Action<Tanimation, float, TValue, CompositionEasingFunction> insertEasingKey, TValue[] values)
        {
            this.createAnimation = createAnimation;
            this.insertKey = insertKey;
            this.insertEasingKey = insertEasingKey;
            Target = compositionObject;
            this.expression = expression;
            this.values = values;
        }

        public override TargetedCompositionAnimation Start()
        {
            if (Animation == null)
            {
                var ani = createAnimation(Target.Compositor);
                TargetProperty = ExpressionHelper.ExpressionToPropertyName(expression);

                if (values.Length == 1)
                {
                    if (easeOut != null)
                        insertEasingKey(ani, 1, values[0], easeOut);
                    else
                        insertKey(ani, 1, values[0]);
                }
                else
                {
                    var step = 1f / (values.Length - 1);
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i == 0 && easeIn != null)
                            insertEasingKey(ani, step * i, values[i], easeIn);
                        else if (i == values.Length - 1 && easeOut != null)
                            insertEasingKey(ani, step * i, values[i], easeOut);
                        else
                            insertKey(ani, step * i, values[i]);
                    }

                }


                ani.Duration = TimeSpan.FromMilliseconds(duration);
                if (count > 0)
                {
                    ani.IterationBehavior = AnimationIterationBehavior.Count;
                    ani.IterationCount = count;
                }
                else
                {
                    ani.IterationBehavior = AnimationIterationBehavior.Forever;
                }

                Animation = ani;
            }

            Target.StartAnimation(TargetProperty, Animation);

            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> EaseIn(CompositionEasingFunction func)
        {
            easeIn = func;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> EaseOut(CompositionEasingFunction func)
        {
            easeOut = func;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> Duration(int time)
        {
            duration = time;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> Loop(int c)
        {
            count = c;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> Loop()
        {
            count = -1;
            return this;
        }
    }

}
