using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System;
using LambdicSql;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

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

        public static void Gen(this ISqlExpressionBase query, IDbConnection con)
        {
          //  if (con.GetType() != typeof(SqlConnection)) return;
            Debug.Print("AssertEx.AreEqual(query, _connection," + 
                Environment.NewLine + "@\"" + query.ToSqlInfo(con.GetType()).SqlText + "\");");
        }
    }

    public static class AssertEx
    {
        public static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected)
        {
            var actual = query.ToSqlInfo(con.GetType()).SqlText;
            if (con.GetType().Name == "OracleConnection") expected = expected.Replace("@", ":");
            if (actual != expected) throw new InvalidProgramException();
        }
    }
}
