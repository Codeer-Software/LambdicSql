using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbols;

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

            var query = Db<DB>.Sql(db =>
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

            var exp1 = Db<DB>.Sql(db => db.tbl_data);
            var exp2 = Db<DB>.Sql(db => new Assign(db.tbl_data.val1, 100));
            var exp3 = Db<DB>.Sql(db => new Assign(db.tbl_data.val2, "200"));
            var query = Db<DB>.Sql(db =>
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
            var query = Db<DB>.Sql(db =>
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
            var query = Db<DB>.Sql(db =>
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
            
            var query = Db<DB>.Sql(db =>
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

            var exp1 = Db<DB>.Sql(db => db.tbl_data);
            var exp2 = Db<DB>.Sql(db => db.tbl_data.id);
            var exp3 = Db<DB>.Sql(db => db.tbl_data.val2);
            var exp4 = Db<DB>.Sql(db =>1);
            var exp5 = Db<DB>.Sql(db => "val2");
            var query = Db<DB>.Sql(db =>
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
            var query = Db<DB>.Sql(db =>
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



        public class Table1
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }
        public class Table2
        {
            public int id { get; set; }
            public int table1Id { get; set; }
            public int val1 { get; set; }
        }

        public class DBForCreateTest
        {
            public Table1 table1 { get; set; }
            public Table2 table2 { get; set; }
        }


        //やっぱり、IS NULL と IS NOT NULL は必要かな
        //  IF OBJECT_ID('dbo.Scores', 'U') IS NOT NULL DROP TABLE dbo.Scores;

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Drop()
        {
            try
            {
                var sql2 = Db<DBForCreateTest>.Sql(db => DropTable(db.table2));
                _connection.Execute(sql2);
            }
            catch { }
            try
            {
                var sql1 = Db<DBForCreateTest>.Sql(db => DropTable(db.table1));
                _connection.Execute(sql1);
            }
            catch { }

            {
                var sql = Db<DBForCreateTest>.Sql(db => 
                    CreateTable(db.table1,
                        new Column(db.table1.id, DataTypes.Int(), Default(10), NotNull(), PrimaryKey()),
                        new Column(db.table1.val2, DataTypes.VarChar(10), NotNull()),
                        Constraint("xxx").Check(db.table1.id < 100),
                        Unique(db.table1.val2)
                    ));
                sql.Gen(_connection);
                _connection.Execute(sql);
            }
            {
                var sql = Db<DBForCreateTest>.Sql(db => CreateTable(db.table2,
                    new Column(db.table2.id, DataTypes.Int(), NotNull()),
                    new Column(db.table2.table1Id, DataTypes.Int()),
                    ForeignKey(db.table2.table1Id).References(db.table1, db.table1.id),
                    PrimaryKey(db.table2.id)
                    ));
                sql.Gen(_connection);
                _connection.Execute(sql);
            }

            {
                var sql2 = Db<DBForCreateTest>.Sql(db => DropTable(db.table2));
                _connection.Execute(sql2);
                var sql1 = Db<DBForCreateTest>.Sql(db => DropTable(db.table1));
                _connection.Execute(sql1);
            }

            //lite, oracle, db2はダメ まとめて消す
            {
      //          var sql2 = Db<DBForCreateTest>.Sql(db => DropTable(db.table1, db.table2));
         //       _connection.Execute(sql2);
            }

            //.Cascade().Constraint() はオラクルしか使えない
        }



    }
}
