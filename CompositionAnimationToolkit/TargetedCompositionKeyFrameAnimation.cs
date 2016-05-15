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

        Action<Tanimation, float, TValue, CompositionEasingFunction> insertEasingKey;

        Expression expression;
        int duration = 500;
        int count = 1;
        List<KeyFrame<TValue>> Values;
        private int delay;



        internal TargetedKeyFrameAnimation(CompositionObject compositionObject, Expression expression, Func<Compositor, Tanimation> createAnimation, Action<Tanimation, float, TValue, CompositionEasingFunction> insertEasingKey, KeyFrame<TValue>[] values)
        {
            this.createAnimation = createAnimation;
            this.insertEasingKey = insertEasingKey;
            Target = compositionObject;
            this.expression = expression;
            Values = values.ToList();

            DistributeKeyFrames();
        }

        private void DistributeKeyFrames()
        {
            var length = Values.Count;

            if (length > 0)
            {
                float start = 0, end = 1;
                int startIndex = 0, endIndex = length - 1;

                for (int i = 0; i < length; i++)
                {
                    if (Values[i].Floating)
                    {
                        var next = Values.Skip(i + 1).FirstOrDefault(k => !k.Floating);
                        if (next != null)
                        {
                            endIndex = Values.IndexOf(next);
                            end = next.TimeStamp;
                        }
                        else
                        {
                            endIndex = length - 1;
                            end = 1;
                        }

                        Values[i].TimeStamp = lerp(start, end, normalize(startIndex, endIndex, i));
                        Values[i].Floating = true;
                    }
                    else
                    {
                        startIndex = i;
                        start = Values[i].TimeStamp;
                    }
                }
            }
        }

        public int Count => Values.Count;

        public KeyFrame<TValue> this[int index] => Values[index];


        float normalize(float min, float max, float pos) => (pos - min) / (max - min);

        float lerp(float value1, float value2, float pos) => (1 - pos) * value1 + pos * value2;

        public override TargetedCompositionAnimation Start()
        {
            if (Animation == null)
            {
                Values = Values.OrderBy(k => k.TimeStamp).ToList();
                DistributeKeyFrames();

                var ani = createAnimation(Target.Compositor);
                TargetProperty = ExpressionHelper.ExpressionToPropertyName(expression);

                if (Values.Count == 1)
                    insertEasingKey(ani, 1, Values[0].Value, Values[0].Easeing);
                else
                    foreach (var item in Values)
                        insertEasingKey(ani, item.TimeStamp, item.Value, item.Easeing);


                ani.Duration = TimeSpan.FromMilliseconds(duration);
                ani.DelayTime = TimeSpan.FromMilliseconds(delay);
                if (count > 0)
                {
                    ani.IterationBehavior = AnimationIterationBehavior.Count;
                    ani.IterationCount = count;
                }
                else
                    ani.IterationBehavior = AnimationIterationBehavior.Forever;

                Animation = ani;
            }

            Target.StartAnimation(TargetProperty, Animation);

            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> InsertKeyframe(KeyFrame<TValue> keyFrame)
        {
            if (keyFrame.TimeStamp < 0)
            {
                keyFrame.TimeStamp = 1;
                keyFrame.Floating = true;
            }

            //var prepend = Values.FirstOrDefault(k => keyFrame.TimeStamp > k.TimeStamp);
            //if (prepend != null)
            //    Values.Insert(Values.IndexOf(prepend), keyFrame);            
            //else
                Values.Add(keyFrame);

            Values = Values.OrderBy(k => k.TimeStamp).ToList();
            DistributeKeyFrames();
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> EaseIn(CompositionEasingFunction func)
        {
            Values[0].Easeing = func;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> EaseOut(CompositionEasingFunction func)
        {
            Values.Last().Easeing = func;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> Duration(int time)
        {
            duration = time;
            return this;
        }

        public TargetedKeyFrameAnimation<TValue, Tanimation> Delay(int time)
        {
            delay = time;
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
