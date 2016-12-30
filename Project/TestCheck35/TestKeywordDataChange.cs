using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

namespace TestCheck35
{
    [TestClass]
    public class TestKeywordDataChange
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
        public void Test_Update_Set1()
        {
            Test_InsertInto_Values1();

            var query = Sql<DB>.Of(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1));

            query.Gen(_connection);

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"UPDATE tbl_data
SET
	val1 = @p_0,
	val2 = @p_1
WHERE (tbl_data.id) = (@p_2)",
100, "200", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Update_Set2()
        {
            Test_InsertInto_Values1();

            var exp1 = Sql<DB>.Of(db => db.tbl_data);
            var exp2 = Sql<DB>.Of(db => new Assign(db.tbl_data.val1, 100));
            var exp3 = Sql<DB>.Of(db => new Assign(db.tbl_data.val2, "200"));
            var query = Sql<DB>.Of(db =>
                Update(exp1).Set(exp2.Body, exp3.Body).
                Where(db.tbl_data.id == 1));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"UPDATE tbl_data
SET
	val1 = @p_0,
	val2 = @p_1
WHERE (tbl_data.id) = (@p_2)",
100, "200", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Delete()
        {
            var query = Sql<DB>.Of(db =>
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3));

            _connection.Execute(query);
            AssertEx.AreEqual(query, _connection,
@"DELETE
FROM tbl_data
WHERE (tbl_data.id) = (@p_0)",
3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Delete_All()
        {
            var query = Sql<DB>.Of(db =>
                Delete().
                From(db.tbl_data));

            _connection.Execute(query);
            AssertEx.AreEqual(query, _connection,
@"DELETE
FROM tbl_data");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values1()
        {
            Test_Delete_All();
            
            var query = Sql<DB>.Of(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "val2"));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES(@p_0, @p_1)",
1, "val2");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values2()
        {
            Test_Delete_All();

            var exp1 = Sql<DB>.Of(db => db.tbl_data);
            var exp2 = Sql<DB>.Of(db => db.tbl_data.id);
            var exp3 = Sql<DB>.Of(db => db.tbl_data.val2);
            var exp4 = Sql<DB>.Of(db =>1);
            var exp5 = Sql<DB>.Of(db => "val2");
            var query = Sql<DB>.Of(db =>
                   InsertInto(exp1, exp2, exp3).Values(exp4, exp5));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES(@p_0, @p_1)",
1, "val2");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values_Select()
        {
            Test_InsertInto_Values1();
            var query = Sql<DB>.Of(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val1, db.tbl_data.val2).
                   Select(new
                   {
                       id = db.tbl_data.id + 10,
                       val1 = db.tbl_data.val1,
                       val2 = db.tbl_data.val2
                   }).
                   From(db.tbl_data));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"INSERT INTO tbl_data(id, val1, val2)
SELECT
	(tbl_data.id) + (@p_0) AS id,
	tbl_data.val1 AS val1,
	tbl_data.val2 AS val2
FROM tbl_data", 10);
        }
    }
}
