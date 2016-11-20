using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System.Diagnostics;

namespace TestCheck35
{
    public class TestFuncs
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public class SelectData1
        {
            public decimal Val1 { get; set; }
            public decimal Val2 { get; set; }
            public decimal Val3 { get; set; }
            public decimal Val4 { get; set; }
            public decimal Val5 { get; set; }
            public decimal Val6 { get; set; }
            public decimal Val7 { get; set; }
            public decimal Val8 { get; set; }
            public decimal Val9 { get; set; }
            public decimal Val10 { get; set; }
        }

        public class SelectData2
        {
            public decimal Val1 { get; set; }
            public decimal Val2 { get; set; }
        }

        public class SelectData3
        {
            public decimal Val { get; set; }
        }

        public void Test_Aggregate()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Val1 = Sum(db.tbl_remuneration.money),
                   Val2 = Sum(AggregatePredicate.All, db.tbl_remuneration.money),
                   Val3 = Sum(AggregatePredicate.Distinct, db.tbl_remuneration.money),
                   Val4 = Count(db.tbl_remuneration.money),
                   Val5 = Count(new Asterisk()),
                   Val6 = Count(AggregatePredicate.All, db.tbl_remuneration.money),
                   Val7 = Count(AggregatePredicate.Distinct, db.tbl_remuneration.money),
                   Val8 = Avg(db.tbl_remuneration.money),
                   Val9 = Min(db.tbl_remuneration.money),
                   Val10 = Max(db.tbl_remuneration.money),
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money) AS Val1,
	SUM(ALL tbl_remuneration.money) AS Val2,
	SUM(DISTINCT tbl_remuneration.money) AS Val3,
	COUNT(tbl_remuneration.money) AS Val4,
	COUNT(*) AS Val5,
	COUNT(ALL tbl_remuneration.money) AS Val6,
	COUNT(DISTINCT tbl_remuneration.money) AS Val7,
	AVG(tbl_remuneration.money) AS Val8,
	MIN(tbl_remuneration.money) AS Val9,
	MAX(tbl_remuneration.money) AS Val10
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        public void Test_Abs_Round()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Val1 = Abs(db.tbl_remuneration.money),
                   Val2 = Round(db.tbl_remuneration.money, 2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	ABS(tbl_remuneration.money) AS Val1,
	ROUND(tbl_remuneration.money, @p_1) AS Val2
FROM tbl_remuneration");
        }

        public void Test_Mod()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData3
               {
                   Val = Mod(db.tbl_remuneration.money, 2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MOD(tbl_remuneration.money, @p_1) AS Val
FROM tbl_remuneration");
        }

        public void Test_Concat()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Concat(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
        }

        public void Test_Length()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Length(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LENGTH(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        public void Test_Len()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Len(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LEN(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        public void Test_Lower()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Lower(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LOWER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        public void Test_Upper()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Upper(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
        }

        public void Test_Replace()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Replace(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	UPPER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        public void Test_Substring()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Substring(db.tbl_staff.name, 0, 1)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUBSTRING(tbl_staff.name, @p_2, @p_3) AS Val
FROM tbl_staff");
        }
    }
}