using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

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
        public void Test_Where1()
        {
            var query = Db<DB>.Sql(db =>
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
        public void Test_Where2()
        {
            var exp = Db<DB>.Sql(db => db.tbl_remuneration.id == 1);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               Where(exp));

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
        public void Test_GroupBy1()
        {
            var query = Db<DB>.Sql(db =>
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
        public void Test_GroupBy2()
        {
            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupBy(exp1, exp2));

            var datas = _connection.Query(query).ToList();

            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByRollup1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
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
        public void Test_GroupByRollup2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByRollup(exp1, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY ROLLUP(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup1()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup2()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByWithRollup(exp1, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY tbl_remuneration.id, tbl_remuneration.staff_id WITH ROLLUP");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByCube(exp1, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY CUBE(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByGroupingSets1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
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
        public void Test_GroupByGroupingSets2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
               GroupByGroupingSets(exp1, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Count
FROM tbl_remuneration
GROUP BY GROUPING SETS(tbl_remuneration.id, tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Having1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
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
        public void Test_Having2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var exp = Db<DB>.Sql(db => 100 < Sum(db.tbl_remuneration.money));
            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id).
                Having(exp));

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
        public void Test_OrderBy1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                OrderBy(Asc(db.tbl_remuneration.money), Desc(db.tbl_remuneration.staff_id)));

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
        public void Test_OrderBy2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.money);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var exp3 = Db<DB>.Sql(db => Asc(exp1));
            var exp4 = Db<DB>.Sql(db => Desc(exp2));
            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                OrderBy(exp3.Body, exp4.Body));

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
        public void Test_Continue_Where1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => Where(db.tbl_remuneration.id == 1));
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
        public void Test_Continue_Where2()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var exp = Db<DB>.Sql(db => db.tbl_remuneration.id == 1);
            var target = Db<DB>.Sql(db => Where(exp));
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
        public void Test_Continue_GroupBy1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupBy(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
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
        public void Test_Continue_GroupBy2()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var target = Db<DB>.Sql(db => GroupBy(exp1, exp2));
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
        public void Test_Continue_GroupByRollup1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
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
        public void Test_Continue_GroupByRollup2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByRollup(exp1, exp2));
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
        public void Test_Continue_GroupByWithRollup1()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
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
        public void Test_Continue_GroupByWithRollup2()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByWithRollup(exp1, exp2));
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
        public void Test_Continue_GroupByCube1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
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
        public void Test_Continue_GroupByCube2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByCube(exp1, exp2));
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
        public void Test_Continue_GroupByGroupingSets1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id));
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
        public void Test_Continue_GroupByGroupingSets2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.id);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData1
               {
                   Count = Count(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => GroupByGroupingSets(exp1, exp2));
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
        public void Test_Continue_Having1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id));

            var target = Db<DB>.Sql(db => Having(100 < Sum(db.tbl_remuneration.money)));
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
        public void Test_Continue_Having2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var exp = Db<DB>.Sql(db => 100 < Sum(db.tbl_remuneration.money));
            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration).
                GroupBy(db.tbl_remuneration.staff_id));

            var target = Db<DB>.Sql(db => Having(exp));
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
        public void Test_Continue_OrderBy1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => OrderBy(Asc(db.tbl_remuneration.money), Desc(db.tbl_remuneration.staff_id)));
            query = query.Concat(target);


            query.Gen(_connection);

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
        public void Test_Continue_OrderBy2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var exp1 = Db<DB>.Sql(db => db.tbl_remuneration.money);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id);
            var exp3 = Db<DB>.Sql(db => Asc(exp1));
            var exp4 = Db<DB>.Sql(db => Desc(exp2));
            var query = Db<DB>.Sql(db =>
                Select(new SelectedData2
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => OrderBy(exp3.Body, exp4.Body));
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
