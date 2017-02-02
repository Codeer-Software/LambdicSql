using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestSymbolEtc
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
        public void Test_CurrentDate_1()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_DATE AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }).From(Dual));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_DATE AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate_3()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Date()
                }).From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT DATE AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTime_1()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Time()
                }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIME AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTime_2()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_Time()
                }).
                From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT TIME AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
            Select(new
            {
                Val = Current_TimeStamp()
            }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_TimeStamp()
                }).
                From(Dual));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp_3()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val = Current_TimeStamp()
                }).
                From(SysIBM.SysDummy1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CURRENT TIMESTAMP AS Val
FROM SYSIBM.SYSDUMMY1");
        }
    }
}
