using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestUtilsWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestUtils _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestUtils();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Cast1() => _core.Test_Cast1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Cast2() => _core.Test_Cast2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void TestCondition1() => _core.Test_Condition1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Condition2() => _core.Test_Condition2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Condition3() => _core.Test_Condition3();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Condition4() => _core.Test_Condition4();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Text() => _core.Test_Text();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Format2WaySql() => _core.Test_Format2WaySql();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_ColumnOnly() => _core.Test_ColumnOnly();
    }
}
//TODO Conditionの←の条件式の書き方。
//これもここに書くか