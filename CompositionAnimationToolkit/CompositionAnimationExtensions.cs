using CompositionAnimationToolkit.Internal;
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

        static T ApplyParameters<T>(this T animation, CompositionAnimationPropertyCollection parameters) where T : CompositionAnimation
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

        public static string ExpressionToPropertyName(Expression expression)
        {
            var property = ((expression as LambdaExpression)?.Body as MemberExpression)?.Member.Name;
            if (property == null)
                throw new ArgumentException();
            else
                return property;
        }

        static CompositionExpression ExpressionToCompositionExpression(Expression expression)
        {
            var parameters = new CompositionAnimationPropertyCollection();
            return new CompositionExpression { Expression = GetCompositionString(expression, parameters), Parameters = parameters };
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
                    return GetCompositionString(u.Operand, parameters);
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
                return "(" + GetCompositionString(exp, parameters) + ")";
            else
                return GetCompositionString(exp, parameters);
        }

        static string GetConditionalExpression(ConditionalExpression exp, CompositionAnimationPropertyCollection parameters)
        {
            return GetCompositionString(exp.Test, parameters) + " ? " + GetCompositionString(exp.IfTrue, parameters) + " : " + GetCompositionString(exp.IfFalse, parameters);
        }

        static string GetMethodCallExpression(MethodCallExpression ca, CompositionAnimationPropertyCollection parameters)
        {
            if (ca.Method.DeclaringType != typeof(CompositionPropertySetExtensions))
                return ca.Method.Name + "(" + string.Join(",", ca.Arguments.Select(a => GetCompositionString(a, parameters))) + ")";
            else
                return GetCompositionString(ca.Arguments[0], parameters) + "." + GetCompositionString(ca.Arguments[1], parameters);
        }

        static string GetCompositionString(Expression exp, CompositionAnimationPropertyCollection parameters)
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
                    return GetCompositionString(((System.Linq.Expressions.LambdaExpression)exp).Body, parameters);
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
}
