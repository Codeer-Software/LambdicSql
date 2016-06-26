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
            var select = new SelectInfo();
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
                    AnalyzeMethod(select, dbColumns, propInfo, argMethod);
                    continue;
                }
                throw new NotSupportedException();
            }
            return select;
        }

        static void AnalyzeNormal(SelectInfo select, PropertyInfo member, MemberExpression argMember)
        {
            select.Add(member.Name,
                new SelectElementInfoDBColumn(argMember.GetElementName()));
        }

        static void AnalyzeMethod(SelectInfo select, IReadOnlyDictionary<string, ColumnInfo> dbColumns, PropertyInfo propInfo, MethodCallExpression argMethod)
        {
            var arguments = ExpressionAnalyzer.GetArguments(dbColumns, argMethod);
            select.Add(propInfo.Name,
                new SelectElementInfoFunction(argMethod.Method.Name, arguments.ToList()));
        }
    }
}
