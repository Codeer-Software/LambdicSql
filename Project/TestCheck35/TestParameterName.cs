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
            public SqlExpression<bool> Condition1 => Sql<DB>.Of(DB => val1 == 1);
            public SqlExpression<bool> Condition2 => Sql<DB>.Of(DB => val1 == 2);
            public SqlExpression<bool> GetCondition3()
            {
                int val1 = 0;
                return Sql<DB>.Of(DB => val1 == 3);
            }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test1()
        {
            var exps = new Expressions();

            var query = Sql<DB>.Of(db =>
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

            var query = Sql<DB>.Of(db =>
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
            var query = Sql<DB>.Of(db =>
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
