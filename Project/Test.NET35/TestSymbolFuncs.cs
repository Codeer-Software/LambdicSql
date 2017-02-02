using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;

namespace Test
{
    [TestClass]
    public class TestSymbolFuncs
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

        public class SelectData
        {
            public decimal Val { get; set; }
            public decimal Val1 { get; set; }
            public decimal Val2 { get; set; }
            public decimal Val3 { get; set; }
            public double ValDouble { get; set; }
            public double ValFloat { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Sum()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Val1 = Sum(db.tbl_remuneration.money),
                   Val2 = Sum(All(), db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	SUM(tbl_remuneration.money) AS Val1,
	SUM(ALL tbl_remuneration.money) AS Val2
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_1()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Val1 = Count(db.tbl_remuneration.money),
                   Val2 = Count(Asterisk()),
                   Val3 = Count(Distinct(), db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money) AS Val1,
	COUNT(*) AS Val2,
	COUNT(DISTINCT tbl_remuneration.money) AS Val3
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Count_2()
        {
            if (!_connection.IsTarget(TargetDB.Oracle, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Val1 = Count(All(), Asterisk())
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(ALL *) AS Val1
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Avg_Min_Max()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   Val1 = (decimal)Avg(db.tbl_remuneration.money),
                   Val2 = Min(db.tbl_remuneration.money),
                   Val3 = Max(db.tbl_remuneration.money),
               }).
               From(db.tbl_remuneration).
                   Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	AVG(tbl_remuneration.money) AS Val1,
	MIN(tbl_remuneration.money) AS Val2,
	MAX(tbl_remuneration.money) AS Val3
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)
GROUP BY tbl_staff.id, tbl_staff.name");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Abs_Round()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Val1 = Abs(1),
                    Val2 = Round(2, 3),
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	ABS(@p_0) AS Val1,
	ROUND(@p_1, @p_2) AS Val2
FROM tbl_staff", 1, 2, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Mod()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.Oracle, TargetDB.DB2)) return;
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Val1 = Mod(1, 2)
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	MOD(@p_0, @p_1) AS Val1
FROM tbl_staff", 1, 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Concat()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.SqlServer, TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Concat(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CONCAT(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff", "a", "b");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Length()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Length(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	LENGTH(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Len()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Len(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	LEN(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lower()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Lower(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	LOWER(tbl_staff.name) AS Val
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Upper()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Upper(db.tbl_staff.name)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	UPPER(tbl_staff.name) AS Val
FROM tbl_staff");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Replace()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Replace(db.tbl_staff.name, "a", "b")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	REPLACE(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
"a", "b");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Substring()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   Val = Substring(db.tbl_staff.name, 0, 1)
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	SUBSTRING(tbl_staff.name, @p_0, @p_1) AS Val
FROM tbl_staff",
0, 1);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Extract_1()
        {
            if (!_connection.IsTarget(TargetDB.Postgre)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val1 = Extract(DateTimeElement.Year, Current_TimeStamp()),
                    Val2 = Extract(DateTimeElement.Month, Current_TimeStamp()),
                    Val3 = Extract(DateTimeElement.Day, Current_TimeStamp()),
                    Val4 = Extract(DateTimeElement.Hour, Current_TimeStamp()),
                    Val5 = Extract(DateTimeElement.Minute, Current_TimeStamp()),
                    Val6 = Extract(DateTimeElement.Second, Current_TimeStamp()),
                    Val7 = Extract(DateTimeElement.Millisecond, Current_TimeStamp()),
                    Val8 = Extract(DateTimeElement.Microsecond, Current_TimeStamp()),
                    Val9 = Extract(DateTimeElement.Quarter, Current_TimeStamp()),
                    Val10 = Extract(DateTimeElement.Week, Current_TimeStamp())
                }));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Extract_2()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            var sql = Db<DB>.Sql(db =>
            Select(new
            {
                Val1 = (long)Extract(DateTimeElement.Year, Current_TimeStamp()),
                Val2 = (long)Extract(DateTimeElement.Month, Current_TimeStamp()),
                Val3 = (long)Extract(DateTimeElement.Day, Current_TimeStamp()),
                Val4 = (long)Extract(DateTimeElement.Hour, Current_TimeStamp()),
                Val5 = (long)Extract(DateTimeElement.Minute, Current_TimeStamp()),
                Val6 = (long)Extract(DateTimeElement.Second, Current_TimeStamp()),
                Val7 = (long)Extract(DateTimeElement.Quarter, Current_TimeStamp()),
                Val8 = (long)Extract(DateTimeElement.Week, Current_TimeStamp())
            }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    Val1 = DatePart(DateTimeElement.Year, Current_TimeStamp()),
                    Val2 = DatePart(DateTimeElement.Quarter, Current_TimeStamp()),
                    Val3 = DatePart(DateTimeElement.Month, Current_TimeStamp()),
                    Val4 = DatePart(DateTimeElement.Dayofyear, Current_TimeStamp()),
                    Val5 = DatePart(DateTimeElement.Day, Current_TimeStamp()),
                    Val6 = DatePart(DateTimeElement.Week, Current_TimeStamp()),
                    Val7 = DatePart(DateTimeElement.Weekday, Current_TimeStamp()),
                    Val8 = DatePart(DateTimeElement.Hour, Current_TimeStamp()),
                    Val9 = DatePart(DateTimeElement.Minute, Current_TimeStamp()),
                    Val10 = DatePart(DateTimeElement.Second, Current_TimeStamp()),
                    Val11 = DatePart(DateTimeElement.Millisecond, Current_TimeStamp()),
                    Val12 = DatePart(DateTimeElement.Microsecond, Current_TimeStamp()),
                    Val13 = DatePart(DateTimeElement.Nanosecond, Current_TimeStamp()),
                    Val14 = DatePart(DateTimeElement.ISO_WEEK, Current_TimeStamp())
                }));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            if (!_connection.IsTarget(TargetDB.DB2, TargetDB.SqlServer, TargetDB.Postgre)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new
                {
                    id = Cast<int>(db.tbl_remuneration.money, DataType.Int())
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CAST(tbl_remuneration.money AS INT) AS id
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Coalesce()
        {
            var sql = Db<DB>.Sql(db =>
               Select(new
               {
                   id = Coalesce(db.tbl_staff.name, "a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COALESCE(tbl_staff.name, @p_0) AS id
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NVL()
        {
            if (!_connection.IsTarget(TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
               Select(new 
               {
                   id = NVL(db.tbl_staff.name, "a")
               }).
               From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	NVL(tbl_staff.name, @p_0) AS id
FROM tbl_staff",
"a");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_First_Value()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Val = First_Value(db.tbl_remuneration.money).
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	FIRST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Last_Value()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Val = Last_Value(db.tbl_remuneration.money).
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	LAST_VALUE(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rank()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Rank().
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Dense_Rank()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Dense_Rank().
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	DENSE_RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Percent_Rank()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    ValDouble = Percent_Rank().
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	PERCENT_RANK()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS ValDouble
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cume_Dist()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = (decimal)Cume_Dist().
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	CUME_DIST()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Ntile()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Ntile(2).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	NTILE(@p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNth_Value()
        {
            if (!_connection.IsTarget(TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Nth_Value(db.tbl_remuneration.money, 2).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	NTH_VALUE(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration", 2);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.Oracle)) return;
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Lag(3).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	LAG(@p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Lag_2()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.Oracle)) return;
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Lag(db.tbl_remuneration.money, 2).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	LAG(tbl_remuneration.money, @p_0)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration", 2);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RowNumber()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Row_Number().
                            Over(OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	ROW_NUMBER()
	OVER(
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Count(db.tbl_remuneration.money).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)), Rows(1))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS 1 PRECEDING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Rows_2()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Count(db.tbl_remuneration.money).
                            Over(OrderBy(Asc(db.tbl_remuneration.money)), Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_PartitionBy()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Count(db.tbl_remuneration.money).
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over_1()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Count(db.tbl_remuneration.money).
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Over_2()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Postgre, TargetDB.DB2, TargetDB.Oracle)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData()
                {
                    Val = Count(db.tbl_remuneration.money) + 
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	COUNT(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }
    }
}