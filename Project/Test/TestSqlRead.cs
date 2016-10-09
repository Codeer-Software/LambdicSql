using LambdicSql;
using LambdicSql.SqlBase;
using LambdicSql.feat.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity;
using LambdicSql.feat.EntityFramework;
using Dapper;

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
        public class Work
        {
            public DateTime date { get; set; }
            public int member1_id { get; set; }
            public int member2_id { get; set; }
            public int member3_id { get; set; }
        }

        public class Data
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public Work tbl_work { get; set; }
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
            public Types tbl_types { get; set; }
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

            var query = Sql<DataChangeTest>.Create(db =>
                Delete().
                From(db.tbl_types));
            _connection.Execute(query);
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
                    type = caseExp.Body,
                    total = subQuery.Cast<decimal>()
                }).
                From(db.tbl_staff).
                    Join(db.tbl_remuneration, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(condition).
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
                InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "b"));

            query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Write();
        }

        [TestMethod]
        public void InsertEx2()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new tbl_data() { id = 1, val1 = 10, val2 = "a" };

            DeleteX();

            var query = Sql<DataChangeTest>.Create(db =>
                InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(data));

            query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Write();
        }

        public class Types
        {
            public string ntext { get; set; }
            public string text { get; set; }
            public string nchar { get; set; }
            public string @char { get; set; }
            public string varchar { get; set; }
            public string nvarchar { get; set; }
            public int id { get; set; }
            public DateTime datetime { get; set; }
            public DateTime datetime2 { get; set; }
        }

        public class TypesParamInfo : IParamInfo
        {
            Types _core;
            public string ntext => _core.ntext;
            public string text => _core.text;
            public DbParam nchar => new DbParam() { Value = _core.nchar, DbType = DbType.StringFixedLength, Size = 50 };
            public DbParam @char => new DbParam() { Value = _core.@char, DbType = DbType.AnsiStringFixedLength, Size = 50 };
            public DbParam varchar => new DbParam() { Value = _core.varchar, DbType = DbType.AnsiString, Size = 50 };
            public DbParam nvarchar => new DbParam() { Value = _core.nvarchar, DbType = DbType.String, Size = 50 };
            public int id { get; set; }
            public DbParam datetime => new DbParam() { Value = _core.datetime, DbType = DbType.DateTime };
            public DbParam datetime2 => new DbParam() { Value = _core.datetime2, DbType = DbType.DateTime2, Size = 7 };

            public TypesParamInfo(Types core)
            {
                _core = core;
            }
        }

        [TestMethod]
        public void InsertEx3()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new Types()
            {
                id = 1,
                text = "text",
                ntext = "ntext",
                @char = "char",
                nchar = "nchar",
                varchar = "varchar",
                nvarchar = "nvarchar",
                datetime = new DateTime(1999, 1, 1),
                datetime2 = new DateTime(2000, 1, 1)
            };

            DeleteX();
        }

        [TestMethod]
        public void InsertEx4()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new Types()
            {
                id = 1,
                text = "text",
                ntext = "ntext",
                @char = "char",
                nchar = "nchar",
                varchar = "varchar",
                nvarchar = "nvarchar",
                datetime = new DateTime(1999, 1, 1),
                datetime2 = new DateTime(2000, 1, 1)
            };

            DeleteX();

            var query = Sql<DataChangeTest>.Create(db =>
                InsertInto(db.tbl_types, db.tbl_types.id).Values(data));
            var info = query.ToSqlInfo(typeof(SqlConnection));
            var detail = info.DbParams;
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void InsertAll()
        {
            SqlOption.Log = l => Debug.Print(l);

            var data = new tbl_data() { id = 1, val1 = 10, val2 = "a" };

            DeleteX();

            var query = Sql<DataChangeTest>.Create(db =>
                InsertInto(db.tbl_data).Values(data));

            query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Write();
        }

        [TestMethod]
        public void UpdateEx()
        {
            SqlOption.Log = l => Debug.Print(l);
            var count1 = Sql<DataChangeTest>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, 100), new Assign(db.tbl_data.val2, "200")).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();

            var count2 = Sql<DataChangeTest>.Create(db =>
                Update(db.tbl_data).Set(new Assign(db.tbl_data.val1, db.tbl_data.val1 * 2)).
                Where(db.tbl_data.id == 1)).
                ToExecutor(_connection).Write();

            InsertEx3();
            var count3 = Sql<DataChangeTest>.Create(db =>
                Update(db.tbl_types).Set(new Assign(db.tbl_types.nvarchar, new DbParam() { Value = "nvarchar", DbType = DbType.String })).
                Where(db.tbl_types.id == 1)).
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

        public ISqlExpressionBase GetExp()
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
            var query = Sql<Data>.Create(db => TwoWaySql(sql,
                bonus,
                Where(
                    Condition(false, 3000 < db.tbl_remuneration.money) &&
                    Condition(false, db.tbl_remuneration.money < 4000))
                ));

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var datas = cnn.Query<SelectedData>(query).ToList();
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
            var datas = cnn.Query(query).ToList();
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
        
        [TestMethod]
        public void TestDifficultExp()
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
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < GetDataY(3000).X.GetMax()));

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            Debug.Print(info.SqlText);
            var datas = cnn.Query(query).ToList();
        }

        class DataX
        {
            public int Max { get; set; }
            public int GetMax() => Max;
        }
        class DataY
        {
            public DataX X { get; set; }
        }

        DataY GetDataY(int max) => new DataY() { X = new DataX() { Max = max } };

        //あっと、見せるようのサンプルにはもう少しDBにテーブル足せばいいか。
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

        //noraml type.
        [Table("vvv.tbl_staff")]
        public class StaffX
        {
            [Column("id")]
            public int idx { get; set; }
            [Column("name")]
            public string namex { get; set; }
        }

        [Table("vvv.tbl_remuneration")]
        public class RemunerationX
        {
            [Column("id")]
            public int idx { get; set; }
            [Column("staff_id")]
            public int staff_idx { get; set; }
            [Column("payment_date")]
            public DateTime payment_datex { get; set; }
            [Column("money")]
            public decimal moneyx { get; set; }
        }

        public class DataAttrEF
        {
            public DbSet<StaffX> xxx { get; set; }
            public DbSet<RemunerationX> yyy { get; set; }
        }
        public class DataAttr
        {
            public StaffX xxx { get; set; }
            public RemunerationX yyy { get; set; }
        }
        public class DBO
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
        }
        public class DB_EX
        {
            //スキーマも変数名で表す
            public DBO dbo { get; set; }
        }

        [TestMethod]
        public void SelectX()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<DB_EX>.Create(db =>
                Select(new
                {
                    Name = db.dbo.tbl_staff.name,
                    PaymentDate = db.dbo.tbl_remuneration.payment_date,
                    Money = db.dbo.tbl_remuneration.money,
                }).
                From(db.dbo.tbl_remuneration).
                    Join(db.dbo.tbl_staff, db.dbo.tbl_staff.id == db.dbo.tbl_remuneration.staff_id));

            var y = query.ToExecutor(new SqlConnection(TestEnvironment.SqlServerConnectionString)).Read();
        }


        [TestMethod]
        public void SelectAttr()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<DataAttr>.Create(db =>
                Select(new
                {
                    Name = db.xxx.namex,
                    PaymentDate = db.yyy.payment_datex,
                    Money = db.yyy.moneyx,
                }).
                From(db.yyy).
                    Join(db.xxx, db.xxx.idx == db.yyy.staff_idx));

            Debug.Print(query.ToSqlInfo().SqlText);
        }

        [TestMethod]
        public void SelectAttrEF()
        {
            SqlOption.Log = l => Debug.Print(l);
            var query = Sql<DataAttrEF>.Create(db =>
                Select(new
                {
                    Name = db.xxx.T().namex,
                    PaymentDate = db.yyy.T().payment_datex,
                    Money = db.yyy.T().moneyx,
                }).
                From(db.yyy).
                    Join(db.xxx, db.xxx.T().idx == db.yyy.T().staff_idx));

            Debug.Print(query.ToSqlInfo().SqlText);
        }

        public class SelectDataEF
        {
            public string name { get; set; }
            public string payment_date { get; set; }
            public decimal? money { get; set; }
        }

        [TestMethod]
        public void TestEF()
        {
            var m = new ModelLambdicSqlTestDB();
            var datas = m.tbl_staff.ToList();

            var query = Sql<ModelLambdicSqlTestDB>.Create(db =>
                Select(new SelectDataEF()
                {
                    name = db.tbl_staff.T().name + "xxx",
                    payment_date = db.tbl_remuneration.T().payment_date,
                    money = db.tbl_remuneration.T().money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.T().staff_id == db.tbl_staff.T().id));

            var xxx = query.SqlQuery(m).ToArray();


            var queryDelete = Sql<ModelLambdicSqlTestDB>.Create(db =>
                Delete().
                From(db.tbl_data));

            int a = queryDelete.ExecuteSqlCommand(m);

            var queryInsert = Sql<ModelLambdicSqlTestDB>.Create(db =>
                InsertInto(db.tbl_data.T(), db.tbl_data.T().id, db.tbl_data.T().val2).Values(1, "b"));
            var b = queryInsert.ExecuteSqlCommand(m);

        }

        [TestMethod]
        public void TestEFAndLambdic()
        {
            var query = Sql<ModelLambdicSqlTestDB>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.T().name,
                    payment_date = db.tbl_remuneration.T().payment_date,
                    money = db.tbl_remuneration.T().money,
                }).
                From(db.tbl_remuneration.T()).
                    Join(db.tbl_staff, db.tbl_remuneration.T().staff_id == db.tbl_staff.T().id).
                Where(3000 < db.tbl_remuneration.T().money && db.tbl_remuneration.T().money < 4000));
            
            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            Debug.Print(info.SqlText);
            var datas = cnn.Query(query).ToList();
        }

        [TestMethod]
        public void ParamName()
        {
            var min = 3000;
            var max = 4000;

            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < max));

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            Debug.Print(info.SqlText);
            var datas = cnn.Query(query).ToList();
        }

        [TestMethod]
        public void ParamName2()
        {
            var min = 3000;
            var max = 4000;

            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    money = MoneyExp(3, 10).Body,
                }).
                From(db.tbl_remuneration).
                Where(min < db.tbl_remuneration.money && db.tbl_remuneration.money < max));

            var cnn = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            var info = query.ToSqlInfo(cnn.GetType());
            Debug.Print(info.SqlText);
            var datas = cnn.Query(query).ToList();
        }

        public ISqlExpression<decimal> MoneyExp(int min, int max)
            => Sql<Data>.Create(db => db.tbl_remuneration.money + min + max);

        [TestMethod]
        public void TestParam()
        {
            var xxx = new DbParam<string>() { Value = "xxx" };
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name + xxx,
                }).
                From(db.tbl_staff));

            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TestParam2()
        {
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    name = db.tbl_staff.name 
                        + new DbParam<string>() { Value = "xxx", DbType = DbType.AnsiStringFixedLength, Size = 10  },
                }).
                From(db.tbl_staff));

            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TestParam3()
        {
            var a = TestParamCore("a", DbType.AnsiString).DbParams.First().Value;
            Assert.AreEqual(a.DbType, DbType.AnsiString);
            Assert.AreEqual(a.Value, "a");
            var b = TestParamCore("b", DbType.String).DbParams.First().Value;
            Assert.AreEqual(b.DbType, DbType.String);
            Assert.AreEqual(b.Value, "b");
        }

        public SqlInfo TestParamCore(string value, DbType type)
        {
            var query = Sql<Data>.Create(db => new DbParam<string>() { Value = value, DbType = type });
            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
            return info;
        }

        [TestMethod]
        public void TestColumnOnly()
        {
            var query = Sql<Data>.Create(db => ColumnOnly(db.tbl_remuneration.id));
            var info = query.ToSqlInfo(typeof(SqlConnection));
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TestQ()
        {
            var query = Sql<Data>.Create(db =>
                Select(new
                {
                    Name = db.tbl_staff.name + "★",
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id).
                Where(1000000 < db.tbl_remuneration.money));

            var info = query.ToSqlInfo(new SqlConvertOption() { ParameterPrefix = ":", StringAddOperator = "||" });
            Debug.Print(info.SqlText);
        }

        [TestMethod]
        public void TestAsterisk1()
        {
            var query = Sql<Data>.Create(db =>
               Select(Asterisk(db.tbl_remuneration)).
               From(db.tbl_remuneration));

            var info = query.ToSqlInfo(new SqlConvertOption() { ParameterPrefix = ":", StringAddOperator = "||" });
            Debug.Print(info.SqlText);

            var data = _connection.Query(query);
        }

        [TestMethod]
        public void TestAsterisk2()
        {
            var query = Sql<Data>.Create(db =>
               Select(new Asterisk<Remuneration>()).
               From(db.tbl_remuneration));

            var info = query.ToSqlInfo(new SqlConvertOption() { ParameterPrefix = ":", StringAddOperator = "||" });
            Debug.Print(info.SqlText);

            var data = _connection.Query(query);
        }

        public enum CheckEnum
        {
            A,
            B
        }

        [TestMethod]
        public void TestConditionEnum()
        {
            var A = CheckEnum.A;
            {
                var query = Sql<Data>.Create(db => Condition(A == CheckEnum.B, db.tbl_staff.id == 3));
                Debug.Print("1: " + query.ToSqlInfo().SqlText);
            }
            {
                var query = Sql<Data>.Create(db => Condition(A != CheckEnum.B, db.tbl_staff.id == 3));
                Debug.Print("2: " + query.ToSqlInfo().SqlText);
            }
            {
                var query = Sql<Data>.Create(db => Condition(!(A == CheckEnum.B), db.tbl_staff.id == 3));
                Debug.Print("3: " + query.ToSqlInfo().SqlText);
            }
            {
                var query = Sql<Data>.Create(db => Condition(string.IsNullOrEmpty(null), db.tbl_staff.id == 3));
                Debug.Print("4: " + query.ToSqlInfo().SqlText);
            }
            {
                var query = Sql<Data>.Create(db => Condition(!string.IsNullOrEmpty(null), db.tbl_staff.id == 3));
                Debug.Print("5: " + query.ToSqlInfo().SqlText);
            }
        }
    }
}
