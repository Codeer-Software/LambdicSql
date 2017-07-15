using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestInterpolateSql
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
        public void Test1()
        {
            var name = "Emma";
            var maxId = 100;
            var sql = Db<DB>.InterpolateSql<Staff>(db =>
$@"SELECT *
FROM tbl_staff
WHERE {(db.tbl_staff.name == name && db.tbl_staff.id < maxId)}");

            sql.Gen(new SqlConnection());
            var datas = _connection.Query(sql).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT *
FROM tbl_staff
WHERE ((tbl_staff.name) = (@name)) AND ((tbl_staff.id) < (@maxId))",
 new Params { { "@name", "Emma" }, { "@maxId", 100 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test2()
        {
            var name = "Emma";
            var maxId = 100;
            var sql = Db<DB>.InterpolateSql(db =>
$@"SELECT *
FROM tbl_staff
WHERE {(db.tbl_staff.name == name && db.tbl_staff.id < maxId)}");

            sql.Gen(new SqlConnection());
            var datas = _connection.Query<Staff>(sql).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT *
FROM tbl_staff
WHERE ((tbl_staff.name) = (@name)) AND ((tbl_staff.id) < (@maxId))",
 new Params { { "@name", "Emma" }, { "@maxId", 100 } });
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test3()
        {
            var name = "Emma";
            var maxId = 100;
            var sql = Db.InterpolateSql<Staff>(() =>
$@"SELECT *
FROM tbl_staff
WHERE tbl_staff.name = {name} AND tbl_staff.id < {maxId}");

            sql.Gen(new SqlConnection());
            var datas = _connection.Query(sql).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE tbl_staff.name = @name AND tbl_staff.id < @maxId",
 new Params { { "@name", "Emma" }, { "@maxId", 100 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test4()
        {
            var name = "Emma";
            var maxId = 100;
            var sql = Db.InterpolateSql(() =>
$@"SELECT *
FROM tbl_staff
WHERE tbl_staff.name = {name} AND tbl_staff.id < {maxId}");

            sql.Gen(new SqlConnection());
            var datas = _connection.Query<Staff>(sql).ToList();
            Assert.AreEqual(1, datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE tbl_staff.name = @name AND tbl_staff.id < @maxId",
 new Params { { "@name", "Emma" }, { "@maxId", 100 } });
        }
    }
}
