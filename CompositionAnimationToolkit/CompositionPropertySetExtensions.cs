using System;
using System.Numerics;
using System.Reflection;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public static class CompositionPropertySetExtensions
    {
        public static T Get<T>(this CompositionPropertySet propSet, string key)
        {
            var type = typeof(T);

            if (type == typeof(float))
            {
                float res;
                if (propSet.TryGetScalar(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Vector2))
            {
                Vector2 res;
                if (propSet.TryGetVector2(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Vector3))
            {
                Vector3 res;
                if (propSet.TryGetVector3(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Vector4))
            {
                Vector4 res;
                if (propSet.TryGetVector4(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Matrix3x2))
            {
                Matrix3x2 res;
                if (propSet.TryGetMatrix3x2(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Matrix4x4))
            {
                Matrix4x4 res;
                if (propSet.TryGetMatrix4x4(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Quaternion))
            {
                Quaternion res;
                if (propSet.TryGetQuaternion(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }
            else if (type == typeof(Color))
            {
                Color res;
                if (propSet.TryGetColor(key, out res) == CompositionGetValueStatus.Succeeded)
                    return (T)(object)res;
            }

            throw new ArgumentException("Unsupported type");
        }

        public static CompositionPropertySet ToPropertySet(object input, Compositor compositor)
        {
            var res = compositor.CreatePropertySet();

            foreach (var p in input.GetType().GetTypeInfo().DeclaredProperties)
            {

                if (p.PropertyType == typeof(float))
                    res.InsertScalar(p.Name, (float)p.GetValue(input));
                else if (p.PropertyType == typeof(double))
                    res.InsertScalar(p.Name, (float)(double)p.GetValue(input));
                else if (p.PropertyType == typeof(int))
                    res.InsertScalar(p.Name, (int)p.GetValue(input));
                else if (p.PropertyType == typeof(decimal))
                    res.InsertScalar(p.Name, (float)(decimal)p.GetValue(input));
                else if (p.PropertyType == typeof(Vector2))
                    res.InsertVector2(p.Name, (Vector2)p.GetValue(input));
                else if (p.PropertyType == typeof(Vector3))
                    res.InsertVector3(p.Name, (Vector3)p.GetValue(input));
                else if (p.PropertyType == typeof(Vector4))
                    res.InsertVector4(p.Name, (Vector4)p.GetValue(input));
                else if (p.PropertyType == typeof(Matrix3x2))
                    res.InsertMatrix3x2(p.Name, (Matrix3x2)p.GetValue(input));
                else if (p.PropertyType == typeof(Matrix4x4))
                    res.InsertMatrix4x4(p.Name, (Matrix4x4)p.GetValue(input));
                else if (p.PropertyType == typeof(Quaternion))
                    res.InsertQuaternion(p.Name, (Quaternion)p.GetValue(input));
                else if (p.PropertyType == typeof(Color))
                    res.InsertColor(p.Name, (Color)p.GetValue(input));
            }

            return res;
        }
    }
}
