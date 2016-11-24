using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Aggregate()
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
                   Val8 = Avg(db.tbl_remuneration.money),
                   Val9 = Min(db.tbl_remuneration.money),
                   Val10 = Max(db.tbl_remuneration.money),
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
        public void Test_Abs_Round()
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
        public void Test_Mod()
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
        public void Test_Concat()
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
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Length()
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
        public void Test_Len()
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
        public void Test_Lower()
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
        public void Test_Upper()
        {
            var query = Sql<DB>.Create(db =>
               Select(new
               {
                   Val = Upper(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
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
        public void Test_CurrentDate1()
        {
            var name = _connection.GetType().Name;
            if (name != "NpgsqlConnection" &&
                name != "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    Val = CurrentDate()
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
                    Val = CurrentDate()
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
                    Val = CurrentTime<TimeSpan>()
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
                Val = CurrentTimeStamp()
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
                    Val = CurrentTimeStamp()
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
                    Val = CurrentSpaceDate()
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
                    Val = CurrentSpaceTime()
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
                    Val = CurrentSpaceTimeStamp()
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
                    Val1 = Extract<double>(DateTiemElement.Year, CurrentTimeStamp()),
                    Val2 = Extract<double>(DateTiemElement.Month, CurrentTimeStamp()),
                    Val3 = Extract<double>(DateTiemElement.Day, CurrentTimeStamp()),
                    Val4 = Extract<double>(DateTiemElement.Hour, CurrentTimeStamp()),
                    Val5 = Extract<double>(DateTiemElement.Minute, CurrentTimeStamp()),
                    Val6 = Extract<double>(DateTiemElement.Second, CurrentTimeStamp()),
                    Val7 = Extract<double>(DateTiemElement.Millisecond, CurrentTimeStamp()),
                    Val8 = Extract<double>(DateTiemElement.Microsecond, CurrentTimeStamp()),
                    Val9 = Extract<double>(DateTiemElement.Quarter, CurrentTimeStamp()),
                    Val10 = Extract<double>(DateTiemElement.Week, CurrentTimeStamp())
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
                Val1 = Extract<long>(DateTiemElement.Year, CurrentTimeStamp()),
                Val2 = Extract<long>(DateTiemElement.Month, CurrentTimeStamp()),
                Val3 = Extract<long>(DateTiemElement.Day, CurrentTimeStamp()),
                Val4 = Extract<long>(DateTiemElement.Hour, CurrentTimeStamp()),
                Val5 = Extract<long>(DateTiemElement.Minute, CurrentTimeStamp()),
                Val6 = Extract<long>(DateTiemElement.Second, CurrentTimeStamp()),
                Val7 = Extract<long>(DateTiemElement.Quarter, CurrentTimeStamp()),
                Val8 = Extract<long>(DateTiemElement.Week, CurrentTimeStamp())
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
                    Val1 = DatePart(DateTiemElement.Year, CurrentTimeStamp()),
                    Val2 = DatePart(DateTiemElement.Quarter, CurrentTimeStamp()),
                    Val3 = DatePart(DateTiemElement.Month, CurrentTimeStamp()),
                    Val4 = DatePart(DateTiemElement.Dayofyear, CurrentTimeStamp()),
                    Val5 = DatePart(DateTiemElement.Day, CurrentTimeStamp()),
                    Val6 = DatePart(DateTiemElement.Week, CurrentTimeStamp()),
                    Val7 = DatePart(DateTiemElement.Weekday, CurrentTimeStamp()),
                    Val8 = DatePart(DateTiemElement.Hour, CurrentTimeStamp()),
                    Val9 = DatePart(DateTiemElement.Minute, CurrentTimeStamp()),
                    Val10 = DatePart(DateTiemElement.Second, CurrentTimeStamp()),
                    Val11 = DatePart(DateTiemElement.Millisecond, CurrentTimeStamp()),
                    Val12 = DatePart(DateTiemElement.Microsecond, CurrentTimeStamp()),
                    Val13 = DatePart(DateTiemElement.Nanosecond, CurrentTimeStamp()),
                    Val14 = DatePart(DateTiemElement.ISO_WEEK, CurrentTimeStamp())
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
        public void Test_Cast()
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
        public void Test_Coalesce()
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
        public void Test_NVL()
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
    }
}