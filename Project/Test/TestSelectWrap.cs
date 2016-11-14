﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class TestSelectWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestSelect _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestSelect();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select() => _core.Test_Select();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Top() => _core.Test_Select_Top();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Non() => _core.Test_Select_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Asterisk() => _core.Test_Select_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Helper() => _core.Test_Select_Asterisk_Helper();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Top_Asterisk_Non() => _core.Test_Select_Top_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Top_Asterisk() => _core.Test_Select_Top_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_All() => _core.Test_Select_All();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Distinct() => _core.Test_Select_Distinct();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_All_Asterisk_Non() => _core.Test_Select_All_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Asterisk_Non() => _core.Test_Select_Distinct_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_All_Asterisk() => _core.Test_Select_All_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Asterisk() => _core.Test_Select_Distinct_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_All_Top_Asterisk_Non() => _core.Test_Select_All_Top_Asterisk_Non();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Top_Asterisk_Non() => _core.Test_Select_Distinct_Top_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_All_Top_Asterisk() => _core.Test_Select_All_Top_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Top_Asterisk() => _core.Test_Select_Distinct_Top_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select() => _core.Test_Continue_Select();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top() => _core.Test_Continue_Select_Top();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk_Non() => _core.Test_Continue_Select_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk() => _core.Test_Continue_Select_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk_Helper() => _core.Test_Continue_Select_Asterisk_Helper();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top_Asterisk_Non() => _core.Test_Continue_Select_Top_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top_Asterisk() => _core.Test_Continue_Select_Top_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_All() => _core.Test_Continue_Select_All();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct() => _core.Test_Continue_Select_Distinct();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Asterisk_Non() => _core.Test_Continue_Select_All_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Asterisk_Non() => _core.Test_Continue_Select_Distinct_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Asterisk() => _core.Test_Continue_Select_All_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Asterisk() => _core.Test_Continue_Select_Distinct_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Top_Asterisk_Non() => _core.Test_Continue_Select_All_Top_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Top_Asterisk_Non() => _core.Test_Continue_Select_Distinct_Top_Asterisk_Non();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Top_Asterisk() => _core.Test_Continue_Select_All_Top_Asterisk();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Top_Asterisk() => _core.Test_Continue_Select_Distinct_Top_Asterisk();
    }
}
