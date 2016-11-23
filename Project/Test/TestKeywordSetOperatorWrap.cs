using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestKeywordSetOperatorWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordSetOperator _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordSetOperator();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union() => _core.Test_Union();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All() => _core.Test_Union_All();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_False() => _core.Test_Union_All_False();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect() => _core.Test_Intersect();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except() => _core.Test_Except();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All() => _core.Test_Except_All();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus() => _core.Test_Minus();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union() => _core.Test_Continue_Union();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union_All() => _core.Test_Continue_Union_All();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Union_All_False() => _core.Test_Continue_Union_All_False();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Intersect() => _core.Test_Continue_Intersect();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Except() => _core.Test_Continue_Except();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Except_All() => _core.Test_Continue_Except_All();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Minus() => _core.Test_Continue_Minus();
    }
}
