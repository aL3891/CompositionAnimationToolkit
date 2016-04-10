using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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

            throw new ArgumentException();
        }

    }
}
