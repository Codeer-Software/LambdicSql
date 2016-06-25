using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class SelectDefineAnalyzer
    {
        internal static SelectInfo MakeSelectInfo(NewExpression exp, IReadOnlyDictionary<string, ColumnInfo> dbColumns)
        {
            var select = new SelectInfo(dbColumns);
            
            for (int i = 0; i < exp.Arguments.Count; i++)
            {
                var propInfo = exp.Members[i] as PropertyInfo;
                var argMember = exp.Arguments[i] as MemberExpression;
                if (argMember != null)
                {
                    AnalyzeNormal(select, propInfo, argMember);
                    continue;
                }
                var argMethod = exp.Arguments[i] as MethodCallExpression;
                if (argMethod != null)
                {
                    AnalyzeMethod(select, propInfo, argMethod);
                    continue;
                }
                throw new NotSupportedException();
            }
            return select;
        }

        static void AnalyzeNormal(SelectInfo select, PropertyInfo member, MemberExpression argMember)
        {
            select.Add(member.Name,
                SelectElementInfo.DbColumnElement(argMember.GetElementName()),
                new ColumnInfo(member.PropertyType, new[] { member.Name }));
        }

        static void AnalyzeMethod(SelectInfo select, PropertyInfo propInfo, MethodCallExpression argMethod)
        {
            //TODO commonalize！
            var arguments = new List<object>();
            foreach (var arg in argMethod.Arguments.Skip(1))
            {
                var member = arg as MemberExpression;
                if (member != null)
                {
                    var name = member.GetElementName();
                    ColumnInfo col;
                    if (select.DbColumns.TryGetValue(name, out col))
                    {
                        arguments.Add(col);
                    }
                    else
                    {
                        dynamic func = Expression.Lambda(member).Compile();
                        arguments.Add(func());
                    }
                    continue;
                }
                var constant = arg as ConstantExpression;
                if (constant != null)
                {
                    dynamic func = Expression.Lambda(constant).Compile();
                    arguments.Add(func().ToString());
                    continue;
                }
                throw new NotSupportedException();
            }
            select.Add(propInfo.Name,
                SelectElementInfo.FunctionElement(argMethod.Method.Name, arguments),
                new ColumnInfo(propInfo.PropertyType, new[] { propInfo.Name }));
        }
    }
}
