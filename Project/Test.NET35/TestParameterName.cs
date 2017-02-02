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
    public class TestParameterName
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
        
        class Expressions
        {
            public int val1 = 1;
            public Sql<bool> Condition1 => Db<DB>.Sql(DB => val1 == 1);
            public Sql<bool> Condition2 => Db<DB>.Sql(DB => val1 == 2);
            public Sql<bool> GetCondition3()
            {
                int val1 = 0;
                return Db<DB>.Sql(DB => val1 == 3);
            }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test1()
        {
            var exps = new Expressions();

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(exps.Condition1 || exps.Condition2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE ((@val1) = (@p_0)) OR ((@val1) = (@p_1))", 
new Params()
{
    { "@val1", 1 },
    { "@p_0", 1 },
    { "@p_1", 2 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test2()
        {
            var exps = new Expressions();

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(exps.Condition1 || exps.GetCondition3()));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE ((@val1) = (@p_0)) OR ((@val1_) = (@p_1))",
new Params()
{
    { "@val1", 1 },
    { "@val1_", 0 },
    { "@p_0", 1 },
    { "@p_1", 3 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test3()
        {
            var exps = new Expressions();

            int val1 = 0;
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(exps.Condition1 || val1 == 3));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE ((@val1) = (@p_0)) OR ((@val1_) = (@p_1))",
new Params()
{
    { "@val1", 1 },
    { "@val1_", 0 },
    { "@p_0", 1 },
    { "@p_1", 3 },
});
        }
    }
}
