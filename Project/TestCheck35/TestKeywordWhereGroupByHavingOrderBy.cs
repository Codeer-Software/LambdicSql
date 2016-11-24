using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;

namespace TestCheck35
{
    [TestClass]
    public class TestKeywordWhereGroupByHavingOrderBy
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
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               Where(db.tbl_remuneration.id == 1));

            var datas = _connection.Query(query).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
WHERE (tbl_remuneration.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupBy()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupBy(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(query).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByRollup()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY ROLLUP(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByGroupingSets()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY GROUPING SETS(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Having()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(100 < Sum(db.tbl_remuneration.money)));

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
        public void Test_OrderBy()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.staff_id)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.money ASC,
	tbl_remuneration.staff_id DESC");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Where()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => Where(db.tbl_remuneration.id == 1));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
WHERE (tbl_remuneration.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupBy()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => GroupBy(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByRollup()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY ROLLUP(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByWithRollup()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByCube()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByGroupingSets()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY GROUPING SETS(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Having()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id));

            var target = Sql<DB>.Create(db => Having(100 < Sum(db.tbl_remuneration.money)));
            query = query.Concat(target);

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
        public void Test_Continue_OrderBy()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.staff_id)));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.staff_id AS Id
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.money ASC,
	tbl_remuneration.staff_id DESC");
        }
    }
}
