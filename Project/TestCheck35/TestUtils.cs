using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Utils;
using static LambdicSql.Funcs;

namespace TestCheck35
{
    [TestClass]
    public class TestUtils
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

        public class SelectData
        {
            public decimal Total { get; set; }
        }

        public class SelectData2
        {
            public int Id { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
WHERE ((tbl_staff.id) = (@p_0)) OR (((tbl_staff.id) = (@p_1)) AND ((tbl_staff.id) = (@p_2)))",
1, 2, 4);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
HAVING (@p_0) < (SUM(tbl_remuneration.money))",
(decimal)100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition5()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var exp = Sql<DB>.Create(db => 100 < Sum(db.tbl_remuneration.money));
            var condition = Sql<DB>.Create(db => Condition(false, exp));

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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Text1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new Staff
                {
                    id = (int)TextSql("tbl_staff.id"),
                    name = TextSql<string>("tbl_staff.name"),
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Text2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new Staff
                {
                    id = (int)TextSql("{0}", db.tbl_staff.id),
                    name = TextSql<string>("{0}", db.tbl_staff.name),
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Text3()
        {
            var exp1 = Sql<DB>.Create(db => db.tbl_staff.id);
            var exp2 = Sql<DB>.Create(db => db.tbl_staff.name);
            var query = Sql<DB>.Create(db =>
                Select(new Staff
                {
                    id = (int)TextSql("{0}", exp1),
                    name = TextSql<string>("{0}", exp2),
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
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Format2WaySql1()
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
WHERE (@p_1) < (tbl_remuneration.money)",
100, (decimal)3000);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Format2WaySql2()
        {
            var exp1 = Sql<DB>.Create(db => 100);
            var exp2 = Sql<DB>.Create(db => Where(3000 < db.tbl_remuneration.money));
            var sql = @"
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var query = Sql<DB>.Create(db => TwoWaySql(sql,
                exp1,
                exp2
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
WHERE (@p_1) < (tbl_remuneration.money)",
100, (decimal)3000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
WHERE (@p_0) < (tbl_remuneration.money)",
(decimal)3000);
        }
    }
}
