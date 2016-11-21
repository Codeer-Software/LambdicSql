using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Utils;
using static LambdicSql.Funcs;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace TestCheck35
{
    public class TestUtils
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public class SelectData
        {
            public decimal Total { get; set; }
        }

        public class SelectData2
        {
            public int Id { get; set; }
        }
        
        public void Test_Cast1()
        {
            var sub = Sql<DB>.Create(db =>
                Select(new { total = Sum(db.tbl_remuneration.money) }).
                From(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData { Total = sub.Cast<decimal>() }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money) AS total
	FROM tbl_remuneration) AS Total
FROM tbl_remuneration");
        }

        public void Test_Cast2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    Total = Select(new { total = Sum(db.tbl_remuneration.money) }).
                                From(db.tbl_remuneration).Cast<decimal>()
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money) AS total
	FROM tbl_remuneration) AS Total
FROM tbl_remuneration");
        }

        public void Test_Condition1()
        {
            var condition = Sql<DB>.Create(db => Condition(true, db.tbl_staff.id == 1) || (Condition(true, db.tbl_staff.id == 2) && Condition(false, db.tbl_staff.id == 3) && Condition(true, db.tbl_staff.id == 4)));
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name
                }).
                From(db.tbl_staff).
                Where(condition));

            var datas = _connection.Query(query).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff
WHERE ((tbl_staff.id) = (@p_0)) OR (((tbl_staff.id) = (@p_1)) AND ((tbl_staff.id) = (@p_2)))");
        }

        public void Test_Condition2()
        {
            var condition = Sql<DB>.Create(db => Condition(false, db.tbl_staff.id == 2) && Condition(false, db.tbl_staff.id == 3) && Condition(false, db.tbl_staff.id == 4));
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name
                }).
                From(db.tbl_staff).
                Where(condition));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff");
        }

        public void Test_Condition3()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var condition = Sql<DB>.Create(db => Condition(true, 100 < Sum(db.tbl_remuneration.money)));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
GROUP BY tbl_remuneration.staff_id
HAVING (@p_0) < (SUM(tbl_remuneration.money))");
        }

        public void Test_Condition4()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var condition = Sql<DB>.Create(db => Condition(false, 100 < Sum(db.tbl_remuneration.money)));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
GROUP BY tbl_remuneration.staff_id");
        }

        public void Test_Text()
        {
            var query = Sql<DB>.Create(db =>
                Select(new Staff
                {
                    id = (int)Text("tbl_staff.id"),
                    name = Text<string>("tbl_staff.name"),
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS id,
	tbl_staff.name AS name
FROM tbl_staff");
        }

        public void Test_Format2WaySql()
        {
            var sql = @"
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var query = Sql<DB>.Create(db => TwoWaySql(sql,
                100,
                Where(3000 < db.tbl_remuneration.money)
                ));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + @p_0 AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
WHERE (@p_1) < (tbl_remuneration.money)");
        }

        public void Test_ColumnOnly()
        {
            var sql = @"
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	/*0*/tbl_remuneration.money/**/ AS /*1*/money/**/
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*2*/WHERE tbl_remuneration.money = 100/**/";

            var query = Sql<DB>.Create(db => TwoWaySql(sql,
                db.tbl_remuneration.money,
                ColumnOnly(db.tbl_remuneration.money),
                Where(3000 < db.tbl_remuneration.money)
                ));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
WHERE (@p_0) < (tbl_remuneration.money)");
        }

        //おっとテキストのフォーマット
        /*
        [TestMethod]
        public void TestFormatText2()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = Text<decimal>("{0} + 1000", db.tbl_remuneration.money),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }
        */
    }
}
