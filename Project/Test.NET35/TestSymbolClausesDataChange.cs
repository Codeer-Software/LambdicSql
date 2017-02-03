using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestSymbolClausesDataChange
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

            var sql = Db<DB>.Sql(db =>
                Update(db.tbl_data).
                Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1));

            Assert.AreEqual(1, _connection.Execute(sql));
            AssertEx.AreEqual(sql, _connection,
@"UPDATE tbl_data
SET
	val1 = @p_0,
	val2 = @p_1
WHERE (tbl_data.id) = (@p_2)",
100, "200", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Update_Set_Start()
        {
            Test_InsertInto_Values();

            var sql = Db<DB>.Sql(db =>
                Update(db.tbl_data) +
                Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1));

            Assert.AreEqual(1, _connection.Execute(sql));
            AssertEx.AreEqual(sql, _connection,
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
            var sql = Db<DB>.Sql(db =>
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"DELETE
FROM tbl_data
WHERE (tbl_data.id) = (@p_0)",
3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Delete_All()
        {
            var sql = Db<DB>.Sql(db =>
                Delete().
                From(db.tbl_data));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"DELETE
FROM tbl_data");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values()
        {
            Test_Delete_All();

            var sql = Db<DB>.Sql(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).
                   Values(1, "val2"));

            Assert.AreEqual(1, _connection.Execute(sql));
            AssertEx.AreEqual(sql, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES(@p_0, @p_1)",
1, "val2");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values_Start()
        {
            Test_Delete_All();

            var sql = Db<DB>.Sql(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2) +
                   Values(1, "val2"));

            Assert.AreEqual(1, _connection.Execute(sql));
            AssertEx.AreEqual(sql, _connection,
@"INSERT INTO tbl_data(id, val2)
	VALUES(@p_0, @p_1)",
1, "val2");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_InsertInto_Values_Select()
        {
            Test_InsertInto_Values();
            var sql = Db<DB>.Sql(db =>
                   InsertInto(db.tbl_data).
                   Select(new
                   {
                       id = db.tbl_data.id + 10,
                       val1 = db.tbl_data.val1,
                       val2 = db.tbl_data.val2
                   }).
                   From(db.tbl_data));

            Assert.AreEqual(1, _connection.Execute(sql));
            AssertEx.AreEqual(sql, _connection,
@"INSERT INTO tbl_data
SELECT
	(tbl_data.id) + (@p_0) AS id,
	tbl_data.val1 AS val1,
	tbl_data.val2 AS val2
FROM tbl_data", 10);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable()
        {
            CleanUpCreateDropTestTable();
            
            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int()),
                     new Column(db.table1.val1, DataType.Int()),
                     new Column(db.table1.val2, DataType.Char(10))
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT,
	val1 INT,
	val2 CHAR(10))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTableIfNotExists()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.MySQL, TargetDB.SQLite)) return;
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTableIfNotExists(db.table1,
                     new Column(db.table1.id, DataType.Int()),
                     new Column(db.table1.val1, DataType.Int()),
                     new Column(db.table1.val2, DataType.Char(10))
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE IF NOT EXISTS table1(
	id INT,
	val1 INT,
	val2 CHAR(10))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Constraint()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int()),
                     Constraint("xxx").Check(db.table1.id < 100)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT,
	CONSTRAINT xxx
		CHECK((id) < (100)))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_PrimaryKey_1()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull(), PrimaryKey())
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL PRIMARY KEY)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_PrimaryKey_2()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull()),
                     PrimaryKey(db.table1.id)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL,
	PRIMARY KEY(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_PrimaryKey_3()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull()),
                     Constraint("xxx").PrimaryKey(db.table1.id)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL,
	CONSTRAINT xxx
		PRIMARY KEY(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Check_1()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), Check(db.table1.id < 100))
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT CHECK((id) < (100)))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Check_2()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int()),
                     Check(db.table1.id < 100)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT,
	CHECK((id) < (100)))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Check_3()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int()),
                     Constraint("xxx").Check(db.table1.id < 100)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT,
	CONSTRAINT xxx
		CHECK((id) < (100)))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Unique_1()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull(), Unique())
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL UNIQUE)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Unique_2()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull()),
                     Constraint("xxx").Unique(db.table1.id)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL,
	CONSTRAINT xxx
		UNIQUE(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Unique_3()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull()),
                     Unique(db.table1.id)
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL,
	UNIQUE(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ForeignKey_References_1()
        {
            CleanUpCreateDropTestTable();

            var tbl1 = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull(), PrimaryKey())
                 ));
            _connection.Execute(tbl1);

            var sql = Db<DBForCreateTest>.Sql(db => CreateTable(db.table2,
                new Column(db.table2.table1Id, DataType.Int()),
                ForeignKey(db.table2.table1Id).References(db.table1, db.table1.id)
                ));
            
            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table2(
	table1Id INT,
	FOREIGN KEY(table1Id)
		REFERENCES table1(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ForeignKey_References_2()
        {
            CleanUpCreateDropTestTable();

            var tbl1 = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull(), PrimaryKey())
                 ));
            _connection.Execute(tbl1);

            var sql = Db<DBForCreateTest>.Sql(db => CreateTable(db.table2,
                new Column(db.table2.table1Id, DataType.Int()),
                Constraint("xxx").ForeignKey(db.table2.table1Id).References(db.table1, db.table1.id)
                ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table2(
	table1Id INT,
	CONSTRAINT xxx
		FOREIGN KEY(table1Id)
		REFERENCES table1(id))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NotNull()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), NotNull())
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT NOT NULL)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Default()
        {
            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int(), Default(3))
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT DEFAULT 3)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_DropTable()
        {
            CleanUpCreateDropTestTable();

            var create = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table1,
                     new Column(db.table1.id, DataType.Int())
                 ));
            _connection.Execute(create);

            var sql = Db<DBForCreateTest>.Sql(db => DropTable(db.table1));
            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"DROP TABLE table1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_DropTableIfExists()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.MySQL, TargetDB.SQLite)) return;

            var sql = Db<DBForCreateTest>.Sql(db => DropTableIfExists(db.table1));
            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"DROP TABLE IF EXISTS table1");
        }

        void CleanUpCreateDropTestTable()
        {
            try
            {
                var sql = Db<DBForCreateTest>.Sql(db => DropTable(db.table2));
                _connection.Execute(sql);
            }
            catch { }
            try
            {
                var sql = Db<DBForCreateTest>.Sql(db => DropTable(db.table1));
                _connection.Execute(sql);
            }
            catch { }
        }
    }
}
