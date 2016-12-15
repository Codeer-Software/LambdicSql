using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using System.Diagnostics;

namespace TestCheck35
{
    [TestClass]
    public class TestWindow
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
            public decimal Val { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Avg1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = (decimal)Window.Avg(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	AVG(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Avg2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = (decimal)Window.Avg(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	AVG(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Avg3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = (decimal)Window.Avg(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	AVG(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Sum(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Sum(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Sum(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum4()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Sum(AggregatePredicate.All, exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(ALL tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_Asterisk()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(new Asterisk()).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(*)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_AggregatePredicate()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(AggregatePredicate.All, db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(ALL tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_Asterisk_AggregatePredicate()
        {
            if (_connection.GetType().Name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(AggregatePredicate.All, new Asterisk()).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(ALL *)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Max()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Max(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MAX(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Max2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Max(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MAX(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Max3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Max(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MAX(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Min()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Min(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MIN(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Min2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Min(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MIN(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Min3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Min(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MIN(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.First_Value(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	FIRST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.First_Value(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	FIRST_VALUE(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.First_Value(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	FIRST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Last_Value()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Last_Value(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Last_Value2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Last_Value(3).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAST_VALUE(@p_0)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Last_Value3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Last_Value(exp).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rank()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Rank().
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Dense_Rank()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Dense_Rank().
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	DENSE_RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Percent_Rank()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    Val = (decimal)Window.Percent_Rank().
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	PERCENT_RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cume_Dist()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = (decimal)Window.Cume_Dist().
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CUME_DIST()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Ntile()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Ntile(2).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTILE(@p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Ntile2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var exp = Sql<DB>.Create(db => 2);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Ntile(exp).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTILE(@p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value1()
        {
            if (_connection.GetType().Name != "OracleConnection" &&
                _connection.GetType().Name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Nth_Value(db.tbl_remuneration.money, 2).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTH_VALUE(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value2()
        {
            if (_connection.GetType().Name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Nth_Value(2, (long)db.tbl_remuneration.money).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTH_VALUE(:p_0, tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value3()
        {
            if (_connection.GetType().Name != "OracleConnection" &&
                _connection.GetType().Name != "DB2Connection") return;

            var exp1  = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2  = Sql<DB>.Create(db => (long)2);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Nth_Value(exp1, exp2).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTH_VALUE(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", (long)2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag1_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag1_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(3).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(@p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag1_3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(exp).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag2_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money, 2).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag2_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(3, db.tbl_remuneration.id).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(@p_0, tbl_remuneration.id)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag2_3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => 2);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(exp1, exp2).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag3_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money, 2, 100).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money, @p_0, @p_1)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2, (decimal)100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag3_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(2000, db.tbl_remuneration.id, db.tbl_remuneration.id).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(@p_0, tbl_remuneration.id, tbl_remuneration.id)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag3_3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => 2);
            var exp3 = Sql<DB>.Create(db => 100);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(exp1.Body, exp2, exp3).
                            Over(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAG(tbl_remuneration.money, @p_0, @p_1)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2, 100);
        }
        
        //TODO PARTITION BY のテストが少し弱いかな？
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_PartitionBy()
        {
            if (_connection.GetType().FullName == "System.Data.SQLite.SQLiteConnection") return;
            if (_connection.GetType().FullName == "MySql.Data.MySqlClient.MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_staff.name,
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC,
			tbl_remuneration.payment_date DESC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows1_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS 1 PRECEDING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows1_2()
        {
            if (_connection.GetType().Name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows((int)db.tbl_remuneration.money))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS tbl_remuneration.money PRECEDING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows1_3()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var exp = Sql<DB>.Create(db => 1);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(exp))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS 1 PRECEDING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows2_1()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 3))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 3 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows2_2()
        {
            if (_connection.GetType().Name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows((int)db.tbl_remuneration.money, (int)db.tbl_remuneration.money))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN tbl_remuneration.money PRECEDING AND tbl_remuneration.money FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows2_3()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var exp1 = Sql<DB>.Create(db => 1);
            var exp2 = Sql<DB>.Create(db => 3);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(exp1, exp2))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 3 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC,
			tbl_remuneration.payment_date DESC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var exp1 = Sql<DB>.Create(db => new Asc(db.tbl_remuneration.money));
            var exp2 = Sql<DB>.Create(db => new Desc(db.tbl_remuneration.payment_date));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(exp1.Cast<ISortedBy>(), exp2.Cast<ISortedBy>()),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC,
			tbl_remuneration.payment_date DESC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over1_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over1_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var partitionBy = Sql<DB>.Create(db => new PartitionBy(db.tbl_remuneration.payment_date));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(partitionBy)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over2_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over2_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var partitionBy = Sql<DB>.Create(db => new PartitionBy(db.tbl_remuneration.payment_date));
            var orderBy = Sql<DB>.Create(db => new OrderBy(new Asc(db.tbl_remuneration.money)));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(partitionBy, orderBy)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over3_1()
        {
            if (_connection.GetType().Name != "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over3_2()
        {
            if (_connection.GetType().Name != "NpgsqlConnection") return;

            var partitionBy = Sql<DB>.Create(db => new PartitionBy(db.tbl_remuneration.payment_date));
            var rows = Sql<DB>.Create(db => new Rows(1, 5));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(partitionBy, rows)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over4_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over4_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var partitionBy = Sql<DB>.Create(db => new PartitionBy(db.tbl_remuneration.payment_date));
            var orderBy = Sql<DB>.Create(db => new OrderBy(new Asc(db.tbl_remuneration.money)));
            var rows = Sql<DB>.Create(db => new Rows(1, 5));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(partitionBy, orderBy, rows)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over5_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var partitionBy = Sql<DB>.Create(db => new PartitionBy(db.tbl_remuneration.payment_date));
            var orderBy = Sql<DB>.Create(db => new OrderBy(new Asc(db.tbl_remuneration.money)));
            var rows = Sql<DB>.Create(db => new Rows(1, 5));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new OrderBy(new Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over5_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            
            var orderBy = Sql<DB>.Create(db => new OrderBy(new Asc(db.tbl_remuneration.money)));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(orderBy)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over6_1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over6_2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            
            var orderBy = Sql<DB>.Create(db => new OrderBy(new Asc(db.tbl_remuneration.money)));
            var rows = Sql<DB>.Create(db => new Rows(1, 5));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(orderBy, rows)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over7_1()
        {
            if (_connection.GetType().Name != "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over7_2()
        {
            if (_connection.GetType().Name != "NpgsqlConnection") return;
            
            var rows = Sql<DB>.Create(db => new Rows(1, 5));
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over(rows)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }
    }
}
