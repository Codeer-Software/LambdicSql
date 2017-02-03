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
        public void Test_Like()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Like(db.tbl_staff.name, "%a%")));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.name LIKE @p_0",
"%a%");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Between()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Between(db.tbl_staff.id, 0, 100)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id BETWEEN @p_0 AND @p_1",
0, 100);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, 1, 2, 3, 4, 5)));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id IN(@p_0, @p_1, @p_2, @p_3, @p_4)",
1, 2, 3, 4, 5);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In_Array()
        {
            var vals = new int[] { 1, 2 };
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, vals)));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.id IN(@p_0, @p_1)",
1, 2);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In_SubQuery()
        {
            var sub = Db<DB>.Sql(db =>
                Select(db.tbl_remuneration.staff_id).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                Where(1000 < db.tbl_remuneration.money));

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(In(db.tbl_staff.id, sub)));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Exists_1()
        {
            var sub = Db<DB>.Sql(db =>
                Select(new { id = db.tbl_remuneration.staff_id }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_staff.id).
                Where(1000 < db.tbl_remuneration.money));

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(Exists(sub)));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Exists_2()
        {
            var sql = Db<DB>.Sql(db =>
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

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_IsNull_1()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(!IsNull(db.tbl_staff.name)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE NOT (tbl_staff.name IS NULL)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNull_2()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(!(db.tbl_staff.name == null)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE NOT ((tbl_staff.name) IS NULL)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNull_3()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.MySQL, TargetDB.SQLite, TargetDB.DB2)) return;

            string val = "";
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(!(val == null)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE NOT ((@val) IS NULL)", new Params() { { "@val", ""} });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull_1()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(IsNotNull(db.tbl_staff.name)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE tbl_staff.name IS NOT NULL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull_2()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(db.tbl_staff.name != null));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE (tbl_staff.name) IS NOT NULL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull_3()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.MySQL, TargetDB.SQLite, TargetDB.DB2)) return;
            
            string val = "";
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Id = db.tbl_staff.id
               }).
               From(db.tbl_staff).
               Where(val != null));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE (@val) IS NOT NULL", new Params() { { "@val", "" } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_All_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Id = db.tbl_staff.id
                }).
                From(db.tbl_staff).
                Where(db.tbl_staff.id < All(Select(db.tbl_staff.id).From(db.tbl_staff))));

            var datas = _connection.Query(sql).ToList();
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	(tbl_staff.id)
	 <
	ALL(
		(SELECT
			tbl_staff.id
		FROM tbl_staff))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_All_2()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sub = Db<DB>.Sql(db => 
                Select(db.tbl_staff.id).
                From(db.tbl_staff));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Id = db.tbl_staff.id
                }).
                From(db.tbl_staff).
                Where(db.tbl_staff.id < All(sub)));

            var datas = _connection.Query(sql).ToList();
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.id AS Id
FROM tbl_staff
WHERE
	(tbl_staff.id)
	 <
	ALL(
		(SELECT
			tbl_staff.id
		FROM tbl_staff))");
        }
    }
}
