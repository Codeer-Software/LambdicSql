using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestSymbolSub
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
            public int Id { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition_1()
        {
            var condition = Db<DB>.Sql(db => new Condition(true, db.tbl_staff.id == 1) || (new Condition(true, db.tbl_staff.id == 2) && new Condition(false, db.tbl_staff.id == 3) && new Condition(true, db.tbl_staff.id == 4)));

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    name = db.tbl_staff.name
                }).
                From(db.tbl_staff).
                Where(condition));

            var datas = _connection.Query(sql).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff
WHERE ((tbl_staff.id) = (@p_0)) OR (((tbl_staff.id) = (@p_1)) AND ((tbl_staff.id) = (@p_2)))",
1, 2, 4);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition_2()
        {
            var condition = Db<DB>.Sql(db => new Condition(false, db.tbl_staff.id == 2) && new Condition(false, db.tbl_staff.id == 3) && new Condition(false, db.tbl_staff.id == 4));
            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    name = db.tbl_staff.name
                }).
                From(db.tbl_staff).
                Where(condition));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition_3()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var condition = Db<DB>.Sql(db => new Condition(true, 100 < Sum(db.tbl_remuneration.money)));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
GROUP BY tbl_remuneration.staff_id
HAVING (@p_0) < (SUM(tbl_remuneration.money))",
(decimal)100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition_4()
        {
            var condition = Db<DB>.Sql(db => new Condition(false, 100 < Sum(db.tbl_remuneration.money)));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
GROUP BY tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Condition_5()
        {
            var exp = Db<DB>.Sql(db => 100 < Sum(db.tbl_remuneration.money));
            var condition = Db<DB>.Sql(db => new Condition(false, exp));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(condition.Body));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
GROUP BY tbl_remuneration.staff_id");
        }

    }
}
