using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using Windows.UI;
using Windows.UI.Composition;

namespace CompositionAnimationToolkit
{
    public static class CompositionAnimationExtensions
    {
        public static CompositionAnimationPropertyCollection ExpressionLambda(this ExpressionAnimation animation, Expression<Func<ExpressionContext, object>> expression)
        {
            var ce = ExpressionToCompositionExpression(expression);
            animation.Expression = ce.Expression;
            animation.ApplyParameters(ce.Parameters);
            return ce.Parameters;
        }

        public static CompositionAnimationPropertyCollection ExpressionLambda<TProperty, TTarget>(this ExpressionAnimation animation, Expression<Func<ExpressionContext<TProperty, TTarget>, object>> expression)
        {
            var ce = ExpressionToCompositionExpression(expression);
            animation.Expression = ce.Expression;
            animation.ApplyParameters(ce.Parameters);
            return ce.Parameters;
        }

        public static KeyFrameAnimation InsertExpressionLambdaKeyFrame(this KeyFrameAnimation animation, float normalizedProgressKey, Expression<Func<ExpressionContext, object>> expression)
        {
            var ce = ExpressionToCompositionExpression(expression);
            animation.InsertExpressionKeyFrame(normalizedProgressKey, ce.Expression);

            animation.ApplyParameters(ce.Parameters);
            return animation;
        }

        public static T ApplyParameters<T>(this T animation, CompositionAnimationPropertyCollection parameters) where T : CompositionAnimation
        {
            foreach (var p in parameters.Keys.ToList())
            {
                var type = parameters[p].GetType();

                if (type == typeof(float))
                    animation.SetScalarParameter(p, (float)parameters[p]);
                else if (parameters[p] is CompositionObject)
                    animation.SetReferenceParameter(p, (CompositionObject)parameters[p]);
                else if (parameters[p] is CompositionPropertySetWrapper)
                {
                    parameters[p] = ((CompositionPropertySetWrapper)parameters[p]).PropertySet;
                    animation.SetReferenceParameter(p, (CompositionObject)parameters[p]);
                }
                else if (type == typeof(Vector2))
                    animation.SetVector2Parameter(p, (Vector2)parameters[p]);
                else if (type == typeof(Vector3))
                    animation.SetVector3Parameter(p, (Vector3)parameters[p]);
                else if (type == typeof(Vector4))
                    animation.SetVector4Parameter(p, (Vector4)parameters[p]);
                else if (type == typeof(Matrix3x2))
                    animation.SetMatrix3x2Parameter(p, (Matrix3x2)parameters[p]);
                else if (type == typeof(Matrix4x4))
                    animation.SetMatrix4x4Parameter(p, (Matrix4x4)parameters[p]);
                else if (type == typeof(Quaternion))
                    animation.SetQuaternionParameter(p, (Quaternion)parameters[p]);
                else if (type == typeof(Color))
                    animation.SetColorParameter(p, (Color)parameters[p]);
                else
                {
                    parameters[p] = CompositionPropertySetExtensions.ToPropertySet(parameters[p], animation.Compositor);
                    animation.SetReferenceParameter(p, (CompositionObject)parameters[p]);
                }

            }
            return animation;
        }

        public static string ExpressionToPropertyName(Expression expression) {
            var property = ((expression as LambdaExpression)?.Body as MemberExpression)?.Member.Name;
            if (property == null)
                throw new ArgumentException();
            else
                return property;
        }

        public static CompositionExpression ExpressionToCompositionExpression(Expression expression)
        {
            var parameters = new CompositionAnimationPropertyCollection();
            return new CompositionExpression { Expression = ExpressionToCompositionString(expression, parameters), Parameters = parameters };
        }

        static string GetMemberExpression(MemberExpression m, CompositionAnimationPropertyCollection parameters)
        {
            if (m.Expression is MemberExpression)
                return GetMemberExpression((MemberExpression)m.Expression, parameters) + "." + m.Member.Name;
            else
            {
                if (m.Member.DeclaringType.GetTypeInfo().IsGenericType && m.Member.DeclaringType.GetGenericTypeDefinition() == typeof(ExpressionContext<,>))
                {
                    return "this." + m.Member.Name;
                }
                else
                {
                    if (!parameters.ContainsKey(m.Member.Name) && m.Member is FieldInfo && m.Expression is ConstantExpression)
                        parameters.Add(m.Member.Name, ((FieldInfo)m.Member).GetValue(((ConstantExpression)m.Expression).Value));
                    return m.Member.Name;
                }
            }
        }
        
