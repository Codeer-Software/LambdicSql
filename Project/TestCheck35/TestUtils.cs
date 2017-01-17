using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;

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
            var sub = Db<DB>.Sql(db =>
                Select(Sum(db.tbl_remuneration.money)).
                From(db.tbl_remuneration));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData { Total = sub.Body }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);



            query.Gen(_connection);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Total
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast2()
        {
            //TODO 暗黙キャスト
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Total = Select(Sum(db.tbl_remuneration.money)).
                                From(db.tbl_remuneration)
                }).
                From(db.tbl_remuneration));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Total
FROM tbl_remuneration");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition1()
        {
            var condition = Db<DB>.Sql(db => new Condition(true, db.tbl_staff.id == 1) || (new Condition(true, db.tbl_staff.id == 2) && new Condition(false, db.tbl_staff.id == 3) && new Condition(true, db.tbl_staff.id == 4)));
            var query = Db<DB>.Sql(db =>
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
            var condition = Db<DB>.Sql(db => new Condition(false, db.tbl_staff.id == 2) && new Condition(false, db.tbl_staff.id == 3) && new Condition(false, db.tbl_staff.id == 4));
            var query = Db<DB>.Sql(db =>
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

            var condition = Db<DB>.Sql(db => new Condition(true, 100 < Sum(db.tbl_remuneration.money)));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

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
            var condition = Db<DB>.Sql(db => new Condition(false, 100 < Sum(db.tbl_remuneration.money)));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

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
            var exp = Db<DB>.Sql(db => 100 < Sum(db.tbl_remuneration.money));
            var condition = Db<DB>.Sql(db => new Condition(false, exp));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

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
            var query = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"tbl_staff.id".ToSql(),
                    name = (string)"tbl_staff.name".ToSql(),
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
            var query = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"{0}".ToSql(db.tbl_staff.id),
                    name = (string)"{0}".ToSql(db.tbl_staff.name),
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
            var exp1 = Db<DB>.Sql(db => db.tbl_staff.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_staff.name);
            var query = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"{0}".ToSql(exp1),
                    name = (string)"{0}".ToSql(exp2),
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
            var sql =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var query = Db<DB>.Sql(db => sql.TwoWaySql(
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
            var exp1 = Db<DB>.Sql(db => 100);
            var exp2 = Db<DB>.Sql(db => Where(3000 < db.tbl_remuneration.money));
            var sql =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var query = Db<DB>.Sql(db => sql.TwoWaySql(
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

        class SelectedData
        {
            public int id { get; set; }
        }
        

        //TODO ConcatでSelectが正しくでることのテスト

        //TODO こことは関係ないけどSqlTextがイミュータブルであることのテスト


        //TODO 移動
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void XXX()
        {
            var name = _connection.GetType().Name;
            if (name == "MySqlConnection") return;

            var sub1 = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff));

            var sub2 = Db<DB>.Sql(db =>
                Select(Asterisk(sub1.Body)).
                From(sub1));

            var b = Db<DB>.Sql(db => sub2);

            //TODO あー、分けることができないなー
            var query = Db<DB>.Sql(db =>
                With(sub1, sub2).
                Select(new SelectedData
                {
                    id = b.Body.id
                }).
                From(b)
            );
            
            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"WITH
	sub1 AS
		(SELECT *
		FROM tbl_staff),
	sub2 AS
		(SELECT *
		FROM sub1)
SELECT
	b.id AS id
FROM sub2 b");
        }


        //このテストをどっかにおいておかないと
        class YData
        {
            public int StaffId { get; set; }
            public int AId { get; set; }
            public int BId { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void YYY()
        {
            var a = Db<DB>.Sql(db => db.tbl_remuneration);
            var b = Db<DB>.Sql(db => db.tbl_remuneration);

            var query = Db<DB>.Sql(db =>
                Select(new YData
                {
                    StaffId = db.tbl_staff.id,
                    AId = a.Body.id,
                    BId = b.Body.id
                }).
                From(db.tbl_staff).
                Join(a, a.Body.id == 2).
                Join(b, b.Body.id == 3)
            );

            query.Gen(_connection);
            var datas = _connection.Query<SelectedData>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS StaffId,
	a.id AS AId,
	b.id AS BId
FROM tbl_staff
	JOIN tbl_remuneration a ON (a.id) = (@p_0)
	JOIN tbl_remuneration b ON (b.id) = (@p_1)", 2, 3);
        }


        //TODO あれ？DV2って再帰かけないんだっけ？

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void ZZZ()
        {
            var name = _connection.GetType().Name;
            if (name == "MySqlConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;
            if (name == "NpgsqlConnection") return;


            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));
            
            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                Union(All()).
                Select(new object[] { rec.Body.val + 1}).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var query = Db<DB>.Sql(db => 
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            query.Gen(_connection);

            var datas = _connection.Query<SelectedData>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"WITH
	rec(val) AS
		(SELECT
			@p_0
		UNION ALL
		SELECT
			(rec.val) + (@p_1)
		FROM rec
		WHERE ((rec.val) + (@p_2)) <= (@p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void ZZZ2()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection") return;


            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));

            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                Union(All()).
                Select(new object[] { rec.Body.val + 1 }).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var query = Db<DB>.Sql(db =>
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            var datas = _connection.Query<SelectedData>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"WITH
	RECURSIVE rec(val) AS
		(SELECT
			@p_0
		UNION ALL
		SELECT
			(rec.val) + (@p_1)
		FROM rec
		WHERE ((rec.val) + (@p_2)) <= (@p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void ZZZ3()
        {
            var name = _connection.GetType().Name;
            if (name != "OracleConnection") return;

            var rec = Db<DB>.Sql(db => Recursive(new { val = 0 }));

            var select = Db<DB>.Sql(db =>
                Select(new object[] { 1 }).
                From(Dual).
                Union(All()).
                Select(new object[] { rec.Body.val + 1 }).
                From(rec).
                Where(rec.Body.val + 1 <= 5)
                );

            var query = Db<DB>.Sql(db =>
                With(rec, select).
                Select(rec.Body.val).
                From(rec)
                );

            query.Gen(_connection);

            var datas = _connection.Query<SelectedData>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"WITH
	rec(val) AS
		(SELECT
			:p_0
		FROM DUAL
		UNION ALL
		SELECT
			(rec.val) + (:p_1)
		FROM rec
		WHERE ((rec.val) + (:p_2)) <= (:p_3))
SELECT
	rec.val
FROM rec", 1, 1, 1, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ColumnOnlyAndDirectValue()
        {
            var sql =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	/*0*/tbl_remuneration.money/**/ AS /*1*/money/**/
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*2*/WHERE tbl_remuneration.money = 100/**/";

            var query = Db<DB>.Sql(db => sql.TwoWaySql(
                db.tbl_remuneration.money,
                db.tbl_remuneration.money.ColumnOnly(),
                Where(3000.DirectValue() < db.tbl_remuneration.money)
                ));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
WHERE (3000) < (tbl_remuneration.money)");
        }
    }
}
