using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;

namespace TestCheck35
{
    [TestClass]
    public class TestFuncs
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

        public class SelectData1
        {
            public decimal Val1 { get; set; }
            public decimal Val2 { get; set; }
            public decimal Val3 { get; set; }
            public decimal Val4 { get; set; }
            public decimal Val5 { get; set; }
            public decimal Val6 { get; set; }
            public decimal Val7 { get; set; }
            public decimal Val8 { get; set; }
            public decimal Val9 { get; set; }
            public decimal Val10 { get; set; }
        }

        public class SelectData2
        {
            public decimal Val1 { get; set; }
            public decimal Val2 { get; set; }
        }

        public class SelectData3
        {
            public decimal Val { get; set; }
        }

        public class SelectData4
        {
            public int Val { get; set; }
        }

        public class SelectData5
        {
            public string Val { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Aggregate1()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Val1 = Sum(db.tbl_remuneration.money),
                   Val2 = Sum(AggregatePredicate.All, db.tbl_remuneration.money),
                   Val3 = Sum(AggregatePredicate.Distinct, db.tbl_remuneration.money),
                   Val4 = Count(db.tbl_remuneration.money),
                   Val5 = Count(new Asterisk()),
                   Val6 = Count(AggregatePredicate.All, db.tbl_remuneration.money),
                   Val7 = Count(AggregatePredicate.Distinct, db.tbl_remuneration.money),
                   Val8 = (decimal)Avg(db.tbl_remuneration.money),
                   Val9 = Min(db.tbl_remuneration.money),
                   Val10 = Max(db.tbl_remuneration.money),
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money) AS Val1,
	SUM(ALL tbl_remuneration.money) AS Val2,
	SUM(DISTINCT tbl_remuneration.money) AS Val3,
	COUNT(tbl_remuneration.money) AS Val4,
	COUNT(*) AS Val5,
	COUNT(ALL tbl_remuneration.money) AS Val6,
	COUNT(DISTINCT tbl_remuneration.money) AS Val7,
	AVG(tbl_remuneration.money) AS Val8,
	MIN(tbl_remuneration.money) AS Val9,
	MAX(tbl_remuneration.money) AS Val10
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Aggregate2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Val1 = Sum(1),
                   Val2 = Sum(AggregatePredicate.All, 2),
                   Val3 = Sum(AggregatePredicate.Distinct, 3),
                   Val4 = Count(4),
                   Val5 = Count(new Asterisk()),
                   Val6 = Count(AggregatePredicate.All, 5),
                   Val7 = Count(AggregatePredicate.Distinct, 6),
                   Val8 = (decimal)Avg(7),
                   Val9 = Min(8),
                   Val10 = Max(9),
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(@p_0) AS Val1,
	SUM(ALL @p_1) AS Val2,
	SUM(DISTINCT @p_2) AS Val3,
	COUNT(@p_3) AS Val4,
	COUNT(*) AS Val5,
	COUNT(ALL @p_4) AS Val6,
	COUNT(DISTINCT @p_5) AS Val7,
	AVG(@p_6) AS Val8,
	MIN(@p_7) AS Val9,
	MAX(@p_8) AS Val10
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name", 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Aggregate3()
        {
            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => AggregatePredicate.All);
            var exp3 = Sql<DB>.Create(db => AggregatePredicate.Distinct);
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Val1 = Sum(exp1),
                   Val2 = Sum(exp2, exp1),
                   Val3 = Sum(exp3, exp1),
                   Val4 = Count(exp1),
                   Val5 = Count(new Asterisk()),
                   Val6 = Count(exp2, exp1),
                   Val7 = Count(exp3, exp1),
                   Val8 = (decimal)Avg(exp1),
                   Val9 = Min(exp1),
                   Val10 = Max(exp1),
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUM(tbl_remuneration.money) AS Val1,
	SUM(ALL tbl_remuneration.money) AS Val2,
	SUM(DISTINCT tbl_remuneration.money) AS Val3,
	COUNT(tbl_remuneration.money) AS Val4,
	COUNT(*) AS Val5,
	COUNT(ALL tbl_remuneration.money) AS Val6,
	COUNT(DISTINCT tbl_remuneration.money) AS Val7,
	AVG(tbl_remuneration.money) AS Val8,
	MIN(tbl_remuneration.money) AS Val9,
	MAX(tbl_remuneration.money) AS Val10
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_AggregatePredicate_Asterisk()
        {
            if (_connection.GetType().Name != "MySqlConnection" &&
                _connection.GetType().Name != "OracleConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var query = Sql<DB>.Create(db =>
               Select(new SelectData1
               {
                   Val1 = Count(AggregatePredicate.All, new Asterisk())
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(ALL *) AS Val1
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Abs_Round1()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Val1 = Abs(db.tbl_remuneration.money),
                   Val2 = Round(db.tbl_remuneration.money, 2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	ABS(tbl_remuneration.money) AS Val1,
	ROUND(tbl_remuneration.money, @p_0) AS Val2
FROM tbl_remuneration",
2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Abs_Round2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Val1 = Abs(1),
                   Val2 = Round(2, db.tbl_remuneration.staff_id)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	ABS(@p_0) AS Val1,
	ROUND(@p_1, tbl_remuneration.staff_id) AS Val2
FROM tbl_remuneration", 1, 2);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Abs_Round3()
        {
            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => 2);
            var query = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Val1 = Abs(exp1),
                   Val2 = Round(exp1, exp2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	ABS(tbl_remuneration.money) AS Val1,
	ROUND(tbl_remuneration.money, @p_0) AS Val2
FROM tbl_remuneration",
2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Mod1()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new SelectData3
               {
                   Val = Mod(db.tbl_remuneration.money, 2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MOD(tbl_remuneration.money, @p_0) AS Val
FROM tbl_remuneration", 
2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Mod2()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;
            
            var query = Sql<DB>.Create(db =>
               Select(new SelectData3
               {
                   Val = Mod(1, db.tbl_remuneration.staff_id)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	MOD(@p_0, tbl_remuneration.staff_id) AS Val
FROM tbl_remuneration", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Mod3()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "SQLiteConnection") return;
            if (name == "MySqlConnection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => 2);
            var query = Sql<DB>.Create(db =>
               Select(new SelectData3
               {
                   Val = Mod(exp1, exp2)
               }).
               From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	MOD(tbl_remuneration.money, @p_0) AS Val
FROM tbl_remuneration",
2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat1()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Concat(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));
            
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CONCAT(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff", "a", "b");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Concat("a", db.tbl_staff.name, "b")
               }).
               From(db.tbl_staff));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CONCAT(@p_0, tbl_staff.name, @p_1) AS Val
FROM tbl_staff", "a", "b");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat3()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;
            if (name == "DB2Connection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_staff.name);
            var exp2 = Sql<DB>.Create(db => "a");
            var exp3 = Sql<DB>.Create(db => "b");
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Concat(exp1, exp2, exp3)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CONCAT(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff", "a", "b");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Length1()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Length(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LENGTH(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Length2()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Length("a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LENGTH(@p_0) AS Val
FROM tbl_staff", "a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Length3()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "DB2Connection") return;

            var exp = Sql<DB>.Create(db => db.tbl_staff.name);
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Length(exp)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LENGTH(tbl_staff.name) AS Val
FROM tbl_staff");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Len1()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Len(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LEN(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Len2()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Len("a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LEN(@p_0) AS Val
FROM tbl_staff", "a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Len3()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var exp = Sql<DB>.Create(db => db.tbl_staff.name);
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Len(exp)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	LEN(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lower1()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Lower(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LOWER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lower2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Lower("a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LOWER(@p_0) AS Val
FROM tbl_staff", "a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lower3()
        {
            var exp = Sql<DB>.Create(db => db.tbl_staff.name);
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Lower(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	LOWER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Upper1()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Upper(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	UPPER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Upper2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Upper("a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	UPPER(@p_0) AS Val
FROM tbl_staff", "a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Upper3()
        {
            var exp = Sql<DB>.Create(db => db.tbl_staff.name);
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Upper(exp)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	UPPER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Replace()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Replace(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	REPLACE(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
"a", "b");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Replace2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Replace("a", db.tbl_staff.name, "b")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	REPLACE(@p_0, tbl_staff.name, @p_1) AS Val
FROM tbl_staff",
"a", "b");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Replace3()
        {
            var exp1 = Sql<DB>.Create(db => db.tbl_staff.name);
            var exp2 = Sql<DB>.Create(db => "a");
            var exp3 = Sql<DB>.Create(db => "b");
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Replace(exp1, exp2, exp3)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	REPLACE(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
"a", "b");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Substring()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Substring(db.tbl_staff.name, 0, 1)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUBSTRING(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
0, 1);
        }
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Substring2()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Substring("a", db.tbl_staff.id, db.tbl_staff.id)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUBSTRING(@p_0, tbl_staff.id, tbl_staff.id) AS Val
FROM tbl_staff", "a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Substring3()
        {
            var name = _connection.GetType().Name;
            if (name == "SQLiteConnection") return;
            if (name == "OracleConnection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_staff.name);
            var exp2 = Sql<DB>.Create(db => 0);
            var exp3 = Sql<DB>.Create(db => 1);
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Substring(exp1, exp2, exp3)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	SUBSTRING(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
0, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate1()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = Current_Date
                }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT_DATE AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentDate2()
        {
            var name = _connection.GetType().Name;
            if (name != "OracleConnection") return;


            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = Current_Date
                }).From(Dual));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT_DATE AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTime()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = Current_Time
                }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT_TIME AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp1()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection" &&
                 name != "NpgsqlConnection" &&
                 name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
            Select(new
            {
                Val = Current_TimeStamp
            }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentTimeStamp2()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = Current_TimeStamp
                }).
                From(Dual));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT_TIMESTAMP AS Val
FROM DUAL");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentSpaceDate()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = CurrentSpaceDate
                }).
                From(Sysibm.Sysdummy1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT DATE AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentSpaceTime()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = CurrentSpaceTime
                }).
                From(Sysibm.Sysdummy1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT TIME AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CurrentSpaceTimeStamp()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = CurrentSpaceTimeStamp
                }).
                From(Sysibm.Sysdummy1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CURRENT TIMESTAMP AS Val
FROM SYSIBM.SYSDUMMY1");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Extract1()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val1 = Extract(DateTimeElement.Year, Current_TimeStamp),
                    Val2 = Extract(DateTimeElement.Month, Current_TimeStamp),
                    Val3 = Extract(DateTimeElement.Day, Current_TimeStamp),
                    Val4 = Extract(DateTimeElement.Hour, Current_TimeStamp),
                    Val5 = Extract(DateTimeElement.Minute, Current_TimeStamp),
                    Val6 = Extract(DateTimeElement.Second, Current_TimeStamp),
                    Val7 = Extract(DateTimeElement.Millisecond, Current_TimeStamp),
                    Val8 = Extract(DateTimeElement.Microsecond, Current_TimeStamp),
                    Val9 = Extract(DateTimeElement.Quarter, Current_TimeStamp),
                    Val10 = Extract(DateTimeElement.Week, Current_TimeStamp)
                }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	EXTRACT(YEAR FROM CURRENT_TIMESTAMP) AS Val1,
	EXTRACT(MONTH FROM CURRENT_TIMESTAMP) AS Val2,
	EXTRACT(DAY FROM CURRENT_TIMESTAMP) AS Val3,
	EXTRACT(HOUR FROM CURRENT_TIMESTAMP) AS Val4,
	EXTRACT(MINUTE FROM CURRENT_TIMESTAMP) AS Val5,
	EXTRACT(SECOND FROM CURRENT_TIMESTAMP) AS Val6,
	EXTRACT(MILLISECOND FROM CURRENT_TIMESTAMP) AS Val7,
	EXTRACT(MICROSECOND FROM CURRENT_TIMESTAMP) AS Val8,
	EXTRACT(QUARTER FROM CURRENT_TIMESTAMP) AS Val9,
	EXTRACT(WEEK FROM CURRENT_TIMESTAMP) AS Val10");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Extract2()
        {
            var name = _connection.GetType().Name;
            if (name != "MySqlConnection") return;
            var query = Sql<DB>.Create(db =>
            Select(new
            {
                Val1 = (long)Extract(DateTimeElement.Year, Current_TimeStamp),
                Val2 = (long)Extract(DateTimeElement.Month, Current_TimeStamp),
                Val3 = (long)Extract(DateTimeElement.Day, Current_TimeStamp),
                Val4 = (long)Extract(DateTimeElement.Hour, Current_TimeStamp),
                Val5 = (long)Extract(DateTimeElement.Minute, Current_TimeStamp),
                Val6 = (long)Extract(DateTimeElement.Second, Current_TimeStamp),
                Val7 = (long)Extract(DateTimeElement.Quarter, Current_TimeStamp),
                Val8 = (long)Extract(DateTimeElement.Week, Current_TimeStamp)
            }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	EXTRACT(YEAR FROM CURRENT_TIMESTAMP) AS Val1,
	EXTRACT(MONTH FROM CURRENT_TIMESTAMP) AS Val2,
	EXTRACT(DAY FROM CURRENT_TIMESTAMP) AS Val3,
	EXTRACT(HOUR FROM CURRENT_TIMESTAMP) AS Val4,
	EXTRACT(MINUTE FROM CURRENT_TIMESTAMP) AS Val5,
	EXTRACT(SECOND FROM CURRENT_TIMESTAMP) AS Val6,
	EXTRACT(QUARTER FROM CURRENT_TIMESTAMP) AS Val7,
	EXTRACT(WEEK FROM CURRENT_TIMESTAMP) AS Val8");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_DatePart()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val1 = DatePart(DateTimeElement.Year, Current_TimeStamp),
                    Val2 = DatePart(DateTimeElement.Quarter, Current_TimeStamp),
                    Val3 = DatePart(DateTimeElement.Month, Current_TimeStamp),
                    Val4 = DatePart(DateTimeElement.Dayofyear, Current_TimeStamp),
                    Val5 = DatePart(DateTimeElement.Day, Current_TimeStamp),
                    Val6 = DatePart(DateTimeElement.Week, Current_TimeStamp),
                    Val7 = DatePart(DateTimeElement.Weekday, Current_TimeStamp),
                    Val8 = DatePart(DateTimeElement.Hour, Current_TimeStamp),
                    Val9 = DatePart(DateTimeElement.Minute, Current_TimeStamp),
                    Val10 = DatePart(DateTimeElement.Second, Current_TimeStamp),
                    Val11 = DatePart(DateTimeElement.Millisecond, Current_TimeStamp),
                    Val12 = DatePart(DateTimeElement.Microsecond, Current_TimeStamp),
                    Val13 = DatePart(DateTimeElement.Nanosecond, Current_TimeStamp),
                    Val14 = DatePart(DateTimeElement.ISO_WEEK, Current_TimeStamp)
                }));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	DATEPART(YEAR, CURRENT_TIMESTAMP) AS Val1,
	DATEPART(QUARTER, CURRENT_TIMESTAMP) AS Val2,
	DATEPART(MONTH, CURRENT_TIMESTAMP) AS Val3,
	DATEPART(DAYOFYEAR, CURRENT_TIMESTAMP) AS Val4,
	DATEPART(DAY, CURRENT_TIMESTAMP) AS Val5,
	DATEPART(WEEK, CURRENT_TIMESTAMP) AS Val6,
	DATEPART(WEEKDAY, CURRENT_TIMESTAMP) AS Val7,
	DATEPART(HOUR, CURRENT_TIMESTAMP) AS Val8,
	DATEPART(MINUTE, CURRENT_TIMESTAMP) AS Val9,
	DATEPART(SECOND, CURRENT_TIMESTAMP) AS Val10,
	DATEPART(MILLISECOND, CURRENT_TIMESTAMP) AS Val11,
	DATEPART(MICROSECOND, CURRENT_TIMESTAMP) AS Val12,
	DATEPART(NANOSECOND, CURRENT_TIMESTAMP) AS Val13,
	DATEPART(ISO_WEEK, CURRENT_TIMESTAMP) AS Val14");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast1()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection" &&
                name != "SqlConnection" &&
                name != "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    id = Cast<int>(db.tbl_remuneration.money, "int")
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CAST(tbl_remuneration.money AS int) AS id
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast2()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection" &&
                name != "SqlConnection" &&
                name != "NpgsqlConnection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_remuneration.money);
            var exp2 = Sql<DB>.Create(db => "int");
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    id = Cast<int>(exp1, exp2)
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CAST(tbl_remuneration.money AS int) AS id
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Coalesce1()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   id = Coalesce(db.tbl_staff.name, "a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COALESCE(tbl_staff.name, @p_0) AS id
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Coalesce2()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   id = Coalesce("a", db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COALESCE(@p_0, tbl_staff.name) AS id
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Coalesce3()
        {
            var exp1 = Sql<DB>.Create(db => db.tbl_staff.name);
            var exp2 = Sql<DB>.Create(db => "a");
            var query = Sql<DB>.Create(db =>
               Select(new SelectData5
               {
                   Val = Coalesce(exp1, exp2)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COALESCE(tbl_staff.name, @p_0) AS Val
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NVL1()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection" &&
                name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new 
               {
                   id = NVL(db.tbl_staff.name, "a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NVL(tbl_staff.name, @p_0) AS id
FROM tbl_staff",
"a");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NVL2()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection" &&
                name != "OracleConnection") return;

            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   id = NVL("a", db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NVL(@p_0, tbl_staff.name) AS id
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NVL3()
        {
            var name = _connection.GetType().Name;
            if (name != "DB2Connection" &&
                name != "OracleConnection") return;

            var exp1 = Sql<DB>.Create(db => db.tbl_staff.name);
            var exp2 = Sql<DB>.Create(db => "a");
            var query = Sql<DB>.Create(db =>
               Select(new SelectData5
               {
                   Val = NVL(exp1, exp2)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	NVL(tbl_staff.name, @p_0) AS Val
FROM tbl_staff",
"a");
        }
    }
}