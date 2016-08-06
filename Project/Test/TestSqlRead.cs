using Dapper;
using LambdicSql;
using LambdicSql.QueryBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;

namespace Test
{
    //TODO must to be detail testing.
    [TestClass]
    public class TestSqlRead
    {
        //noraml type.
        public class Staff
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Remuneration
        {
            public int id { get; set; }
            public int staff_id { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }
        public class Data
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
        }
        public class SelectData
        {
            public string name { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        class Data2
        {
            public Staff tbl_staff { get; set; }
            public SelectData tbl_sub { get; set; }
        }
        public class tbl_data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DataChangeTest
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public tbl_data tbl_data { get; set; }
        }

        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection("SQLServer");
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod]
        public void DeleteX()
        {
            SqlOption.Log = l => Debug.Print(l);

            var count = Sql<DataChangeTest>.Create(db => 
                Delete().
                From(db.tbl_data)).
                ToExecutor(_connection).Write();

        }

        [TestMethod]
        public void DeleteWhere()
        {
            SqlOption.Log = l => Debug.Print(l);

            var count = Sql<DataChangeTest>.Create(db => 
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3)).
                ToExecutor(_connection).Write();
        }

        [TestMethod]
        public void SelectEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void GroupByEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    count = Count(db.tbl_remuneration.money),
                    total = Sum(db.tbl_remuneration.money),
                    average = Avg(db.tbl_remuneration.money),
                    minimum = Min(db.tbl_remuneration.money),
                    maximum = Max(db.tbl_remuneration.money),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void OrderByEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000).
                OrderBy(new Asc(db.tbl_staff.id), new Desc(db.tbl_remuneration.id)));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void SubEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var q = Sql<Data>.Create(db =>
                Select(new
                {
                    id = db.tbl_staff.id,
                    rid = db.tbl_remuneration.id,
                    type = Case().
                                When(db.tbl_remuneration.money < 1000).Then("poverty").
                                When(4000 < db.tbl_remuneration.money).Then("rich").
                                Else("normal").
                            End(),
                    total = Select(new { total = Sum(db.tbl_remuneration.money) }).
                            From(db.tbl_remuneration).Cast<decimal>()
                }).
                From(db.tbl_staff).
                    Join(db.tbl_remuneration, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(100 < db.tbl_remuneration.money && db.tbl_remuneration.money < 500).
                OrderBy(new Asc(db.tbl_remuneration.money)));

            var datas = q.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void SubEx2()
        {
            SqlOption.Log = l => Debug.Print(l);

            var caseExp = Sql<Data>.Create(db => 
                Case().
                    When(db.tbl_remuneration.money < 1000).Then("poverty").
                    When(4000 < db.tbl_remuneration.money).Then("rich").
                    Else("normal").
                End());

            var subQuery = Sql<Data>.Create(db =>
                Select(new { total = Sum(db.tbl_remuneration.money) }).
                            From(db.tbl_remuneration));

            var condition = Sql<Data>.Create(db =>
                 100 < db.tbl_remuneration.money && db.tbl_remuneration.money < 500);

            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    id = db.tbl_staff.id,
                    rid = db.tbl_remuneration.id,
                    type = caseExp.Cast<string>(),
                    total = subQuery.Cast<decimal>()
                }).
                From(db.tbl_staff).
                    Join(db.tbl_remuneration, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(condition.Cast<bool>()).
                OrderBy(new Asc(db.tbl_remuneration.money)));

            var datas = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void InsertEx()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new tbl_data() { id = 1, val1 = 10, val2 = "a" };

            DeleteX();
            var query = Sql<DataChangeTest>.Create(db =>
                InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val1, db.tbl_data.val2).
                Values(data.id, data.val1, data.val2));//TODO change style.

            query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Write();
        }

        [TestMethod]
        public void UpdateEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var count1 = Sql<DataChangeTest>.Create(db =>
                Update(db.tbl_data).
                Set(new tbl_data() { val1 = 100, val2 = "200" }).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();

            var count2 = Sql<DataChangeTest>.Create(db =>
                Update(db.tbl_data).
                Set(new tbl_data() { val1 = db.tbl_data.val1 * 2 }).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();
        }

        [TestMethod]
        public void WindowX()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    x = Window.Avg(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 1)),
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }
        
        [TestMethod]
        public void LagX()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    lag = Window.Lag(db.tbl_remuneration.money, 1, 0).Over<decimal>(
                            new PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date),
                            new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                            null),
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();

        }

        int b = 0;
        [TestMethod]
        public void TestMeta()
        {
            int a = 0;
            var exp = GetExp();
            var query = Sql<Data>.Create(db => a == 1 && a == b && exp.Cast<bool>());
            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
            
            query = Sql<Data>.Create(db => a == 1 && a == b && GetExp().Cast<bool>());
            info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        public ISqlExpression GetExp()
        {
            int a = 0;
            return Sql<Data>.Create(db => a == 2 && a == b);
        }

        [TestMethod]
        public void TestMetaMethod()
        {
            var info1 = Sql<Data>.Create(db => GetStatic1()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info1.SqlText);
            var info2 = Sql<Data>.Create(db => GetInstance2()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info2.SqlText);

            var info3 = Sql<Data>.Create(db => GetInstance3(GetStatic1(), GetInstance2())).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info3.SqlText);

            //check cahe.
            info1 = Sql<Data>.Create(db => GetStatic1()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info1.SqlText);
            info2 = Sql<Data>.Create(db => GetInstance2()).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info2.SqlText);
        }

        public static int GetStatic1() => 1;
        public int GetInstance2() => 2;
        public int GetInstance3(int a, int b) => a + b;

        [TestMethod]
        public void TestExpression()
        {
            var exp = GetExp();
            var query = Sql<Data>.Create(db => exp);
            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TextFormatText()
        {
            var query = Sql<Data>.Create(db => Text<object>("{0} - {1}", db.tbl_staff.id, db.tbl_staff.id == 2));
            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TestFormatText2()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = Text<decimal>("{0} + 1000", db.tbl_remuneration.money),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }

        [TestMethod]
        public void TestFormat2WaySql()
        {
            SqlOption.Log = l => Debug.Print(l);
            var sql = @"
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";

            var bonus = 1000;
            var addMoney = Sql<Data>.Create(db => bonus);

            var where = Sql<Data>.Create(db => 
                Where(
                    Condition(false, 3000 < db.tbl_remuneration.money) &&
                    Condition(false, db.tbl_remuneration.money < 4000)));
            
            var where2 = Sql<Data>.Create(db =>
                Where(
                    Condition(false, 3000 < db.tbl_remuneration.money) &&
                    Condition(false, db.tbl_remuneration.money < 4000)));


            var query = TwoWaySql.Format(sql, addMoney, where);

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            var datas = cnn.Query<SelectedData>(info.SqlText, info.Parameters).ToList();
        }

        public class SelectedData
        {
            public string name { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        [TestMethod]
        public void Dapper()
        {
            var query = Sql<Data>.Create(db =>
                Select(new SelectedData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            var datas = cnn.Query<SelectedData>(info.SqlText, info.Parameters).ToList();
        }

        [TestMethod]
        public void UsingStatic()
        {
            {
                var info = Sql<Data>.Create(db => From(db.tbl_remuneration)).ToSqlInfo(typeof(SqlConnection));
                Debug.Print(info.SqlText);
            }
            {
                var info = Sql<Data>.Create(db => GroupBy(db.tbl_remuneration)).ToSqlInfo(typeof(SqlConnection));
                Debug.Print(info.SqlText);
            }
            {
                var info = Sql<Data>.Create(db => Having(db.tbl_remuneration.id == 0)).ToSqlInfo(typeof(SqlConnection));
                Debug.Print(info.SqlText);
            }
            {
                var info = Sql<Data>.Create(db => InsertInto(db.tbl_remuneration, db.tbl_remuneration.id).Values(1)).ToSqlInfo(typeof(SqlConnection));
                Debug.Print(info.SqlText);
            }
            {
                var info = Sql<Data>.Create(db => OrderBy(new Asc(db.tbl_remuneration.id), new Desc(db.tbl_staff.id))).ToSqlInfo(typeof(SqlConnection));
                Debug.Print(info.SqlText);
            }
        }

        [TestMethod]
        public void NewSyntax()
        {
            var info = Sql<Data>.Create(db => new DateTime(1999, 1, 1)).ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }
    }

    public static class DapperApaptExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpression<ISqlKeyWord<T>> exp)
            where T : class
            => Query<T>(cnn, (ISqlExpression)exp);

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpression exp)
        {
            var info = exp.ToSqlInfo(cnn.GetType());
            var ps = new DynamicParameters();
            info.Parameters.ToList().ForEach(e => ps.Add(e.Key, e.Value));
            return cnn.Query<T>(info.SqlText, ps);
        }
    }
}
