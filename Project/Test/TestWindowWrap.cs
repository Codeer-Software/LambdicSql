using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestWindowWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestWindow _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestWindow();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Avg() => _core.Test_Avg();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum() => _core.Test_Sum();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count() => _core.Test_Count();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Max() => _core.Test_Max();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Min() => _core.Test_Min();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value() => _core.Test_First_Value();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Last_Value() => _core.Test_Last_Value();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rank() => _core.Test_Rank();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Dense_Rank() => _core.Test_Dense_Rank();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Percent_Rank() => _core.Test_Percent_Rank();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cume_Dist() => _core.Test_Cume_Dist();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Ntile() => _core.Test_Ntile();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value() => _core.TestNth_Value();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag1() => _core.Test_Lag1();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag2() => _core.Test_Lag2();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag3() => _core.Test_Lag3();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows() => _core.Test_Rows();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy() => _core.Test_OrderBy();
    }
}
