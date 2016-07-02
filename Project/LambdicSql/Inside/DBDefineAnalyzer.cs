using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    static class DBDefineAnalyzer
    {
        internal static IQuery<T, T> CreateQuery<T>(Expression<Func<T>> define)
           where T : class
        {
            var db = new DbInfo();
            foreach (var column in FindColumns(typeof(T), new string[0]))
            {
                db.Add(column);
            }
            var lambdaNameAndSubQuery = new Dictionary<string, Expression>();
            GetSubQuery(new string[0], define.Body, lambdaNameAndSubQuery);
            db.AddSubQueryTableInfo(lambdaNameAndSubQuery);

            var indexInSelect = db.GetLambdaNameAndColumn().Keys.ToList();
            var create = ExpressionToCreateFunc.ToCreateUseDbResult<T>(name => indexInSelect.IndexOf(name), define.Body);
            return new ClauseMakingQuery<T, T, IClause>(db, create, new IClause[0]);
        }
        
        static IEnumerable<ColumnInfo> FindColumns(Type type, IEnumerable<string> names)
        {
            if (SupportedTypeSpec.IsSupported(type))
            {
                //TODO@ if exist difference lambda name and sql name, I'll implement the spec. 
                var name = string.Join(".", names.ToArray());
                return new[] { new ColumnInfo(type, name, name) };
            }
            else if (type.IsClass)
            {
                var list = new List<ColumnInfo>();
                foreach (var p in type.GetProperties().Where(e => e.DeclaringType == type))
                {
                    list.AddRange(FindColumns(p.PropertyType, names.Concat(new[] { p.Name }).ToArray()));
                }
                return list;
            }
            throw new NotSupportedException();
        }

        static void GetSubQuery(string[] names, Expression exp, Dictionary<string, Expression> lambdaNameAndSubQuery)
        {
            var newExp = exp as NewExpression;
            if (newExp != null)
            {
                if (newExp.Members != null)
                {
                    GetSubQueryFromAnonymous(names, newExp.Arguments.ToArray(), newExp.Members.ToArray(), lambdaNameAndSubQuery);
                }
            }
            else
            {
                var initExp = exp as MemberInitExpression;
                if (initExp != null)
                {
                    foreach (var bind in initExp.Bindings)
                    {
                        var assign = bind as MemberAssignment;
                        if (assign != null)
                        { 
                            GetSubQuery(names.Concat(new[] { assign.Member.Name }).ToArray(), assign.Expression, lambdaNameAndSubQuery);
                        }
                    }
                }
                else
                {
                    GetSubQueryFromMethod(names, exp, lambdaNameAndSubQuery);
                }
            }
        }

        static void GetSubQueryFromAnonymous(string[] names, Expression[] args, MemberInfo[] members, Dictionary<string, Expression> lambdaNameAndSubQuery)
        {
            var newArgs = new List<Expression>();
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                var propInfo = members[i] as PropertyInfo;
                string[] currentNames = null;
                Type paramType = null;
                if (propInfo != null)
                {
                    currentNames = names.Concat(new[] { propInfo.Name }).ToArray();
                    paramType = propInfo.PropertyType;
                }
                else
                {
                    //.net3.5
                    var method = members[i] as MethodInfo;
                    paramType = method.ReturnType;
                    currentNames = names.Concat(new[] { method.Name }).ToArray();
                }
                var newExp = arg as NewExpression;
                if (newExp != null)
                {
                    GetSubQuery(currentNames.ToArray(), newExp, lambdaNameAndSubQuery);
                }
                GetSubQueryFromMethod(currentNames, arg, lambdaNameAndSubQuery);
            }
        }

        static void GetSubQueryFromMethod(string[] currentNames, Expression arg, Dictionary<string, Expression> lambdaNameAndSubQuery)
        {
            var methodCall = arg as MethodCallExpression;
            if (methodCall != null &&
                methodCall.Arguments.Count == 1 &&
                typeof(IQuery).IsAssignableFrom(methodCall.Arguments[0].Type) &&
                methodCall.Method.DeclaringType == typeof(QueryExtensions) &&
                methodCall.Method.Name == "Cast")
            {
                var name = string.Join(".", currentNames);
                lambdaNameAndSubQuery[name] = methodCall;
            }
        }
    }
}
