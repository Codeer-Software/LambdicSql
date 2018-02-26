﻿using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.ConverterServices.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    public class SqlBuilder
    {
        public static Sql FromFormattableString(string format, object[] argsObj)
        {
            var args = new ICode[argsObj.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = new ParameterCode(argsObj[i]);
            }
            return new Sql(new CodeParts.StringFormatCode(format, args));
        }

        public static Sql<T> FromFormattableString<T>(string format, object[] argsObj)
        {
            var args = new ICode[argsObj.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = new ParameterCode(argsObj[i]);
            }
            return new Sql<T>(new CodeParts.StringFormatCode(format, args));
        }

        public static Sql FromExpressionContainFormattableString(Expression exp)
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<Non>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = (string)obj;
            var array = methodExp.Arguments[1] as NewArrayExpression;

            var args = new ICode[array.Expressions.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = converter.ConvertToCode(array.Expressions[i]);
            }
            return new Sql(new CodeParts.StringFormatCode(text, array.Expressions.Select(e => converter.ConvertToCode(e)).ToArray()));
        }

        public static Sql FromExpressionContainFormattableString<T>(Expression exp) where T : class
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = (string)obj;
            var array = methodExp.Arguments[1] as NewArrayExpression;

            var args = new ICode[array.Expressions.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = converter.ConvertToCode(array.Expressions[i]);
            }
            return new Sql(new CodeParts.StringFormatCode(text, args));
        }

        public static Sql<T2> FromExpressionContainFormattableString<T, T2>(Expression exp) where T : class
        {
            var methodExp = exp as MethodCallExpression;
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            var converter = new ExpressionConverter(db);
            var obj = converter.ConvertToObject(methodExp.Arguments[0]);
            var text = (string)obj;
            var array = methodExp.Arguments[1] as NewArrayExpression;

            var args = new ICode[array.Expressions.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = converter.ConvertToCode(array.Expressions[i]);
            }
            return new Sql<T2>(new CodeParts.StringFormatCode(text, args));
        }
    }
}
