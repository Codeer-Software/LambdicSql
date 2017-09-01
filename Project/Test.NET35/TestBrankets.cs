using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static Test.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;
using LambdicSql.ConverterServices.SymbolConverters;

namespace Test
{
    [TestClass]
    public class TestBrankets
    {
        SqlConnection _connection = new SqlConnection();

        [TestMethod]
        public void TestOR()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => a == b || a == b);
            AssertEx.AreEqual(sql, _connection, "@a = @b OR @a = @b", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestNot()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => !(a == b || a == b));
            AssertEx.AreEqual(sql, _connection, "NOT (@a = @b OR @a = @b)", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestNotCast()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => !(bool)(object)(a == b || a == b));
            AssertEx.AreEqual(sql, _connection, "NOT (@a = @b OR @a = @b)", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestIsNull()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;
            string a = "a";
            var sql = Db<DB>.Sql(db => a == null);
            AssertEx.AreEqual(sql, _connection, "@a IS NULL", new Params { { "@a", "a" } });
        }

        [TestMethod]
        public void TestIsNullBinaryExpression()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;
            string a = "a";
            string b = "b";
            string c = null;
            var sql = Db<DB>.Sql(db => a + b == c);
            AssertEx.AreEqual(sql, _connection, "(@a + @b) IS NULL", new Params { { "@a", "a" }, { "@b", "b" } });
        }
    }
}
