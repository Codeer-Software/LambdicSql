using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordCaseWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordCase _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordCase();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Case1() => _core.Test_Case1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Case2() => _core.Test_Case2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Case3() => _core.Test_Case3();
    }
}
