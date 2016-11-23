using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordConditionWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordCondition _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordCondition();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Like() => _core.Test_Like();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Between() => _core.Test_Between();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In1() => _core.Test_In1();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In2() => _core.Test_In2();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_In3() => _core.Test_In3();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Exists1() => _core.Test_Exists1();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Exists2() => _core.Test_Exists2();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNull() => _core.Test_IsNull();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsNotNull() => _core.Test_IsNotNull();
    }
}
