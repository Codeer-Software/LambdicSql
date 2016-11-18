﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Union() => _core.Test_Union();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Union_All() => _core.Test_Union_All();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Union_All_False() => _core.Test_Union_All_False();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Intersect() => _core.Test_Intersect();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Except() => _core.Test_Except();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Except_All() => _core.Test_Except_All();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Test_Minus() => _core.Test_Minus();
    }
}
