using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCheck35;
using TestCore;
using static Test.Helper.DBProviderInfo;




using System.Diagnostics;
using System.Linq;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using System.Collections.Generic;
using LambdicSql.SqlBase;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static TestCheck35.TestSynatax;

namespace Test
{
    [TestClass]
    public class TestKeywordWhereGroupByHavingOrderByWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        TestKeywordWhereGroupByHavingOrderBy _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new TestKeywordWhereGroupByHavingOrderBy();
            _core.TestInitialize(_connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Where() => _core.Test_Where();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupBy() => _core.Test_GroupBy();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByRollup() => _core.Test_GroupByRollup();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByWithRollup() => _core.Test_GroupByWithRollup();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByCube() => _core.Test_GroupByCube();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_GroupByGroupingSets() => _core.Test_GroupByGroupingSets();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Having() => _core.Test_Having();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OrderBy() => _core.Test_OrderBy();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Where() => _core.Test_Continue_Where();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupBy() => _core.Test_Continue_GroupBy();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByRollup() => _core.Test_Continue_GroupByRollup();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByWithRollup() => _core.Test_Continue_GroupByWithRollup();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByCube() => _core.Test_Continue_GroupByCube();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_GroupByGroupingSets() => _core.Test_Continue_GroupByGroupingSets();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Having() => _core.Test_Continue_Having();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_OrderBy() => _core.Test_Continue_OrderBy();
    }
}
