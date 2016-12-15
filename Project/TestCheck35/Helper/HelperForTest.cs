﻿using LambdicSql;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestCheck35
{
    [SqlSyntax]
    public static class TestSynatax
    {
        public static IClauseChain<Non> Empty() => null;
        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods) => new SingleText(string.Empty);
    }

    public static class TestExtensions
    {
        public static string Flat(this string text)=> text.Replace(Environment.NewLine, " ").Replace("\t", " ");

        public static void Gen(this ISqlExpressionBase query, IDbConnection con)
        {
            Debug.Print("AssertEx.AreEqual(query, _connection," + 
                Environment.NewLine + "@\"" + query.ToSqlInfo(con.GetType()).SqlText + "\");");
        }

        public static string GetStringAddExp(this IDbConnection con)
        {
            switch (con.GetType().Name)
            {
                case "SqlConnection":
                case "MySqlConnection":
                    return "+";
            }
            return "||";
        }
     }

    public class Params : Dictionary<string, object> { }
    public class DbParams : Dictionary<string, DbParam> { }

    public static class AssertEx
    {
        public static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected, params object[] args)
        {
            int i = 0;
            AreEqual(query, con, expected, args.ToDictionary(e => "@p_" + i++, e => new DbParam { Value = e }));
        }

        public static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected, Params args)
            => AreEqual(query, con, expected, (Dictionary<string, object>)args);

        public static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected, DbParams args)
            => AreEqual(query, con, expected, (Dictionary<string, DbParam>)args);

        public static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected, Dictionary<string, object> args)
            => AreEqual(query, con, expected, args.ToDictionary(e => e.Key, e => new DbParam { Value = e.Value }));

        static void AreEqual(ISqlExpressionBase query, IDbConnection con, string expected, Dictionary<string, DbParam> args)
        {
            var info = query.ToSqlInfo(con.GetType());
            if (con.GetType().Name == "OracleConnection")
            {
                expected = expected.Replace("@", ":");
                args = args.ToDictionary(e => e.Key.Replace("@", ":"), e => e.Value);
            }
            Assert.AreEqual(expected, info.SqlText);
            
            var dbParams = info.DbParams;
            Assert.AreEqual(args.Count, dbParams.Count);
            for (int i = 0; i < dbParams.Count; i++)
            {
                DbParam paramExprected;
                Assert.IsTrue(args.TryGetValue(dbParams.Keys.ToArray()[i], out paramExprected));
                var paramActural = dbParams.Values.ToArray()[i];
                Assert.AreEqual(paramExprected.Value, paramActural.Value);
                Assert.AreEqual(paramExprected.DbType, paramActural.DbType);
                Assert.AreEqual(paramExprected.Direction, paramActural.Direction);
                Assert.AreEqual(paramExprected.SourceColumn, paramActural.SourceColumn);
                Assert.AreEqual(paramExprected.SourceVersion, paramActural.SourceVersion);
                Assert.AreEqual(paramExprected.Precision, paramActural.Precision);
                Assert.AreEqual(paramExprected.Scale, paramActural.Scale);
                Assert.AreEqual(paramExprected.Size, paramActural.Size);
            }
        }
    }
}
