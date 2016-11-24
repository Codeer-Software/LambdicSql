using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

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
        public void Test_Update_Set()
        {
            Test_InsertInto_Values();

            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
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
            var query = Sql<DB>.Create(db =>
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
            var query = Sql<DB>.Create(db =>
                Delete().
                From(db.tbl_data));

            _connection.Execute(query);
            AssertEx.AreEqual(query, _connection,
@"DELETE
FROM tbl_data");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values()
        {
            Test_Delete_All();
            
            var query = Sql<DB>.Create(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "val2"));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES (@p_0, @p_1)",
1, "val2");
        }
    }
}

//TODO テスト残件
//Inで配列で渡す
//InsertInto + Select
//Insert時にDBParamを型から推論してくるやつ
//これはここに書いた方がいいな

