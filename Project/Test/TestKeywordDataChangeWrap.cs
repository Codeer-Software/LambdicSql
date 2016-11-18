using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordDataChangeWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordDataChange _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordDataChange();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Update_Set() => _core.Test_Update_Set();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Delete() => _core.Test_Delete();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Delete_All() => _core.Test_Delete_All();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_InsertInto_Values() => _core.Test_InsertInto_Values();

    }
}
