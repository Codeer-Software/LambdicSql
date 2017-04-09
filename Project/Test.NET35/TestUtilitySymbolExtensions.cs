using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static Test.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestUtilitySymbolExtensions
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
        public void Test_Text_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"tbl_staff.id".ToSql(),
                    name = (string)"tbl_staff.name".ToSql(),
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS id,
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Text_2()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"{0}".ToSql(db.tbl_staff.id),
                    name = (string)"{0}".ToSql(db.tbl_staff.name),
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS id,
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Text_3()
        {
            var exp1 = Db<DB>.Sql(db => db.tbl_staff.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_staff.name);
            var sql = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = (int)"{0}".ToSql(exp1),
                    name = (string)"{0}".ToSql(exp2),
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS id,
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Format2WaySql_1()
        {
            var text =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var sql = Db<DB>.Sql(db => text.TwoWaySql(
                100,
                Where(3000 < db.tbl_remuneration.money)
                ));

            var datas = _connection.Query<Remuneration>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Format2WaySql_2()
        {
            var exp1 = Db<DB>.Sql(db => 100);
            var exp2 = Db<DB>.Sql(db => Where(3000 < db.tbl_remuneration.money));

            var text =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var sql = Db<DB>.Sql(db => text.TwoWaySql(
                exp1,
                exp2
                ));

            var datas = _connection.Query<Remuneration>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_ColumnOnlyAndDirectValue()
        {
            var text =
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	/*0*/tbl_remuneration.money/**/ AS /*1*/money/**/
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*2*/WHERE tbl_remuneration.money = 100/**/";

            var sql = Db<DB>.Sql(db => text.TwoWaySql(
                db.tbl_remuneration.money,
                db.tbl_remuneration.money.ColumnOnly(),
                Where(3000.DirectValue() < db.tbl_remuneration.money)
                ));

            sql.Gen(_connection);

            var datas = _connection.Query<Remuneration>(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
WHERE (3000) < (tbl_remuneration.money)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OperatorHelpers()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new Staff
                {
                    id = db.tbl_staff.id,
                    name = db.tbl_staff.name,
                }).
                From(db.tbl_staff).
                Where(
                    db.tbl_staff.name.GreaterThan("a") &&
                    db.tbl_staff.name.GreaterThanOrEqual("b") &&
                    db.tbl_staff.name.LessThan("c") &&
                    db.tbl_staff.name.LessThanOrEqual("d")
                    ));

            sql.Gen(_connection);
            var datas = _connection.Query(sql).ToList();
        }
    }
}
