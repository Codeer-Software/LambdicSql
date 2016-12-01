using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Utils;

namespace TestCheck35
{
    [TestClass]
    public class TestSetOperation
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
        public void Test_Union()
        {
            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Union(query);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All()
        {
            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Union(true, query);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION ALL
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_False()
        {
            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Union(false, query);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Intersect(query);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
INTERSECT
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Except(queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Except(true, queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT ALL
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All_False()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Except(false, queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
EXCEPT
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            var queryAfter = Sql<DB>.Create(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Minus(queryAfter);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
MINUS
SELECT *
FROM tbl_staff
WHERE (tbl_staff.id) = (@p_0)",
1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_Exp()
        {
            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            query = query.Union(query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
UNION
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_Exp()
        {
            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            query = query.Union(true, query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
UNION ALL
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Union_All_False_Exp()
        {
            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            query = query.Union(false, query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
UNION
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Intersect_Exp()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            query = query.Intersect(query);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
INTERSECT
SELECT * FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_Exp()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            var queryAfter = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)"));
            query = query.Except(queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
EXCEPT
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All_Exp()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            var queryAfter = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)"));
            query = query.Except(true, queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
EXCEPT ALL
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Except_All_False_Exp()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            var queryAfter = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)"));
            query = query.Except(false, queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
EXCEPT
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Minus_Exp()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff"));
            var queryAfter = Sql<DB>.Create(db => Text("SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)"));
            query = query.Minus(queryAfter);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT * FROM tbl_staff
MINUS
SELECT * FROM tbl_staff WHERE (tbl_staff.id) = (1)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat()
        {
            var select = Sql<DB>.Create(db => Select(new { name = db.tbl_staff.name}));
            var from = Sql<DB>.Create(db => From(db.tbl_staff));
            var query = select.Concat(from);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS name
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat_Exp()
        {
            var select = Sql<DB>.Create(db => Text("SELECT tbl_staff.name AS name"));
            var from = Sql<DB>.Create(db => Text("FROM tbl_staff"));
            var query = select.Concat(from);

            var datas = _connection.Query<Staff>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT tbl_staff.name AS name
FROM tbl_staff");
        }
    }
}
