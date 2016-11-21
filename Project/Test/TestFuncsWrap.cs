using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestFuncsWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestFuncs _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestFuncs();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Aggregate() => _core.Test_Aggregate();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Abs_Round() => _core.Test_Abs_Round();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Mod() => _core.Test_Mod();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Length() => _core.Test_Length();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Len() => _core.Test_Len();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Lower() => _core.Test_Lower();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Upper() => _core.Test_Upper();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Replace() => _core.Test_Replace();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Substring() => _core.Test_Substring();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentDate1() => _core.Test_CurrentDate1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentDate2() => _core.Test_CurrentDate2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentTime() => _core.Test_CurrentTime();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp1() => _core.Test_CurrentTimeStamp1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp2() => _core.Test_CurrentTimeStamp2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentSpaceDate() => _core.Test_CurrentSpaceDate();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentSpaceTime() => _core.Test_CurrentSpaceTime();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_CurrentSpaceTimeStamp() => _core.Test_CurrentSpaceTimeStamp();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Extract1() => _core.Test_Extract1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Extract2() => _core.Test_Extract2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_DatePart() => _core.Test_DatePart();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Cast() => _core.Test_Cast();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Coalesce() => _core.Test_Coalesce();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_NVL() => _core.Test_NVL();
    }
}
