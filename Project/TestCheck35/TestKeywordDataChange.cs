using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace TestCheck35
{
    public class TestKeywordDataChange
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public void Test_Update_Set()
        {
            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1));
            
            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"UPDATE tbl_data
SET
	val1 = @p_0,
	val2 = @p_1
WHERE (tbl_data.id) = (@p_2)");
        }

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
WHERE (tbl_data.id) = (@p_0)");
        }

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

        public void Test_InsertInto_Values()
        {
            Test_Delete_All();
            
            var query = Sql<DB>.Create(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "val2"));

            Assert.AreEqual(1, _connection.Execute(query));
            AssertEx.AreEqual(query, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES (@p_0, @p_1)");
        }
    }
}
