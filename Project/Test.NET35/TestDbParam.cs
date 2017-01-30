using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;

namespace Test
{
    [TestClass]
    public class TestDbParam
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
            public string Name { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Param()
        {
            var text = new DbParam<string>() { Value = "xxx" };
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name + text,
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(tbl_staff.name) " + _connection.GetStringAddExp() + @" (@text) AS Name
FROM tbl_staff",
 new DbParams() { { "@text", new DbParam() { Value = "xxx" } } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Param_Direct()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name
                        + new DbParam<string>() { Value = "xxx", DbType = DbType.AnsiStringFixedLength, Size = 10 },
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	(tbl_staff.name) " + _connection.GetStringAddExp() + @" (@p_0) AS Name
FROM tbl_staff",
 new DbParams() { { "@p_0", new DbParam() { Value = "xxx", DbType = DbType.AnsiStringFixedLength, Size = 10 } } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Param_Setting()
        {
            var param = new DbParam<string>()
            {        
                Value = "xxx",
                DbType = DbType.AnsiString,
                Direction = ParameterDirection.InputOutput,
                SourceColumn = "aaa",
                SourceVersion = DataRowVersion.Original,
                Precision = 10,
                Scale = 20,
                Size = 30,
            };

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name + param,
                }).
                From(db.tbl_staff));

            //check parameter only.
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(tbl_staff.name) " + _connection.GetStringAddExp() + @" (@param) AS Name
FROM tbl_staff",
 new DbParams() { { "@param", new DbParam()
 {
    Value = "xxx",
    DbType = DbType.AnsiString,
    Direction = ParameterDirection.InputOutput,
    SourceColumn = "aaa",
    SourceVersion = DataRowVersion.Original,
    Precision = 10,
    Scale = 20,
    Size = 30,
 } } });
        }
    }
}
