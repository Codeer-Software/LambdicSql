﻿using LambdicSql;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using static LambdicSql.KeyWords;
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

            SqlOption.Log = null;
            DeleteEx();
            SqlOption.Log = l =>
            {
                Debug.Print("");
                Debug.Print(testName);
                Debug.Print("```sql");
                Debug.Print(l);
                Debug.Print("```");
            };
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
        public class DB
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
        
        public void StandardNoramlType()
        {
            //log for debug.
            SqlOption.Log = l => Debug.Print(l);

            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var datas = query.ToExecutor(_connection).Read();
        }

        //Select one table.
        public void SelectFromX()
        {
            //make sql.
            var query = Sql<DB>.Create(db => SelectFrom(db.tbl_staff));

            //execute.
            var datas = query.ToExecutor(_connection).Read();
        }

        //Group by.
        public void GroupBy()
        {
            var query = Sql< DB>.Create(db =>
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

            var datas = query.ToExecutor(_connection).Read();
            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}", e.name, e.count, e.total, e.average, e.minimum, e.maximum);
            }
        }

        //Group by using Distinct.
        public void GroupByPredicateDistinct()
        {
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    count = Count(AggregatePredicate.Distinct, db.tbl_remuneration.money),
                    total = Sum(AggregatePredicate.Distinct, db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = query.ToExecutor(_connection).Read();
        }

        //Group by using All.
        public void GroupByPredicateAll()
        {
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    count = Count(AggregatePredicate.All, db.tbl_remuneration.money),
                    total = Sum(AggregatePredicate.All, db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name));

            var datas = query.ToExecutor(_connection).Read();
        }

        //Having
        public void Having()
        {
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    total = Sum(db.tbl_remuneration.money)
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                GroupBy(db.tbl_staff.id, db.tbl_staff.name).
                Having(10000 < Sum(db.tbl_remuneration.money)));

            var datas = query.ToExecutor(_connection).Read();
        }

        //Like, In, Between
        public void LikeX()
        {
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(Like(db.tbl_staff.name, "%son%")));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void InX()
        {
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(In(db.tbl_staff.id, 1, 3)));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void BetweenX()
        {
            var query = Sql<DB>.Create(db =>
                SelectFrom(db.tbl_staff).
                Where(Between(db.tbl_staff.id, 1, 3)));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        //Distinct
        //```cs
        public void SelectPredicateDistinct()
        {
            var datas = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new
                {
                    id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration)).
                ToExecutor(_connection).Read();
        }
        //```

        //All
        //```cs
        public void SelectPredicateAll()
        {
            var datas = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new
                {
                    id = db.tbl_remuneration.staff_id
                }).
                From(db.tbl_remuneration)).
                ToExecutor(_connection).Read();
        }
        //```

        //Delete all.
        //```cs
        public void DeleteEx()
        {
            var count = Sql<DBData>.Create(db =>
                Delete().
                From(db.tbl_data)).
                ToExecutor(_connection).Write();
        }
        //```

        //Delete condition matching row. 
        //```cs
        public void DeleteWhere()
        {
            var count = Sql<DBData>.Create(db =>
                Delete().
                From(db.tbl_data).
                Where(db.tbl_data.id == 3)).
                ToExecutor(_connection).Write();
        }
        //```

        //Insert.
        //```cs
        public void Insert()
        {
            var count = Sql<DBData>.Create(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val1, db.tbl_data.val2).
                   Values(1, 10, "a")).
                ToExecutor(_connection).Write();
        }

        //Update
        //```cs
        public void UpdateX()
        {
            var count1 = Sql<DBData>.Create(db =>
                Update(db.tbl_data).
                Set(new Data() { val1 = 100, val2 = "200" }).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();
        }
        //```

        //Update using table value.
        //```cs
        public void UpdateUsingTableValue()
        {
            var count2 = Sql<DBData>.Create(db =>
                Update(db.tbl_data).
                Set(new Data() { val1 = db.tbl_data.val1 * 2 }).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();
        }
        //```
        
        public class Data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DBData
        {
            public Data tbl_data { get; set; }
        }
        
        public void IsNull()
        {
            decimal? val = null;
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db.tbl_staff.name == null || db.tbl_remuneration.money == val));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void IsNotNull()
        {
            decimal? val = null;
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
               Where(db.tbl_staff.name != null || db.tbl_remuneration.money != val));

            var datas = query.ToExecutor(_connection).Read();
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

        public class SelectDataNullable
        {
            public string name { get; set; }
            public DateTime? payment_date { get; set; }
            public decimal? money { get; set; }
        }
        
        public void Nullable()
        {
            var query = Sql<DBNullable>.Create(db =>
                Select(new SelectDataNullable()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(db.tbl_remuneration.money != null));

            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void StringCalc()
        {
            var query = Sql<DB>.Create(db =>
            Select(new SelectData()
            {
                name = db.tbl_staff.name + "x"
            }).
            From(db.tbl_staff));

            //execute.
            var datas = query.ToExecutor(_connection).Read();
        }
        
        public void WhereEx()
        {
            WhereEx(true, true);
            WhereEx(true, false);
            WhereEx(false, true);
            WhereEx(false, false);
        }

        public void WhereEx(bool minCondition, bool maxCondition)
        {
            var exp = Sql<DB>.Create(db =>
                Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
                Condition(maxCondition, db.tbl_remuneration.money < 4000));

            var datas = Sql<DB>.Create(db => SelectFrom(db.tbl_remuneration).Where(exp.Cast<bool>())).
                ToExecutor(_connection).Read();
        }
        
        public void Case1()
        {
            var caseExp = Sql<DB>.Create(db =>
                Case().
                When(db.tbl_staff.id == 3).Then("x").
                When(db.tbl_staff.id == 4).Then("y").
                Else("z").
                End());

            var datas = Sql<DB>.Create(db => 
                Select(new
                {
                    type = caseExp.Cast<string>()
                }).
                From(db.tbl_staff)).
                ToExecutor(_connection).Read();
        }
        
        public void Case2()
        {
            var caseExp = Sql<DB>.Create(db =>
                Case(db.tbl_staff.id).
                When(3).Then("x").
                When(4).Then("y").
                Else("z").
                End());

            var datas = Sql<DB>.Create(db => 
                Select(new
                {
                    type = caseExp.Cast<string>()
                }).
                From(db.tbl_staff)).
                ToExecutor(_connection).Read();
        }

        //Building Query
        //Concat query.
        public void QueryConcat()
        {
            var select = Sql<DB>.Create(db => 
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }));

            var from = Sql<DB>.Create(db => 
                 From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var where = Sql<DB>.Create(db => 
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var orderby = Sql<DB>.Create(db => 
                 OrderBy(new Asc(db.tbl_staff.name)));

            var query = select.Concat(from).Concat(where).Concat(orderby);

            //execute.
            var datas = query.ToExecutor(_connection).Read();
        }

        public void SqlExtension()
        {
            var expMoneyAdd = Sql<DB>.Create(db => db.tbl_remuneration.money + 100);
            var expWhereMin = Sql<DB>.Create(db => 3000 < db.tbl_remuneration.money);
            var expWhereMax = Sql<DB>.Create(db => db.tbl_remuneration.money < 4000);

            var query = Sql< DB>.Create(db =>
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = expMoneyAdd.Cast<decimal>(),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(expWhereMin.Cast<bool>() && expWhereMax.Cast<bool>()).
                OrderBy(new Asc(db.tbl_staff.name)));

            //execute.
            var datas = query.ToExecutor(_connection).Read();

        }

        //You can use sub query.
        public void WhereInSubQuery()
        {
            var sub = Sql<DB>.Create(db => 
                Select(new { total = db.tbl_remuneration.staff_id }).
                From(db.tbl_remuneration));

            var datas = Sql<DB>.Create(db => 
                Select(new { name = db.tbl_staff.name }).
                From(db.tbl_staff).
                Where(In(db.tbl_staff.id, sub.Cast<int>()))).//sub query.
                ToExecutor(_connection).Read();
        }

        public void SelectSubQuery()
        {
            var sub = Sql<DB>.Create(db => 
                Select(new { total = Sum(db.tbl_remuneration.money) }).
                From(db.tbl_remuneration));

            var datas = Sql<DB>.Create(db => 
                Select(new
                {
                    name = db.tbl_staff.name,
                    total = sub.Cast<decimal>()//sub query.
                }).
                From(db.tbl_staff)).
                ToExecutor(_connection).Read();
        }

        public class DBSub : DB
        {
            public SelectData tbl_sub { get; set; }
        }
        
        public void FromSubQuery()
        {
            var subQuery = Sql<DB>.Create(db => 
                Select(new SelectData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));
            
            var query = Sql<DB>.Create(db => 
                Select(new
                {
                    name = subQuery.Cast().name
                }).
                From(subQuery.Cast()));

            var datas = query.ToExecutor(_connection).Read();
        }
    }
}
