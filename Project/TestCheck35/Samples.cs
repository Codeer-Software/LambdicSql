using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

//important
using Dapper;
using LambdicSql;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;

namespace TestCore
{
    public class Samples
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

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
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DB
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public Data tbl_data { get; set; }
        }

        public class RemunerationNullable
        {
            public int id { get; set; }
            public int? staff_id { get; set; }
            public DateTime? payment_date { get; set; }
            public decimal? money { get; set; }
        }

        public class DBNullable
        {
            public Staff tbl_staff { get; set; }
            public RemunerationNullable tbl_remuneration { get; set; }
        }

        public class SelectData1
        {
            public string Name { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public class SelectData2
        {
            public string Name { get; set; }
            public decimal Count { get; set; }
            public decimal Total { get; set; }
            public decimal Average { get; set; }
            public decimal Minimum { get; set; }
            public decimal Maximum { get; set; }
        }

        public class SelectData3
        {
            public string Name { get; set; }
            public decimal Count { get; set; }
            public decimal Total { get; set; }
        }

        public class SelectData4
        {
            public int Id { get; set; }
        }

        public class SelectData5
        {
            public string Type { get; set; }
        }

        public class SelectData6
        {
            public string Name { get; set; }
            public decimal Total { get; set; }
        }

        public class SelectData7
        {
            public string Name { get; set; }
            public DateTime? PaymentDate { get; set; }
            public decimal? Money { get; set; }
        }

        public class SelectData8
        {
            public decimal Avg { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public void TestStandard()
        {
            var min = 3000;

            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Select one table.
        public void TestSelectFrom()
        {
            //make sql.
            var query = Sql<DB>.Create(db => SelectFrom(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Group by.
        public void TestGroupBy()
        {
            //make sql.
            var query = Sql< DB>.Create(db =>
                Select(new SelectData2
                {
                    Name = db.tbl_staff.name,
                    Count = Count(db.tbl_remuneration.money),
                    Total = Sum(db.tbl_remuneration.money),
                    Average = Avg(db.tbl_remuneration.money),
                    Minimum = Min(db.tbl_remuneration.money),
                    Maximum = Max(db.tbl_remuneration.money),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Group by using Distinct.
        public void TestGroupByPredicateDistinct()
        {
            var distinct = AggregatePredicate.Distinct;
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData3()
                {
                    Name = db.tbl_staff.name,
                    Count = Count(distinct, db.tbl_remuneration.money),
                    Total = Sum(AggregatePredicate.Distinct, db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Group by using All.
        public void TestGroupByPredicateAll()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData3()
                {
                    Name = db.tbl_staff.name,
                    Count = Count(AggregatePredicate.All, db.tbl_remuneration.money),
                    Total = Sum(AggregatePredicate.All, db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Having
        public void TestHaving()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData3
                {
                    Name = db.tbl_staff.name,
                    Count = Count(db.tbl_remuneration.money),
                    Total = Sum(db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name).
                Having(10000 < Sum(db.tbl_remuneration.money)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Like, In, Between
        public void TestLike()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(Like(db.tbl_staff.name, "%son%")));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestIn()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(In(db.tbl_staff.id, 1, 3)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestBetween()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(Between(db.tbl_staff.id, 1, 3)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Distinct
        //```cs
        public void TestSelectPredicateDistinct()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new SelectData4()
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        //```

        //All
        //```cs
        public void TestSelectPredicateAll()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new SelectData4()
                {
                    Id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        //```

        //Delete all.
        //```cs
        public void TestDelete()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Delete().
                From(db.tbl_data));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(info.SqlText, info.Parameters);
        }
        //```

        //Delete condition matching row. 
        //```cs
        public void TestDeleteWhere()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(info.SqlText, info.Parameters);
        }
        //```

        //Insert.
        //```cs
        public void TestInsert()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val1, db.tbl_data.val2).
                   Values(1, 10, "a"));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(info.SqlText, info.Parameters);
        }

        //Update
        //```cs
        public void TestUpdate()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).
                Set(new Data() { val1 = 100, val2 = "200" }).
                Where(db.tbl_data.id == 1));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(info.SqlText, info.Parameters);
        }
        //```

        //Update using table value.
        //```cs
        public void TestUpdateUsingTableValue()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).
                Set(new Data() { val1 = db.tbl_data.val1 * 2 }).
                Where(db.tbl_data.id == 1));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(info.SqlText, info.Parameters);
        }
        //```

        public void TestIsNull()
        {
            decimal? val = null;

            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db.tbl_staff.name == null || db.tbl_remuneration.money == val));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestIsNotNull()
        {
            decimal? val = null;

            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db.tbl_staff.name != null || db.tbl_remuneration.money != val));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        public void TestNullable()
        {
            //make sql.
            var query = Sql<DBNullable>.Create(db =>
                Select(new SelectData7()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(db.tbl_remuneration.money != null));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestStringCalc()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
            Select(new SelectData1()
            {
                Name = db.tbl_staff.name + "x"
            }).
            From(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestWhereEx()
        {
            TestWhereEx(true, true);
            TestWhereEx(true, false);
            TestWhereEx(false, true);
            TestWhereEx(false, false);
        }

        public void TestWhereEx(bool minCondition, bool maxCondition)
        {
            //make sql.
            var exp = Sql<DB>.Create(db =>
                Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
                Condition(maxCondition, db.tbl_remuneration.money < 4000));

            var query = Sql<DB>.Create(db => SelectFrom(db.tbl_remuneration).Where(exp));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        public void TestCase1()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData5()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                                Else("z").
                           End()
                }).
                From(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
        
        public void TestCase2()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData5()
                {
                    Type = Case(db.tbl_staff.id).
                                When(3).Then("x").
                                When(4).Then("y").
                                Else("z").
                           End()
                }).
                From(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //window functions.
        public void TestWindow()
        {
            //not supported db.
            if (_connection.GetType().FullName == "System.Data.SQLite.SQLiteConnection") return;

            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData8()
                {
                    Avg = Window.Avg(db.tbl_remuneration.money).
                            Over<decimal>(new PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 1)),
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //Building Query
        //Concat query.
        public void TestQueryConcat()
        {
            //make sql.
            var select = Sql<DB>.Create(db => 
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }));

            var from = Sql<DB>.Create(db => 
                 From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var where = Sql<DB>.Create(db => 
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var orderby = Sql<DB>.Create(db => 
                 OrderBy(new Asc(db.tbl_staff.name)));

            var query = select.Concat(from).Concat(where).Concat(orderby);

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        public void TestSqlExpression()
        {
            //make sql.
            var expMoneyAdd = Sql<DB>.Create(db => db.tbl_remuneration.money + 100);
            var expWhereMin = Sql<DB>.Create(db => 3000 < db.tbl_remuneration.money);
            var expWhereMax = Sql<DB>.Create(db => db.tbl_remuneration.money < 4000);

            var query = Sql< DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = expMoneyAdd,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(expWhereMin && expWhereMax).
                OrderBy(new Asc(db.tbl_staff.name)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //You can use sub query.
        public void TestSubQueryAtWhere()
        {
            //make sql.
            var sub = Sql<DB>.Create(db => 
                Select(new { total = db.tbl_remuneration.staff_id }).
                From(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData6 { Name = db.tbl_staff.name }).
                From(db.tbl_staff).
                Where(In(db.tbl_staff.id, sub.Cast<int>())));//sub query.

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        public void TestSubQuerySelect()
        {
            //make sql.
            var sub = Sql<DB>.Create(db => 
                Select(new SelectData6 { Total = Sum(db.tbl_remuneration.money) }).
                From(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData6
                {
                    Name = db.tbl_staff.name,
                    Total = sub.Cast<decimal>()//sub query.
                }).
                From(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        public void TestSubQueryAtFrom()
        {
            //make sql.
            var subQuery = Sql<DB>.Create(db => 
                Select(new
                {
                    name_sub = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));
            
            var query = Sql<DB>.Create(db => 
                Select(new SelectData6
                {
                    Name = subQuery.Cast().name_sub
                }).
                From(subQuery));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //You can use text.
        public void TestFormatText()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = Text<decimal>("{0} + {1}", db.tbl_remuneration.money, 1000),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }

        //2 way sql
        public void TestFormat2WaySql()
        {
            TestFormat2WaySql(true, true, 1000);
            TestFormat2WaySql(true, false, 2000);
            TestFormat2WaySql(false, true, 3000);
            TestFormat2WaySql(false, false, 4000);
        }
        public void TestFormat2WaySql(bool minCondition, bool maxCondition, decimal bonus)
        {
            //make sql.
            //replace /*no*/---/**/.
            var sql = @"
SELECT
	tbl_staff.name AS name,
    tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money + /*0*/1000/**/ AS money
FROM tbl_remuneration 
    JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id
/*1*/WHERE tbl_remuneration.money = 100/**/";
            
            var addMoney = Sql<DB>.Create(db => bonus);

            var where = Sql<DB>.Create(db =>
                Where(
                    Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
                    Condition(maxCondition, db.tbl_remuneration.money < 4000)));

            var query = TwoWaySql.Format(sql, addMoney, where);

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(info.SqlText, info.Parameters).ToList();
        }
    }
}
