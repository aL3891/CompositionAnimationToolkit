using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public class CompositionPropertySetWrapper
    {
        internal CompositionPropertySet PropertySet { get; set; }

        public CompositionPropertySetWrapper(Compositor copositor)
        {
            PropertySet = copositor.CreatePropertySet();
        }

        public CompositionPropertySetWrapper(CompositionPropertySet propertySet)
        {
            PropertySet = propertySet;
        }
        
        public void StartAnimation(string propertyName, CompositionAnimation animation)
        {
            PropertySet.StartAnimation(propertyName, animation);
        }

        public void StopAnimation(string propertyName)
        {
            PropertySet.StopAnimation(propertyName);
        }

        protected Color GetColor([CallerMemberName]string key = "")
        {
            Color tmp;
            if (PropertySet.TryGetColor(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Matrix3x2 GetMatrix3x2([CallerMemberName]string key = "")
        {
            Matrix3x2 tmp;
            if (PropertySet.TryGetMatrix3x2(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Matrix4x4 GetMatrix4x4([CallerMemberName]string key = "")
        {
            Matrix4x4 tmp;
            if (PropertySet.TryGetMatrix4x4(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Quaternion GetQuaternion([CallerMemberName]string key = "")
        {
            Quaternion tmp;
            if (PropertySet.TryGetQuaternion(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected float GetScalar([CallerMemberName]string key = "")
        {
            float tmp;
            if (PropertySet.TryGetScalar(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Vector2 GetVector2([CallerMemberName]string key = "")
        {
            Vector2 tmp;
            if (PropertySet.TryGetVector2(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Vector3 GetVector3([CallerMemberName]string key = "")
        {
            Vector3 tmp;
            if (PropertySet.TryGetVector3(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected Vector4 GetVector4([CallerMemberName]string key = "")
        {
            Vector4 tmp;
            if (PropertySet.TryGetVector4(key, out tmp) == CompositionGetValueStatus.Succeeded)
                return tmp;
            else
                throw new ArgumentException(key + " not found");
        }

        protected void SetValue(Color value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertColor(key, value);
        }

        protected void SetValue(Matrix3x2 value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertMatrix3x2(key, value);
        }

        protected void SetValue(Matrix4x4 value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertMatrix4x4(key, value);
        }

        protected void SetValue(Quaternion value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertQuaternion(key, value);
        }

        protected void SetValue(float value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertScalar(key, value);
        }

        protected void SetValue(Vector2 value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertVector2(key, value);
        }

        protected void SetValue(Vector3 value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertVector3(key, value);
        }

        protected void SetValue(Vector4 value, [CallerMemberName]string key = "")
        {
            PropertySet.InsertVector4(key, value);
        }
    }

    public static class CompositionPropertySetWrapperExtensions
    {
        public static TypeAnnotatedCompositionObject<T> ToPropertySet<T>(this T compositionObject) where T : CompositionPropertySetWrapper => new TypeAnnotatedCompositionObject<T> { Target = compositionObject.PropertySet };
    }
}
