using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordFromWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordFrom _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordFrom();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From1() => _core.Test_From1();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From2() => _core.Test_From2();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From3() => _core.Test_From3();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join() => _core.Test_Join();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin() => _core.Test_LeftJoin();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RightJoin() => _core.Test_RightJoin();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin() => _core.Test_CrossJoin();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Join() => _core.Test_Continue_Join();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_LeftJoin() => _core.Test_Continue_LeftJoin();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_RightJoin() => _core.Test_Continue_RightJoin();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_CrossJoin() => _core.Test_Continue_CrossJoin();
    }
}
