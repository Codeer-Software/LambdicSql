using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Window;

//TODO last
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
        public void Test_Avg()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Avg(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	AVG(tbl_remuneration.money)OVER(
	PARTITION BY
		tbl_remuneration.payment_date 
	ORDER BY
		tbl_remuneration.money ASC 
	ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Sum(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money)OVER(
	PARTITION BY
		tbl_remuneration.payment_date 
	ORDER BY
		tbl_remuneration.money ASC 
	ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)OVER(
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
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MAX(tbl_remuneration.money)OVER(
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
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MIN(tbl_remuneration.money)OVER(
	PARTITION BY
		tbl_remuneration.payment_date 
	ORDER BY
		tbl_remuneration.money ASC 
	ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.First_Value(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	FIRST_VALUE(tbl_remuneration.money)OVER(
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
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LAST_VALUE(tbl_remuneration.money)OVER(
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
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	RANK()OVER(
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
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	DENSE_RANK()OVER(
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
                    Val = Window.Percent_Rank().
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	PERCENT_RANK()OVER(
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
                    Val = Window.Cume_Dist().
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CUME_DIST()OVER(
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
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTILE(@p_0)OVER(
	ORDER BY
		tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration",
(long)2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value()
        {
            if (_connection.GetType().Name != "OracleConnection" &&
                _connection.GetType().Name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Nth_Value(db.tbl_remuneration.money, 2).
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NTH_VALUE(tbl_remuneration.money, @p_0)OVER(
	ORDER BY
		tbl_remuneration.money ASC 
	ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration",
(long)2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money).
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money, 2).
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag3()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Lag(db.tbl_remuneration.money, 2, 100).
                            Over<decimal>(null,
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                null)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money)),
                                new Rows(1))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)OVER(
	PARTITION BY
		tbl_remuneration.payment_date 
	ORDER BY
		tbl_remuneration.money ASC 
	ROWS @p_0 PRECEDING) AS Val
FROM tbl_remuneration",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Val = Window.Count(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)OVER(
	PARTITION BY
		tbl_remuneration.payment_date 
	ORDER BY
		tbl_remuneration.money ASC,
		tbl_remuneration.payment_date DESC 
	ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }
    }
}