        static string GetBinaryExpression(BinaryExpression b, CompositionAnimationPropertyCollection parameters)
        {
            string operand = null;

            switch (b.NodeType)
            {
                case ExpressionType.Add:
                    operand = "+";
                    break;
                case ExpressionType.AddChecked:
                    operand = "+";
                    break;
                case ExpressionType.And:
                    operand = "&";
                    break;
                case ExpressionType.AndAlso:
                    operand = "&&";
                    break;
                case ExpressionType.Coalesce:
                    operand = "??";
                    break;
                case ExpressionType.Divide:
                    operand = "/";
                    break;
                case ExpressionType.ExclusiveOr:
                    operand = "||";
                    break;
                case ExpressionType.GreaterThan:
                    operand = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    operand = ">=";
                    break;
                case ExpressionType.LeftShift:
                    operand = ">>";
                    break;
                case ExpressionType.LessThan:
                    operand = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    operand = "<=";
                    break;
                case ExpressionType.Modulo:
                    operand = "%";
                    break;
                case ExpressionType.Multiply:
                    operand = "*";
                    break;
                case ExpressionType.MultiplyChecked:
                    operand = "*";
                    break;
                case ExpressionType.NotEqual:
                    operand = "!=";
                    break;
                case ExpressionType.Or:
                    operand = "|";
                    break;
                case ExpressionType.OrElse:
                    operand = "||";
                    break;
                case ExpressionType.RightShift:
                    operand = "<<";
                    break;
                case ExpressionType.Subtract:
                    operand = "-";
                    break;
                case ExpressionType.SubtractChecked:
                    operand = "-";
                    break;
                case ExpressionType.AddAssign:
                    operand = "+=";
                    break;
                case ExpressionType.AndAssign:
                    operand = "&=";
                    break;
                case ExpressionType.DivideAssign:
                    operand = "/=";
                    break;
                case ExpressionType.ExclusiveOrAssign:
                    operand = "^=";
                    break;
                case ExpressionType.LeftShiftAssign:
                    operand = "<<=";
                    break;
                case ExpressionType.ModuloAssign:
                    operand = "%=";
                    break;
                case ExpressionType.MultiplyAssign:
                    operand = "*=";
                    break;
                case ExpressionType.OrAssign:
                    operand = "|=";
                    break;
                case ExpressionType.PowerAssign:
                    operand = "^";
                    break;
                case ExpressionType.RightShiftAssign:
                    operand = ">>=";
                    break;
                case ExpressionType.SubtractAssign:
                    operand = "-=";
                    break;
                case ExpressionType.AddAssignChecked:
                    operand = "+=";
                    break;
                case ExpressionType.MultiplyAssignChecked:
                    operand = "*=";
                    break;
                case ExpressionType.SubtractAssignChecked:
                    operand = "-=";
                    break;
                default:
                    throw new ArgumentException();
            }

            return GetExpressionWithParameters(b.Left, parameters) + " " + operand + " " + GetExpressionWithParameters(b.Right, parameters);
        }

        static string GetUnaryExpression(UnaryExpression u, CompositionAnimationPropertyCollection parameters)
        {
            string operand = null;

            switch (u.NodeType)
            {
                case ExpressionType.Negate:
                    operand = "!";
                    break;
                case ExpressionType.UnaryPlus:
                    operand = "+";
                    break;
                case ExpressionType.NegateChecked:
                    operand = "!";
                    break;
                case ExpressionType.Not:
                    operand = "!";
                    break;
                case ExpressionType.PreIncrementAssign:
                    operand = "++";
                    break;
                case ExpressionType.PreDecrementAssign:
                    operand = "--";
                    break;
                case ExpressionType.Convert:
                    return ExpressionToCompositionString(u.Operand, parameters);
                case ExpressionType.PostIncrementAssign:
                    return GetExpressionWithParameters(u.Operand, parameters) + "++";
                case ExpressionType.PostDecrementAssign:
                    return GetExpressionWithParameters(u.Operand, parameters) + "--";
                case ExpressionType.OnesComplement:
                    operand = "~";
                    break;
                default:
                    throw new ArgumentException(u.NodeType.ToString());
            }

            return operand + GetExpressionWithParameters(u.Operand, parameters);
        }

        static string GetExpressionWithParameters(Expression exp, CompositionAnimationPropertyCollection parameters)
        {
            if (exp is BinaryExpression)
                return "(" + ExpressionToCompositionString(exp, parameters) + ")";
            else
                return ExpressionToCompositionString(exp, parameters);
        }

        static string GetConditionalExpression(ConditionalExpression exp, CompositionAnimationPropertyCollection parameters)
        {
            return ExpressionToCompositionString(exp.Test, parameters) + " ? " + ExpressionToCompositionString(exp.IfTrue, parameters) + " : " + ExpressionToCompositionString(exp.IfFalse, parameters);
        }

        static string GetMethodCallExpression(MethodCallExpression ca, CompositionAnimationPropertyCollection parameters)
        {
            if (ca.Method.DeclaringType != typeof(CompositionPropertySetExtensions))
                return ca.Method.Name + "(" + string.Join(",", ca.Arguments.Select(a => ExpressionToCompositionString(a, parameters))) + ")";
            else
                return ExpressionToCompositionString(ca.Arguments[0], parameters) + "." + ExpressionToCompositionString(ca.Arguments[1], parameters);
        }

        static string ExpressionToCompositionString(Expression exp, CompositionAnimationPropertyCollection parameters)
        {

            if (exp is BinaryExpression)
                return GetBinaryExpression((BinaryExpression)exp, parameters);

            if (exp is UnaryExpression)
                return GetUnaryExpression((UnaryExpression)exp, parameters);

            switch (exp.NodeType)
            {
                case ExpressionType.Call:
                    return GetMethodCallExpression((MethodCallExpression)exp, parameters);
                case ExpressionType.Conditional:
                    return GetConditionalExpression((ConditionalExpression)exp, parameters);
                case ExpressionType.Constant:
                    return (exp as ConstantExpression).Value.ToString();
                case ExpressionType.Lambda:
                    return ExpressionToCompositionString(((System.Linq.Expressions.LambdaExpression)exp).Body, parameters);
                case ExpressionType.MemberAccess:
                    return GetMemberExpression((MemberExpression)exp, parameters);
                case ExpressionType.Parameter:
                    var p = (ParameterExpression)exp;
                    return p.Name;
                default:
                    throw new ArgumentException(exp.NodeType.ToString());
            }
        }
    }

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
