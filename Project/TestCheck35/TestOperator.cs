using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using System.Collections.Generic;

namespace TestCheck35
{
    [TestClass]
    public class TestOperator
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext);
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Calc()
        {
            int val1 =0, val2 =0, val3 = 0, val4 = 0, val5 = 0;
            var query = Sql<DB>.Create(db => val1 + val2 - val3 / val4 * val5);
           
            AssertEx.AreEqual(query, _connection,
            @"((@val1) + (@val2)) - (((@val3) / (@val4)) * (@val5))", Zero(5));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_String1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;
            if (name == "NpgsqlConnection") return;

            string val1 = "", val2 = "";
            var query = Sql<DB>.Create(db => val1 + val2);
            AssertEx.AreEqual(query, _connection,
            @"(@val1) + (@val2)", EmptyString(2));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_String2()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "MySqlConnection") return;

            string val1 = "", val2 = "";
            var query = Sql<DB>.Create(db => val1 + val2);
            AssertEx.AreEqual(query, _connection,
            @"(@val1) || (@val2)", EmptyString(2));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition1()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0;
            var query = Sql<DB>.Create(db =>
                val1 == val2 &&
                val3 != val4
                );
            AssertEx.AreEqual(query, _connection,
            @"((@val1) = (@val2)) AND ((@val3) <> (@val4))", Zero(4));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition2()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0, val8 = 0;
            var query = Sql<DB>.Create(db =>
                val1 < val2 &&
                val3 <= val4 &&
                val5 > val6 &&
                val7 >= val8
                );
            AssertEx.AreEqual(query, _connection,
            @"((((@val1) < (@val2)) AND ((@val3) <= (@val4))) AND ((@val5) > (@val6))) AND ((@val7) >= (@val8))", Zero(8));
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Operator_Condition3()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0;
            var query = Sql<DB>.Create(db =>
                val1 == val2 &&
                val3 == val4 ||
                val5 == val6);
            AssertEx.AreEqual(query, _connection,
            @"(((@val1) = (@val2)) AND ((@val3) = (@val4))) OR ((@val5) = (@val6))", Zero(6));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Brackets()
        {
            int val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0, val8 = 0, val9 = 0, val10= 0;
            var query = Sql<DB>.Create(db =>
               ((val1 == val2 && val3 == val4) || (val5 == val6 && val7 == val8)) && (val9 == val10));
            AssertEx.AreEqual(query, _connection,
            @"((((@val1) = (@val2)) AND ((@val3) = (@val4))) OR (((@val5) = (@val6)) AND ((@val7) = (@val8)))) AND ((@val9) = (@val10))", Zero(10));
        }

        static Dictionary<string, object> Zero(int count) => Enumerable.Range(0, count).ToDictionary(e => "@val" + (e + 1), e => (object)0);
        static Dictionary<string, object> EmptyString(int count) => Enumerable.Range(0, count).ToDictionary(e => "@val" + (e + 1), e => (object)string.Empty);
    }
}
