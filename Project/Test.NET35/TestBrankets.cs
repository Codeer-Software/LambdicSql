using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;

namespace Test
{
    [TestClass]
    public class TestBrankets
    {
        SqlConnection _connection = new SqlConnection();
        
        [TestMethod]
        public void TestAddMultiply_1()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => a * b + a * b);
            AssertEx.AreEqual(sql, _connection, "@a * @b + @a * @b", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestAddMultiply_2()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => a + b * a + b);
            AssertEx.AreEqual(sql, _connection, "@a + @b * @a + @b", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestAddMultiply_3()
        {
            int a = 0;
            int b = 1;
            var sql = Db<DB>.Sql(db => (a + b) * (a + b));
            AssertEx.AreEqual(sql, _connection, "(@a + @b) * (@a + @b)", new Params { { "@a", 0 }, { "@b", 1 } });
        }

        [TestMethod]
        public void TestAndOr_1()
        {
            bool a = true;
            bool b = false;
            var sql = Db<DB>.Sql(db => a && b || a && b);
            AssertEx.AreEqual(sql, _connection, "@a AND @b OR @a AND @b", new Params { { "@a", true }, { "@b", false } });
        }

        [TestMethod]
        public void TestAndOr_2()
        {
            bool a = true;
            bool b = false;
            var sql = Db<DB>.Sql(db => a || b && a || b);
            AssertEx.AreEqual(sql, _connection, "@a OR @b AND @a OR @b", new Params { { "@a", true }, { "@b", false } });
        }

        [TestMethod]
        public void TestAndOr_3()
        {
            bool a = true;
            bool b = false;
            var sql = Db<DB>.Sql(db => (a || b) && (a || b));
            AssertEx.AreEqual(sql, _connection, "(@a OR @b) AND (@a OR @b)", new Params { { "@a", true }, { "@b", false } });
        }

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
