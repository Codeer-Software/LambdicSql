﻿using System.Data;
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
    public class TestSymbolClausesCondition
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
        public void Test_Like1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Like(db.tbl_staff.name, "%a%")));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.name LIKE @p_0",
"%a%");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Like2()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Like("Emma", db.tbl_staff.name)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE @p_0 LIKE tbl_staff.name",
"Emma");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Like3()
        {
            var exp1 = Db<DB>.Sql(db => db.tbl_staff.name);
            var exp2 = Db<DB>.Sql(db => "%a%");
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Like(exp1, exp2)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.name LIKE @p_0",
"%a%");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Between1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Between(db.tbl_staff.id, 0, 100)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id BETWEEN @p_0 AND @p_1",
0, 100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Between2()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Between(1, db.tbl_staff.id, db.tbl_staff.id)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE @p_0 BETWEEN tbl_staff.id AND tbl_staff.id",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Between3()
        {
            var exp1 = Db<DB>.Sql(db => db.tbl_staff.id);
            var exp2 = Db<DB>.Sql(db => 0);
            var exp3 = Db<DB>.Sql(db => 100);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Between(exp1, exp2, exp3)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id BETWEEN @p_0 AND @p_1",
0, 100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, 1, 2, 3, 4, 5)));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id IN(@p_0, @p_1, @p_2, @p_3, @p_4)",
1, 2, 3, 4, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In1_1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(1, 1, db.tbl_staff.id, 3, 4, 5)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE @p_0 IN(@p_1, tbl_staff.id, @p_2, @p_3, @p_4)", 1, 1, 3, 4, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In1_2()
        {
            var exp1 = Db<DB>.Sql(db => db.tbl_staff.id);
            var exp2 = Db<DB>.Sql(db => 1);
            var exp3 = Db<DB>.Sql(db => 2);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(exp1.Body, exp2, exp3)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id IN(@p_0, @p_1)",
1, 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In1_3()
        {
            var vals = new int[] { 1, 2 };
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, vals)));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id IN(@p_0, @p_1)",
1, 2);
        }
        //TODO ↑このパターンでvalsがexpressionになるやつを追加

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In2()
        {
            var sub = Db<DB>.Sql(db =>
                Select(db.tbl_remuneration.staff_id).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                Where(1000 < db.tbl_remuneration.money));

            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, sub)));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	tbl_staff.id IN(
		(SELECT
			tbl_remuneration.staff_id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money)))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In2_1()
        {
            var sub = Db<DB>.Sql(db =>
                Select(db.tbl_remuneration.staff_id).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                Where(1000 < db.tbl_remuneration.money));

            var exp = Db<DB>.Sql(db => db.tbl_staff.id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(exp.Body, sub)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	tbl_staff.id IN(
		(SELECT
			tbl_remuneration.staff_id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money)))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In3()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id,
                    Select(db.tbl_remuneration.staff_id).
                    From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                    Where(1000 < db.tbl_remuneration.money)
               )));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	tbl_staff.id IN(
		(SELECT
			tbl_remuneration.staff_id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money)))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In3_1()
        {
            var exp = Db<DB>.Sql(db => db.tbl_staff.id);
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(exp.Body,
                    Select(db.tbl_remuneration.staff_id).
                    From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                    Where(1000 < db.tbl_remuneration.money)
               )));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	tbl_staff.id IN(
		(SELECT
			tbl_remuneration.staff_id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money)))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Exists1()
        {
            var sub = Db<DB>.Sql(db =>
                Select(new { id = db.tbl_remuneration.staff_id }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                Where(1000 < db.tbl_remuneration.money));

            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Exists(sub)));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	EXISTS
		(SELECT
			tbl_remuneration.staff_id AS id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Exists2()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Exists(
                    Select(new { id = db.tbl_remuneration.staff_id }).
                        From(db.tbl_remuneration).
                        Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                        Where(1000 < db.tbl_remuneration.money)
                   )));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	EXISTS
		(SELECT
			tbl_remuneration.staff_id AS id
		FROM tbl_remuneration
			JOIN tbl_staff ON (tbl_staff.id) = (tbl_staff.id)
		WHERE (@p_0) < (tbl_remuneration.money))",
(decimal)1000);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNull1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(!(db.tbl_staff.name == null)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE NOT ((tbl_staff.name) IS NULL)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNull2()
        {
            var name = _connection.GetType().Name;
            if (name == "OracleConnection") return;

            string val = "";
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(!(val == null)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE NOT ((@val) IS NULL)", new Params() { { "@val", ""} });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull1()
        {
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(db.tbl_staff.name != null));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE (tbl_staff.name) IS NOT NULL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull2()
        {
            var name = _connection.GetType().Name;
            if (name == "OracleConnection") return;

            string val = "";
            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(val != null));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE (@val) IS NOT NULL", new Params() { { "@val", "" } });
        }


        //TODO all
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void XXX()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
            Select(new SelectData
            {
                Id = db.tbl_staff.id
            }).
            From(db.tbl_staff).
            Where(db.tbl_staff.id < All<int>(Select(db.tbl_staff.id).From(db.tbl_staff)))
            );
            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
        }
    }
}