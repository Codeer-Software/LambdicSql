using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

namespace TestCheck35
{
    [TestClass]
    public class TestKeywordSetOperator
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
            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

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
            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union(All()).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

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
        public void Test_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Intersect().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

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

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

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

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Except(All()).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

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
        public void Test_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Minus().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));

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
        public void Test_Continue_Union()
        {
            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Union().Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

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
        public void Test_Continue_Union_All()
        {
            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Union(All()).Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

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
        public void Test_Continue_Intersect()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Intersect().Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));
            query = query.Concat(target);

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
        public void Test_Continue_Except()
        {
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Except().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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
        public void Test_Continue_Except_All()
        {
            if (_connection.GetType().Name == "SqlConnection") return;
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "OracleConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Except(All()).
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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
        public void Test_Continue_Minus()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;
            if (_connection.GetType().Name == "NpgsqlConnection") return;

            var query = Sql<DB>.Of(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var target = Sql<DB>.Of(db =>
                Minus().
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).Where(db.tbl_staff.id == 1));
            query = query.Concat(target);

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
    }
}
