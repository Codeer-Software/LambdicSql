using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using static Test.Helper.DBProviderInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using System.Collections.Generic;
using LambdicSql.SqlBase;

//TODO Make this a more practical sample.
//Add better data for sample
//Separate into separate projects.
namespace TestCheck35
{
    [TestClass]
    public class Samples
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
            public double Avg { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        static IDbDataParameter CreateParameter(IDbCommand com, string name, object obj)
        {
            var param = com.CreateParameter();
            param.ParameterName = name;
            param.Value = obj;
            return param;
        }

        public void Read(string text, Dictionary<string, object> param)
        {
            bool openNow = false;
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
                openNow = true;
            }
            try
            {
                using (var com = _connection.CreateCommand())
                {
                    com.CommandText = text;
                    com.Connection = _connection;
                    foreach (var obj in param.Select(e => CreateParameter(com, e.Key, e.Value)))
                    {
                        com.Parameters.Add(obj);
                    }
                    using (var sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                        }
                    }
                }

            }
            finally
            {
                if (openNow)
                {
                    _connection.Close();
                }
            }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        //Select one table.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestSelectFrom()
        {
            //make sql.
            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestJoin()
        {
            var min = 3000;

            //make sql.
            var query1 = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var query2 = Sql<DB>.Create(db =>
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            var query = query1.Concat(query2);

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        //Group by.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestGroupBy()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Name = db.tbl_staff.name,
                   Count = Count(db.tbl_remuneration.money),
                   Total = Sum(db.tbl_remuneration.money),
                   Average = (decimal)Avg(db.tbl_remuneration.money),
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
            var datas = _connection.Query(query).ToList();
        }

        //Group by using Distinct.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        //Group by using All.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        //Having
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        //Like, In, Between, Exsists
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestLike()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new Asterisk()).From(db.tbl_staff).
                Where(Like(db.tbl_staff.name, "%son%")));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestIn()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new Asterisk()).From(db.tbl_staff).
                Where(In(db.tbl_staff.id, 1, 3)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestBetween()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new Asterisk()).From(db.tbl_staff).
                Where(Between(db.tbl_staff.id, 1, 3)));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestExists()
        {
            //make sql.
            var sub = Sql<DB>.Create(db =>
                Select(new { total = db.tbl_remuneration.staff_id }).
                From(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
                Select(new SelectData6 { Name = db.tbl_staff.name }).
                From(db.tbl_staff).
                Where(Exists(sub)));//sub query.

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }


        //Distinct
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }
        //```

        //All
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }
        //```

        //Delete all.
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var count = _connection.Execute(query);
        }
        //```

        //Delete condition matching row. 
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var count = _connection.Execute(query);
        }
        //```

        //Insert.
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestInsert()
        {
            TestDelete();

            //make sql.
            var query = Sql<DB>.Create(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "val2"));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(query);
        }

        //Update
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestUpdate()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(query);
        }
        //```

        //Update using table value.
        //```cs
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestUpdateUsingTableValue()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, db.tbl_data.val1 * 2)).
                Where(db.tbl_data.id == 1));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var count = _connection.Execute(query);
        }
        //```

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_remuneration).Where(exp));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        //window functions.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestWindow()
        {
            //not supported db.
            if (_connection.GetType().FullName == "System.Data.SQLite.SQLiteConnection") return;
            if (_connection.GetType().FullName == "MySql.Data.MySqlClient.MySqlConnection") return;

            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData8()
                {
                    Avg = Window.Avg(db.tbl_remuneration.money).
                            Over(new PartitionBy(db.tbl_staff.name, db.tbl_remuneration.payment_date),
                                new OrderBy(new Asc(db.tbl_remuneration.money), new Desc(db.tbl_remuneration.payment_date)),
                                new Rows(1, 5)),
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        //Building Query
        //Concat query.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestSqlExpression()
        {
            //make sql.
            var expMoneyAdd = Sql<DB>.Create(db => db.tbl_remuneration.money + 100);
            var expWhereMin = Sql<DB>.Create(db => 3000 < db.tbl_remuneration.money);
            var expWhereMax = Sql<DB>.Create(db => db.tbl_remuneration.money < 4000);

            var query = Sql<DB>.Create(db =>
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
            var datas = _connection.Query(query).ToList();
        }

        //You can use sub query.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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
            {
                var query = Sql<DB>.Create(db =>
                    Select(new SelectData6
                    {
                        Name = subQuery.Body.name_sub
                    }).
                    From(subQuery.Body));

                //to string and params.
                var info = query.ToSqlInfo(_connection.GetType());
                Debug.Print(info.SqlText);

                //dapper
                var datas = _connection.Query(query).ToList();
            }
            {
                var query = Sql<DB>.Create(db =>
                    Select(new SelectData6
                    {
                        Name = subQuery.Body.name_sub
                    }).
                    From(subQuery));

                //to string and params.
                var info = query.ToSqlInfo(_connection.GetType());
                Debug.Print(info.SqlText);

                //dapper
                var datas = _connection.Query(query).ToList();
            }
        }

        //You can use text.
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestFormatText()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = TextSql<decimal>("{0} + {1}", db.tbl_remuneration.money, 1000),
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query<SelectData1>(query).ToList();
        }

        //2 way sql
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
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

            var query = Sql<DB>.Create(db => TwoWaySql(sql,
                bonus,
                Where(
                    Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
                    Condition(maxCondition, db.tbl_remuneration.money < 4000))
                ));
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();


            //pattern2 building.
            var where = Sql<DB>.Create(db => Where(
                    Condition(minCondition, 3000 < db.tbl_remuneration.money) &&
                    Condition(maxCondition, db.tbl_remuneration.money < 4000)));

            var query2 = Sql<DB>.Create(db => TwoWaySql(sql, bonus, where));
            info = query2.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            datas = _connection.Query(query2).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void CheckOperatior()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name + "x",
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(db.tbl_remuneration.money != 100));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void FromMany()
        {
            //make sql.
            var query = Sql<DB>.Create(db =>
                Select(new SelectData1()
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                }).
                From(db.tbl_staff, db.tbl_remuneration));

            //to string and params.
            var info = query.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);

            //dapper
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Funcs()
        {
            var name = _connection.GetType().FullName;
            if (name == "IBM.Data.DB2.DB2Connection" ||
                name == "System.Data.SqlClient.SqlConnection" ||
                name == "Npgsql.NpgsqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        id = Cast<int>(db.tbl_staff.id, "int")
                    }).
                    From(db.tbl_staff));

                ExecuteRead(query);
            }

            {
                var query = Sql<DB>.Create(db =>
                   Select(new
                   {
                       id = Coalesce(db.tbl_staff.name, "a", "b")
                   }).
                   From(db.tbl_staff));

                ExecuteRead(query);
            }

            if (name != "System.Data.SQLite.SQLiteConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Name = Concat(db.tbl_staff.name, "a")
                    }).
                    From(db.tbl_staff));

                ExecuteRead(query);
            }
            if (name == "Npgsql.NpgsqlConnection" ||
                name == "IBM.Data.DB2.DB2Connection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Length = Length(db.tbl_staff.name)
                    }).
                    From(db.tbl_staff));
                ExecuteRead(query);
            }
            if (name == "System.Data.SqlClient.SqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Length = Len(db.tbl_staff.name)
                    }).
                    From(db.tbl_staff));
                ExecuteRead(query);
            }
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Lower = Lower(db.tbl_staff.name),
                        Upper = Upper(db.tbl_staff.name)
                    }).
                    From(db.tbl_staff));
                ExecuteRead(query);
            }
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Replace(db.tbl_staff.name, "a", "b")
                    }).
                    From(db.tbl_staff));
                ExecuteRead(query);
            }
            if (name != "System.Data.SQLite.SQLiteConnection" &&
                name != "Oracle.ManagedDataAccess.Client.OracleConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Substring(db.tbl_staff.name, 1, 2)
                    }).
                    From(db.tbl_staff));
                ExecuteRead(query);
            }

            //CurrentDate
            if (name == "Npgsql.NpgsqlConnection" ||
                name == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_Date
                    }));
                ExecuteRead(query);
            }
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_Date
                    }).From(TextSql("dual")));
                ExecuteRead(query);
            }
            if (name == "IBM.Data.DB2.DB2Connection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = CurrentSpaceDate
                    }).From(TextSql("sysibm.sysdummy1")));
                ExecuteRead(query);
            }

            //CurrentTime
            if (name == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_Time
                    }));
                var x = ExecuteRead(query);
            }

            if (name == "IBM.Data.DB2.DB2Connection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = CurrentSpaceTime
                    }).From(TextSql("sysibm.sysdummy1")));
                var x = ExecuteRead(query);
            }

            //CurrentTimeStamp
            if (name == "System.Data.SqlClient.SqlConnection" ||
                name == "Npgsql.NpgsqlConnection" ||
                name == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_TimeStamp
                    }));
                ExecuteRead(query);
            }
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_TimeStamp
                    }).From(TextSql("dual")));
                ExecuteRead(query);
            }
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = Current_TimeStamp
                    }).From(TextSql("dual")));
                ExecuteRead(query);
            }
            if (name == "IBM.Data.DB2.DB2Connection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = CurrentSpaceTimeStamp
                    }).From(TextSql("sysibm.sysdummy1")));
                var x = ExecuteRead(query);
            }
            if (name == "IBM.Data.DB2.DB2Connection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst = CurrentSpaceTimeStamp
                    }).From(TextSql("sysibm.sysdummy1")));
                var x = ExecuteRead(query);
            }

            //Extract, DatePart
            if (name == "Npgsql.NpgsqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst1 = Extract(DateTimeElement.Year, Current_TimeStamp),
                        Dst2 = Extract(DateTimeElement.Month, Current_TimeStamp),
                        Dst3 = Extract(DateTimeElement.Day, Current_TimeStamp),
                        Dst4 = Extract(DateTimeElement.Hour, Current_TimeStamp),
                        Dst5 = Extract(DateTimeElement.Minute, Current_TimeStamp),
                        Dst6 = Extract(DateTimeElement.Second, Current_TimeStamp),
                        Dst7 = Extract(DateTimeElement.Millisecond, Current_TimeStamp),
                        Dst8 = Extract(DateTimeElement.Microsecond, Current_TimeStamp),
                        Dst9 = Extract(DateTimeElement.Quarter, Current_TimeStamp),
                        Dst10 = Extract(DateTimeElement.Week, Current_TimeStamp)
                    }));
                var x = ExecuteRead(query);
            }
            if (name == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Dst1 = (long)Extract(DateTimeElement.Year, Current_TimeStamp),
                        Dst2 = (long)Extract(DateTimeElement.Month, Current_TimeStamp),
                        Dst3 = (long)Extract(DateTimeElement.Day, Current_TimeStamp),
                        Dst4 = (long)Extract(DateTimeElement.Hour, Current_TimeStamp),
                        Dst5 = (long)Extract(DateTimeElement.Minute, Current_TimeStamp),
                        Dst6 = (long)Extract(DateTimeElement.Second, Current_TimeStamp),
                        Dst9 = (long)Extract(DateTimeElement.Quarter, Current_TimeStamp),
                        Dst10 = (long)Extract(DateTimeElement.Week, Current_TimeStamp)
                    }));
                var x = ExecuteRead(query);
            }
            if (name == "System.Data.SqlClient.SqlConnection")
            {
                var query = Sql<DB>.Create(db =>
                    Select(new
                    {
                        Year = DatePart(DateTimeElement.Year, Current_TimeStamp),
                        Quarter = DatePart(DateTimeElement.Quarter, Current_TimeStamp),
                        Month = DatePart(DateTimeElement.Month, Current_TimeStamp),
                        Dayofyear = DatePart(DateTimeElement.Dayofyear, Current_TimeStamp),
                        Day = DatePart(DateTimeElement.Day, Current_TimeStamp),
                        Week = DatePart(DateTimeElement.Week, Current_TimeStamp),
                        Weekday = DatePart(DateTimeElement.Weekday, Current_TimeStamp),
                        Hour = DatePart(DateTimeElement.Hour, Current_TimeStamp),
                        Minute = DatePart(DateTimeElement.Minute, Current_TimeStamp),
                        Second = DatePart(DateTimeElement.Second, Current_TimeStamp),
                        Millisecond = DatePart(DateTimeElement.Millisecond, Current_TimeStamp),
                        Microsecond = DatePart(DateTimeElement.Microsecond, Current_TimeStamp),
                        Nanosecond = DatePart(DateTimeElement.Nanosecond, Current_TimeStamp),
                        ISO_WEEK = DatePart(DateTimeElement.ISO_WEEK, Current_TimeStamp)
                    }));
                var x = ExecuteRead(query);
            }


        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestUnion()
        {
            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Union().Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
            query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Union(true).Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestIntersect()
        {
            var name = _connection.GetType().FullName;
            if (name == "MySql.Data.MySqlClient.MySqlConnection") return;
            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Intersect().Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "System.Data.SQLite.SQLiteConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;
            query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Intersect(true).Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestExcept()
        {
            var name = _connection.GetType().FullName;
            if (name == "MySql.Data.MySqlClient.MySqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;
            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Except().Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "System.Data.SQLite.SQLiteConnection") return;
            query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Except(true).Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestMinus()
        {
            var name = _connection.GetType().FullName;
            if (name != "Oracle.ManagedDataAccess.Client.OracleConnection") return;
            var query = Sql<DB>.Create(db => Select(new Asterisk()).From(db.tbl_staff).Minus().Select(new Asterisk()).From(db.tbl_staff));
            ExecuteRead<Staff>(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestGroupByEx()
        {
            //make sql.
            var selectFrom = Sql<DB>.Create(db =>
               Select(new SelectData2
               {
                   Total = Sum(db.tbl_remuneration.money)
               }).
               From(db.tbl_remuneration));

            var name = _connection.GetType().FullName;
            if (name == "System.Data.SQLite.SQLiteConnection") return;
            if (name == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var queryWith = selectFrom.Concat(Sql<DB>.Create(db => GroupByWithRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id)));
                ExecuteRead(queryWith);
                return;
            }

            var query = selectFrom.Concat(Sql<DB>.Create(db => GroupByRollup(db.tbl_remuneration.id, db.tbl_remuneration.staff_id)));
            ExecuteRead(query);

            query = selectFrom.Concat(Sql<DB>.Create(db => GroupByCube(db.tbl_remuneration.id, db.tbl_remuneration.staff_id)));
            ExecuteRead(query);

            query = selectFrom.Concat(Sql<DB>.Create(db => GroupByGroupingSets(db.tbl_remuneration.id, db.tbl_remuneration.staff_id)));
            ExecuteRead(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestOffsetFetchNext()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection" &&
                name != "IBM.Data.DB2.DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                 Select(new Asterisk()).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 OffsetRows(1).
                 FetchNextRowsOnly(3)
                 );
          //  Debug.Print(query.ToSqlInfo().SqlText);
            _connection.Query(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestLimit()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;
            if (name == "Npgsql.NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(new Asterisk()).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(1, 3)
                 );
      //      Debug.Print(query.ToSqlInfo().SqlText);
            _connection.Query(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestLimitOffset()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(new Asterisk()).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(1).
                 Offset(3)
                 );
       //     Debug.Print(query.ToSqlInfo().SqlText);
            _connection.Query(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestRowNum()
        {
            var name = _connection.GetType().FullName;
            if (name != "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(new Asterisk()).
                 From(db.tbl_remuneration).
                 Where(Between(RowNum, 1, 5)).
                 OrderBy(new Asc(db.tbl_remuneration.id))
                 );
       //     Debug.Print(query.ToSqlInfo().SqlText);
            _connection.Query(query);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestTop()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection") return;
            {
                var query = Sql<DB>.Create(db =>
                      Select(
                          new Top(10),
                          new
                          {
                              money = db.tbl_remuneration.money
                          }).
                      From(db.tbl_remuneration));
         //       Debug.Print(query.ToSqlInfo().SqlText);
                _connection.Query(query);
            }
            {
                var query = Sql<DB>.Create(db =>
                      Select(
                          new Top(10),
                          new Asterisk()).
                      From(db.tbl_remuneration));
         //       Debug.Print(query.ToSqlInfo().SqlText);
                _connection.Query(query);
            }
            {
                var query = Sql<DB>.Create(db =>
                      Select(
                          AggregatePredicate.Distinct,
                          new Top(10),
                          new
                          {
                              money = db.tbl_remuneration.money
                          }).
                      From(db.tbl_remuneration));
       //         Debug.Print(query.ToSqlInfo().SqlText);
                _connection.Query(query);
            }
            {
                var query = Sql<DB>.Create(db =>
                      Select(
                          AggregatePredicate.Distinct,
                          new Top(10),
                          new Asterisk()).
                      From(db.tbl_remuneration));
        //        Debug.Print(query.ToSqlInfo().SqlText);
                _connection.Query(query);
            }
        }

        public IEnumerable<T> ExecuteRead<T>(ISqlExpressionBase<IClauseChain<T>> exp)
        {
            var info = exp.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);
            return _connection.Query(exp).ToList();
        }

        public IEnumerable<T> ExecuteRead<T>(ISqlExpressionBase exp)
        {
            var info = exp.ToSqlInfo(_connection.GetType());
            Debug.Print(info.SqlText);
            return _connection.Query<T>(exp).ToList();
        }

        /*
        [TestMethod]
        public void TestAlias()
        {
            SqlOption.Log = l => Debug.Print(l);

            var member1 = Sql<Data>.Create(db => db.tbl_staff);
            var member2 = Sql<Data>.Create(db => db.tbl_staff);
            var member3 = Sql<Data>.Create(db => db.tbl_staff);

            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    date = db.tbl_work.date,
                    name1 = member1.Body.name,
                    name2 = member2.Body.name,
                    name3 = member3.Body.name
                }).
                From(db.tbl_work).
                    Join(member1, db.tbl_work.member1_id == member1.Body.id).
                    Join(member2, db.tbl_work.member2_id == member2.Body.id).
                    Join(member3, db.tbl_work.member3_id == member3.Body.id));

            var ret = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }
        */

    }
}