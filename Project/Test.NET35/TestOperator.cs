using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static Test.Helper.DBProviderInfo;
using static Test.Symbol;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestOperator
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext);
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Calc()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0;
            var query = Db<DB>.Sql(db => val1 + val2 - val3 / val4 * val5);

            AssertEx.AreEqual(query, _connection,
            @"((@val1) + (@val2)) - (((@val3) / (@val4)) * (@val5))", Zero(5));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_String1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;
            if (name == "NpgsqlConnection") return;

            string val1 = "", val2 = "";
            var query = Db<DB>.Sql(db => val1 + val2);
            AssertEx.AreEqual(query, _connection,
            @"(@val1) + (@val2)", EmptyString(2));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_String2()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "MySqlConnection") return;

            string val1 = "", val2 = "";
            var query = Db<DB>.Sql(db => val1 + val2);
            AssertEx.AreEqual(query, _connection,
            @"(@val1) || (@val2)", EmptyString(2));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition1()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0;
            var query = Db<DB>.Sql(db =>
                val1 == val2 &&
                val3 != val4
                );
            AssertEx.AreEqual(query, _connection,
            @"((@val1) = (@val2)) AND ((@val3) <> (@val4))", Zero(4));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition2()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0, val8 = 0;
            var query = Db<DB>.Sql(db =>
                val1 < val2 &&
                val3 <= val4 &&
                val5 > val6 &&
                val7 >= val8
                );
            AssertEx.AreEqual(query, _connection,
            @"((((@val1) < (@val2)) AND ((@val3) <= (@val4))) AND ((@val5) > (@val6))) AND ((@val7) >= (@val8))", Zero(8));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition3()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0;
            var query = Db<DB>.Sql(db =>
                val1 == val2 &&
                val3 == val4 ||
                val5 == val6);
            AssertEx.AreEqual(query, _connection,
            @"(((@val1) = (@val2)) AND ((@val3) = (@val4))) OR ((@val5) = (@val6))", Zero(6));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Brackets()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0, val8 = 0, val9 = 0, val10 = 0;
            var query = Db<DB>.Sql(db =>
               ((val1 == val2 && val3 == val4) || (val5 == val6 && val7 == val8)) && (val9 == val10));
            AssertEx.AreEqual(query, _connection,
            @"((((@val1) = (@val2)) AND ((@val3) = (@val4))) OR (((@val5) = (@val6)) AND ((@val7) = (@val8)))) AND ((@val9) = (@val10))", Zero(10));
        }

        static Dictionary<string, object> Zero(int count) => Enumerable.Range(0, count).ToDictionary(e => "@val" + (e + 1), e => (object)0);
        static Dictionary<string, object> EmptyString(int count) => Enumerable.Range(0, count).ToDictionary(e => "@val" + (e + 1), e => (object)string.Empty);

        class SelectData
        {
            public int IntVal { get; set; }
            public string StrVal { get; set; }
            public double DoubleVal { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Query_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.MySQL)) return;

            int a = 1;
            string b = string.Empty;
            double c = 1;
            int d = 1;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    IntVal = a + a - a * a / a %a,
                    StrVal = b + b + b,
                    DoubleVal = c + c - c * c / c,
                }).
                From(db.tbl_remuneration).
                Where(a == d && a != d && a < d && a <= d && a > d || a >= d)
                );

            Assert.IsTrue(0 < _connection.Query(sql).ToList().Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	((@a) + (@a)) - ((((@a) * (@a)) / (@a)) % (@a)) AS IntVal,
	((@b) + (@b)) + (@b) AS StrVal,
	((@c) + (@c)) - (((@c) * (@c)) / (@c)) AS DoubleVal
FROM tbl_remuneration
WHERE ((((((@a) = (@d)) AND ((@a) <> (@d))) AND ((@a) < (@d))) AND ((@a) <= (@d))) AND ((@a) > (@d))) OR ((@a) >= (@d))",
new Params { { "@a", a }, { "@b", b }, { "@c", c }, { "@d", d } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Query_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle, TargetDB.Postgre, TargetDB.SQLite, TargetDB.DB2)) return;

            int a = 1;
            string b = string.Empty;
            double c = 1;
            int d = 1;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    IntVal = a + a - a * a,
                    StrVal = b + b + b,
                    DoubleVal = c + c - c * c / c,
                }).
                From(db.tbl_remuneration).
                Where(a == d && a != d && a < d && a <= d && a > d || a >= d)
                );

            Assert.IsTrue(0 < _connection.Query(sql).ToList().Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	((@a) + (@a)) - ((@a) * (@a)) AS IntVal,
	((@b) || (@b)) || (@b) AS StrVal,
	((@c) + (@c)) - (((@c) * (@c)) / (@c)) AS DoubleVal
FROM tbl_remuneration
WHERE ((((((@a) = (@d)) AND ((@a) <> (@d))) AND ((@a) < (@d))) AND ((@a) <= (@d))) AND ((@a) > (@d))) OR ((@a) >= (@d))",
new Params { { "@a", a }, { "@b", b }, { "@c", c }, { "@d", d } });
        }
    }
}
