using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordLimitWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordLimit _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordLimit();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Limit_Offset() => _core.Test_Limit_Offset();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_OffsetRows_FetchNext() => _core.Test_OffsetRows_FetchNext();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Limit_Offset() => _core.Test_Continue_Limit_Offset();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_OffsetRows_FetchNext() => _core.Test_Continue_OffsetRows_FetchNext();
    }
}
