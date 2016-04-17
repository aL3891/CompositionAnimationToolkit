﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace CompositionAnimationToolkit.Internal
{
    public class ExpressionContext
    {
        protected ExpressionContext()
        {

        }

        public float Abs(float value) { return 0; }
        public Vector2 Abs(Vector2 value) { return new Vector2(0); }
        public Vector3 Abs(Vector3 value) { return new Vector3(0); }
        public Vector4 Abs(Vector4 value) { return new Vector4(0); }
        public float Acos(float value) { return 0; }
        public float Asin(float value) { return 0; }
        public float Atan(float value) { return 0; }
        public float Ceiling(float value) { return 0; }
        public float Clamp(float value, float min, float max) { return 0; }
        public Color ColorLerp(Color ColorTo, Color ColorFrom, float Progression) { return default(Color); }
        public Color ColorLerpHSL(Color ColorTo, Color ColorFrom, float Progression) { return default(Color); }
        public Color ColorLerpRGB(Color ColorTo, Color ColorFrom, float Progression) { return default(Color); }
        public Quaternion Concatenate(Quaternion value, Quaternion value2) { return default(Quaternion); }
        public float Cos(float value) { return 0; }
        public Vector2 Distance(Vector2 value1, Vector2 value2) { return default(Vector2); }
        public Vector3 Distance(Vector3 value1, Vector3 value2) { return default(Vector3); }
        public Vector4 Distance(Vector4 value1, Vector4 value2) { return default(Vector4); }
        public float DistanceSquared(Vector2 value1, Vector2 value2) { return 0; }
        public float DistanceSquared(Vector3 value1, Vector3 value2) { return 0; }
        public float DistanceSquared(Vector4 value1, Vector4 value2) { return 0; }
        public float Floor(float value) { return 0; }
        public Vector2 Inverse(Vector2 value) { return default(Vector2); }
        public Vector3 Inverse(Vector3 value) { return default(Vector3); }
        public Vector4 Inverse(Vector4 value) { return default(Vector4); }
        public Vector2 Length(Vector2 value) { return default(Vector2); }
        public Vector3 Length(Vector3 value) { return default(Vector3); }
        public Vector4 Length(Vector4 value) { return default(Vector4); }
        public Vector2 LengthSquared(Vector2 value) { return default(Vector2); }
        public Vector3 LengthSquared(Vector3 value) { return default(Vector3); }
        public Vector4 LengthSquared(Vector4 value) { return default(Vector4); }
        public Vector2 Lerp(Vector2 value1, Vector2 value2, float progress) { return default(Vector2); }
        public Vector3 Lerp(Vector3 value1, Vector3 value2, float progress) { return default(Vector3); }
        public Vector4 Lerp(Vector4 value1, Vector4 value2, float progress) { return default(Vector4); }
        public Matrix3x2 Lerp(Matrix3x2 value1, Matrix3x2 value2, float progress) { return default(Matrix3x2); }
        public Matrix4x4 Lerp(Matrix4x4 value1, Matrix4x4 value2, float progress) { return default(Matrix4x4); }
        public float Ln(float value) { return 0; }
        public float Log10(float value) { return 0; }
        public Matrix3x2 Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32) { return default(Matrix3x2); }
        public Matrix3x2 Matrix3x2CreateFromScale(Vector2 scale) { return default(Matrix3x2); }
        public Matrix3x2 Matrix3x2CreateFromTranslation(Vector2 translation) { return default(Matrix3x2); }
        public Matrix4x4 Matrix4x4(
            float M11, float M12, float M13, float M14,
            float M21, float M22, float M23, float M24,
            float M31, float M32, float M33, float M34,
            float M41, float M42, float M43, float M44)
        { return default(Matrix4x4); }

        public Matrix4x4 Matrix4x4CreateFromScale(Vector3 scale) { return default(Matrix4x4); }
        public Matrix4x4 Matrix4x4CreateFromTranslation(Vector3 translation) { return default(Matrix4x4); }
        public Matrix4x4 Matrix4x4CreateFromAxisAngle(Vector3 axis, float angle) { return default(Matrix4x4); }
        public float Max(float value1, float value2) { return 0; }
        public float Min(float value1, float value2) { return 0; }
        public float Mod(float dividend, float divisor) { return 0; }
        public float Normalize() { return 0; }
        public Vector2 Normalize(Vector2 value) { return default(Vector2); }
        public Vector3 Normalize(Vector3 value) { return default(Vector3); }
        public Vector4 Normalize(Vector4 value) { return default(Vector4); }
        public float Pow(float value, int power) { return 0; }
        public Quaternion QuaternionCreateFromAxisAngle(Vector3 axis, float angle) { return default(Quaternion); }
        public float Round(float value) { return 0; }
        public Vector2 Scale(Vector2 value, float factor) { return default(Vector2); }
        public Vector3 Scale(Vector3 value, float factor) { return default(Vector3); }
        public Vector4 Scale(Vector4 value, float factor) { return default(Vector4); }
        public Matrix3x2 Scale(Matrix3x2 value, float factor) { return default(Matrix3x2); }
        public Matrix4x4 Scale(Matrix4x4 value, float factor) { return default(Matrix4x4); }
        public float Sin(float value) { return 0; }
        public Quaternion Slerp(Quaternion value1, Quaternion value2, float progress) { return default(Quaternion); }
        public float Sqrt(float value) { return 0; }
        public float Square(float value) { return 0; }
        public float Tan(float value) { return 0; }
        public float ToDegrees(float radians) { return 0; }
        public float ToRadians(float degrees) { return 0; }
        public Vector2 Transform(Vector2 value, Matrix3x2 matrix) { return default(Vector2); }
        public Vector4 Transform(Vector4 value, Matrix4x4 matrix) { return default(Vector4); }
        public Vector2 Vector2(float x, float y) { return default(Vector2); }
        public Vector3 Vector3(float x, float y, float z) { return default(Vector3); }
        public Vector4 Vector4(float x, float y, float z, float w) { return default(Vector4); }
    }

    public class ExpressionContext<TProperty, TTarget> : ExpressionContext
    {
        public TProperty StartingValue { get; set; }
        public TProperty EndValue { get; set; }
        public TTarget Target { get; set; }
    }
}
