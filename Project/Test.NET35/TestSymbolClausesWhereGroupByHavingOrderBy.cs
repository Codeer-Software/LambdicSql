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
    public class TestSymbolClausesWhereGroupByHavingOrderBy
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

        public class SelectData1
        {
            public string Name { get; set; }
            public decimal Count { get; set; }
        }

        public class SelectedData2
        {
            public int Id { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Where()
        {
            var sql = Db<DB>.Sql(db =>
               Select(Asterisk(db.tbl_remuneration)).
               From(db.tbl_remuneration).
               Where(db.tbl_remuneration.id == 1));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
WHERE (tbl_remuneration.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Where_Start()
        {
            var sql = Db<DB>.Sql(db =>
               Select(Asterisk(db.tbl_remuneration)).
               From(db.tbl_remuneration) +
               Where(db.tbl_remuneration.id == 1));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
WHERE (tbl_remuneration.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Where_Exp()
        {
            var exp = Db<DB>.Sql(db => db.tbl_remuneration.id == 1);

            var sql = Db<DB>.Sql(db =>
               Select(Asterisk(db.tbl_remuneration)).
               From(db.tbl_remuneration).
               Where(exp));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration
WHERE (tbl_remuneration.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupBy()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupBy(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupBy_Start()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration) + 
               GroupBy(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByRollup()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY ROLLUP(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByRollup_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration) + 
               GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY ROLLUP(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup_Start()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration) + 
               GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration) +
               GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByGroupingSets()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY GROUPING SETS(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByGroupingSets_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration) +
               GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY GROUPING SETS(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Having()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(100 < Sum(db.tbl_remuneration.money)));

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
        public void Test_Having_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id) +
                Having(100 < Sum(db.tbl_remuneration.money)));

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
        public void Test_OrderBy()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                OrderBy(Asc(db.tbl_remuneration.money), Desc(db.tbl_remuneration.staff_id)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.money ASC,
	tbl_remuneration.staff_id DESC");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration) +
                OrderBy(Asc(db.tbl_remuneration.money), Desc(db.tbl_remuneration.staff_id)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.money ASC,
	tbl_remuneration.staff_id DESC");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Where_Vanish()
        {
            var empty = new Sql<bool>();

            var sql = Db<DB>.Sql(db =>
               Select(Asterisk(db.tbl_remuneration)).
               From(db.tbl_remuneration).
               Where(empty));

            var datas = _connection.Query(sql).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Having_Vanish()
        {
            var empty = new Sql<bool>();

            var sql = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(empty));

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
