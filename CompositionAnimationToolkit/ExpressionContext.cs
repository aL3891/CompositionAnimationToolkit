using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit.Internal
{
    public class ExpressionContext
    {
        protected ExpressionContext()
        {

        }

        public float Abs(float value) => 0;
        public Vector2 Abs(Vector2 value) => new Vector2(0);
        public Vector3 Abs(Vector3 value) => new Vector3(0);
        public Vector4 Abs(Vector4 value) => new Vector4(0);
        public float Acos(float value) => 0;
        public float Asin(float value) => 0;
        public float Atan(float value) => 0;
        public float Ceiling(float value) => 0;
        public float Clamp(float value, float min, float max) => 0;
        public Color ColorLerp(Color ColorTo, Color ColorFrom, float Progression) => default(Color);
        public Color ColorLerpHSL(Color ColorTo, Color ColorFrom, float Progression) => default(Color);
        public Color ColorLerpRGB(Color ColorTo, Color ColorFrom, float Progression) => default(Color);
        public Quaternion Concatenate(Quaternion value, Quaternion value2) => default(Quaternion);
        public float Cos(float value) => 0;
        public Vector2 Distance(Vector2 value1, Vector2 value2) => default(Vector2);
        public Vector3 Distance(Vector3 value1, Vector3 value2) => default(Vector3);
        public Vector4 Distance(Vector4 value1, Vector4 value2) => default(Vector4);
        public float DistanceSquared(Vector2 value1, Vector2 value2) => 0;
        public float DistanceSquared(Vector3 value1, Vector3 value2) => 0;
        public float DistanceSquared(Vector4 value1, Vector4 value2) => 0;
        public float Floor(float value) => 0;
        public Vector2 Inverse(Vector2 value) => default(Vector2);
        public Vector3 Inverse(Vector3 value) => default(Vector3);
        public Vector4 Inverse(Vector4 value) => default(Vector4);
        public Vector2 Length(Vector2 value) => default(Vector2);
        public Vector3 Length(Vector3 value) => default(Vector3);
        public Vector4 Length(Vector4 value) => default(Vector4);
        public Vector2 LengthSquared(Vector2 value) => default(Vector2);
        public Vector3 LengthSquared(Vector3 value) => default(Vector3);
        public Vector4 LengthSquared(Vector4 value) => default(Vector4);
        public Vector2 Lerp(Vector2 value1, Vector2 value2, float progress) => default(Vector2);
        public Vector3 Lerp(Vector3 value1, Vector3 value2, float progress) => default(Vector3);
        public Vector4 Lerp(Vector4 value1, Vector4 value2, float progress) => default(Vector4);
        public Matrix3x2 Lerp(Matrix3x2 value1, Matrix3x2 value2, float progress) => default(Matrix3x2);
        public Matrix4x4 Lerp(Matrix4x4 value1, Matrix4x4 value2, float progress) => default(Matrix4x4);
        public float Ln(float value) => 0;
        public float Log10(float value) => 0;
        public Matrix3x2 Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32) => default(Matrix3x2);
        public Matrix3x2 Matrix3x2CreateFromScale(Vector2 scale) => default(Matrix3x2);
        public Matrix3x2 Matrix3x2CreateFromTranslation(Vector2 translation) => default(Matrix3x2);
        public Matrix4x4 Matrix4x4(
            float M11, float M12, float M13, float M14,
            float M21, float M22, float M23, float M24,
            float M31, float M32, float M33, float M34,
            float M41, float M42, float M43, float M44) => default(Matrix4x4);

        public Matrix4x4 Matrix4x4CreateFromScale(Vector3 scale) => default(Matrix4x4);
        public Matrix4x4 Matrix4x4CreateFromTranslation(Vector3 translation) => default(Matrix4x4);
        public Matrix4x4 Matrix4x4CreateFromAxisAngle(Vector3 axis, float angle) => default(Matrix4x4);
        public float Max(float value1, float value2) => 0;
        public float Min(float value1, float value2) => 0;
        public float Mod(float dividend, float divisor) => 0;
        public float Normalize() => 0;
        public Vector2 Normalize(Vector2 value) => default(Vector2);
        public Vector3 Normalize(Vector3 value) => default(Vector3);
        public Vector4 Normalize(Vector4 value) => default(Vector4);
        public float Pow(float value, int power) => 0;
        public Quaternion QuaternionCreateFromAxisAngle(Vector3 axis, float angle) => default(Quaternion);
        public float Round(float value) => 0;
        public Vector2 Scale(Vector2 value, float factor) => default(Vector2);
        public Vector3 Scale(Vector3 value, float factor) => default(Vector3);
        public Vector4 Scale(Vector4 value, float factor) => default(Vector4);
        public Matrix3x2 Scale(Matrix3x2 value, float factor) => default(Matrix3x2);
        public Matrix4x4 Scale(Matrix4x4 value, float factor) => default(Matrix4x4);
        public float Sin(float value) => 0;
        public Quaternion Slerp(Quaternion value1, Quaternion value2, float progress) => default(Quaternion);
        public float Sqrt(float value) => 0;
        public float Square(float value) => 0;
        public float Tan(float value) => 0;
        public float ToDegrees(float radians) => 0;
        public float ToRadians(float degrees) => 0;
        public Vector2 Transform(Vector2 value, Matrix3x2 matrix) => default(Vector2);
        public Vector4 Transform(Vector4 value, Matrix4x4 matrix) => default(Vector4);
        public Vector2 Vector2(float x, float y) => default(Vector2);
        public Vector3 Vector3(float x, float y, float z) => default(Vector3);
        public Vector4 Vector4(float x, float y, float z, float w) => default(Vector4);
    }
    
    public class ExpressionContext<TProperty, TTarget> : ExpressionContext
    {
        public TProperty StartingValue { get; set; }
        public TProperty EndValue { get; set; }
        public TTarget Target { get; set; }
    }
    
}
