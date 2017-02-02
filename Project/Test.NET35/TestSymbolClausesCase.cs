using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class TestSymbolClausesCase
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
            public string Type { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_Else_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));

            DapperAdapter.Log = e => Debug.Print(e);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0)
		THEN @p_1
		WHEN (tbl_staff.id) = (@p_2)
		THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff",
 3, "x", 4, "y", "z");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_Else_2()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case(db.tbl_staff.id).
                                When(3).Then("x").
                                When(4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CASE tbl_staff.id
		WHEN @p_0
		THEN @p_1
		WHEN @p_2
		THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff",
3, "x", 4, "y", "z");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0)
		THEN @p_1
		WHEN (tbl_staff.id) = (@p_2)
		THEN @p_3
	END AS Type
FROM tbl_staff",
3, "x", 4, "y");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_2()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case(db.tbl_staff.id).
                                When(3).Then("x").
                                When(4).Then("y").
                            End()
                }).
                From(db.tbl_staff));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CASE tbl_staff.id
		WHEN @p_0
		THEN @p_1
		WHEN @p_2
		THEN @p_3
	END AS Type
FROM tbl_staff",
3, "x", 4, "y");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_Else_1_Separate()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case<string>() + 
                                When(db.tbl_staff.id == 3) + Then("x") +
                                When(db.tbl_staff.id == 4) + Then("y") +
                                Else("z") +
                            End()
                }).
                From(db.tbl_staff));

            DapperAdapter.Log = e => Debug.Print(e);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0)
		THEN @p_1
		WHEN (tbl_staff.id) = (@p_2)
		THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff",
 3, "x", 4, "y", "z");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case_When_Then_Else_2_Separate()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Type = Case<string>(db.tbl_staff.id) + 
                                When(3) + Then("x") + 
                                When(4) + Then("y") + 
                                Else("z") +
                            End()
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CASE tbl_staff.id
		WHEN @p_0
		THEN @p_1
		WHEN @p_2
		THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff",
3, "x", 4, "y", "z");
        }
    }
}
