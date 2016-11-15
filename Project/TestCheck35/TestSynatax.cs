using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System;

namespace TestCheck35
{
    [SqlSyntax]
    public static class TestSynatax
    {
        public static IQuery<Non> Empty() => null;
        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods) => string.Empty;
    }

    public static class TestExtensions
    {
        public static string Flat(this string text)=> text.Replace(Environment.NewLine, " ").Replace("\t", " ");
    }
}
