using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression?.NodeType == ExpressionType.Convert)
            {
                var ex = unaryExpression.Operand;
                var mex = (MemberExpression)ex;
                return mex.Member as PropertyInfo;
            }

            var memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member as PropertyInfo;
        }
    }
}