using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Test
{
    [TestClass]
    public class TestExpression
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

        class ValuesInstance
        {
            internal int field = 1;
            internal int Property => 2;
            internal int Method() => 3;

            internal int GetValue(int value) => value;
            internal int GetValue() => 1;
        }

        class ValuesStatic
        {
            internal static int field = 1;
            internal static int Property => 2;
            internal static int Method() => 3;
        }

        class Objects
        {
            internal ValuesInstance fieldValue = new ValuesInstance();
            internal ValuesInstance PropertyValue => new ValuesInstance();
            internal ValuesInstance MethodValue() => new ValuesInstance();
            internal Objects fieldObject;
            internal Objects PropertyObject => new Objects();
            internal Objects MethodObject() => new Objects();
        }

        class IntObjectImplicit1
        {
            public static implicit operator int(IntObjectImplicit1 src) => 1;
        }
        class IntObjectImplicit2
        {
            public static implicit operator IntObjectImplicit1(IntObjectImplicit2 src) => new IntObjectImplicit1();
        }

        class IntObjectExplicit1
        {
            public static explicit operator int(IntObjectExplicit1 src) => 1;
        }

        class IntObjectExplicit2
        {
            public static explicit operator IntObjectExplicit1(IntObjectExplicit2 src) => new IntObjectExplicit1();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Instance()
        {
            var condition = new ValuesInstance();

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(condition.field == 1 || condition.Property == 20 || condition.Method() == 30));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (((@field) = (@p_0)) OR ((@Property) = (@p_1))) OR ((@p_2) = (@p_3))",
new Params()
{
    { "@field", 1 },
    { "@Property", 2 },
    { "@p_0", 1 },
    { "@p_1", 20 },
    { "@p_2", 3 },
    { "@p_3", 30 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Static()
        {
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(ValuesStatic.field == 1 || ValuesStatic.Property == 20 || ValuesStatic.Method() == 30));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (((@field) = (@p_0)) OR ((@Property) = (@p_1))) OR ((@p_2) = (@p_3))",
new Params()
{
    { "@field", 1 },
    { "@Property", 2 },
    { "@p_0", 1 },
    { "@p_1", 20 },
    { "@p_2", 3 },
    { "@p_3", 30 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator1()
        {
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == new IntObjectImplicit1()));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator2()
        {
            var obj = new IntObjectImplicit1();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (IntObjectImplicit1)obj));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator3()
        {
            var obj = new IntObjectImplicit2();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (IntObjectImplicit1)obj));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator4()
        {
            var obj = new IntObjectImplicit1();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (obj as IntObjectImplicit1)));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ExplicitConvertOperator1()
        {
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)new IntObjectExplicit1()));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ExplicitConvertOperator2()
        {
            var obj = new IntObjectExplicit1();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)(IntObjectExplicit1)obj));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ExplicitConvertOperator3()
        {
            var obj = new IntObjectExplicit2();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)(IntObjectExplicit1)(IntObjectExplicit2)obj));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast1()
        {
            long val1 = 1;
            int val2 = 1;
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(val1 == (long)(short)val2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@val1) = (@val2)", new Params { { "@val1", (long)1 }, { "@val2", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast2()
        {
            int val1 = 6;
            long val2 = 1;
            var obj1 = new IntObjectExplicit2();
            var obj2 = new IntObjectImplicit2();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(val1 == (int)(short)val2 + 3 + (IntObjectImplicit1)obj2 + (int)(IntObjectExplicit1)obj1));


            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@val1) = ((((@val2) + (@p_0)) + (@obj2)) + (@obj1))",
new Params
{
    { "@val1", 6 },
    { "@val2", (long)1 },
    { "@obj2", 1 },
    { "@obj1", 1 },
    { "@p_0", 3 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_New()
        {
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(new ValuesInstance().field == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@field) = (@p_0)", new Params { { "@field", 1 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Field()
        {
            var obj = new Objects();
            obj.fieldObject = new Objects();
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.fieldObject.fieldValue.field == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@field) = (@p_0)", new Params { { "@field", 1 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Property()
        {
            var obj = new Objects();

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.PropertyObject.PropertyValue.Property != 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
            @"SELECT *
FROM tbl_staff
WHERE (@Property) <> (@p_0)", new Params { { "@Property", 2 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Method()
        {
            var obj = new Objects();

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.MethodObject().MethodValue().Method() != 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) <> (@p_1)", 3, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNull1()
        {
            var query = Db<DB>.Sql(db => (string)null);
            AssertEx.AreEqual(query, _connection, @"@p_0", new Params { { @"@p_0", null } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNull2()
        {
            string val = null;
            var query = Db<DB>.Sql(db => val);
            AssertEx.AreEqual(query, _connection, @"@val", new Params { { @"@val", null } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_New_Object()
        {
            var query = Db<Data>.Sql(db => new DateTime(1999, 1, 1));
            AssertEx.AreEqual(query, _connection, @"@p_0", new DateTime(1999, 1, 1));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NestMethods_Static()
        {
            var query = Db<Data>.Sql(db => this.GetValue(TestExpressionEx.GetValue()));
            AssertEx.AreEqual(query, _connection, @"@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NestMethods_Instance()
        {
            var instance = new ValuesInstance();
            var query = Db<Data>.Sql(db => instance.GetValue(instance.GetValue()));
            AssertEx.AreEqual(query, _connection, @"@p_0", 1);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Exp()
        {
            var condition = new ValuesInstance();

            var exp1 = Db<DB>.Sql(db => condition.field == 1 || condition.Property == 20 || condition.Method() == 30);
            int a = 1;
            var exp2 = Db<DB>.Sql(db => exp1 && a == 1);
            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(exp2));
            
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE ((((@field) = (@p_0)) OR ((@Property) = (@p_1))) OR ((@p_2) = (@p_3))) AND ((@a) = (@p_4))",
new Params()
{
    { "@field", 1 },
    { "@Property", 2 },
    { "@p_0", 1 },
    { "@p_1", 20 },
    { "@p_2", 3 },
    { "@p_3", 30 },
    { "@a", 1 },
    { "@p_4", 1 },
});
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ArrayLength()
        {
            var x = new[] { 1, 2, 3 };
            var query = Db<Data>.Sql(db => x.Length);
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ArrayIndex()
        {
            var x = new[] { 1, 2, 3 };
            var query = Db<Data>.Sql(db => x[0]);
            query.Gen(_connection);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ListCount()
        {
            var x = new List<int> { 1, 2, 3 };
            var query = Db<Data>.Sql(db => x.Count);
            AssertEx.AreEqual(query, _connection, @"@Count", new Params { { "@Count", 3 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ListIndex()
        {
            var x = new List<int> { 1, 2, 3 };
            var query = Db<Data>.Sql(db => x[0]);
            AssertEx.AreEqual(query, _connection, @"@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_SymbolMember()
        {
            var data = new
            {
                val = DateTimeElement.Day
            };

            var query = Db<Data>.Sql(db => data.val);
            AssertEx.AreEqual(query, _connection, @"DAY");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Static_Property()
        {
            var query = Db<Data>.Sql(db => DateTime.Now);
            var ret = query.Build(typeof(SqlConnection));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AddSQP()
        {
            var select = Db<DB>.Sql(db => Select(Asterisk()));
            var from = Db<DB>.Sql(db => From(db.tbl_staff));
            var query = Db<DB>.Sql(db => select + from);
            query.Gen(_connection);

        }
        public class SelectData
        {
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public static readonly Sql<SelectData> Select = Db<DB>.Sql(db =>
            Select(new SelectData
            {
                PaymentDate = db.tbl_remuneration.payment_date,
                Money = db.tbl_remuneration.money,
            }));

        public static readonly Sql From = Db<DB>.Sql(db =>
            From(db.tbl_remuneration));

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test()
        {
            var query = Select + From;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ParamChange()
        {
            decimal val = 0;
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Money = db.tbl_remuneration.money + val,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	(tbl_remuneration.money) + (@val) AS Money
FROM tbl_remuneration", new Params { { "@val", (decimal)0 } });

            sql = sql.ChangeParams(new Params { { nameof(val), 3 } });

            datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	(tbl_remuneration.money) + (@val) AS Money
FROM tbl_remuneration", new Params { { "@val", 3 } });

            if (_connection.GetType().Name == "OracleConnection") return;

            var info = sql.Build(_connection.GetType()).ChangeParams(new Params { { "@val", 10 } });

            datas = _connection.Query(info).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(info, _connection,
@"SELECT
	(tbl_remuneration.money) + (@val) AS Money
FROM tbl_remuneration", new Params { { "@val", 10 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Add()
        {
            var from = Db<DB>.Sql(db => From(db.tbl_remuneration));

             var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                })
                +
                from
                +
                Where(0 < db.tbl_remuneration.id)
             );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);

            query.Gen(_connection);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
WHERE (@p_0) < (tbl_remuneration.id)", 0);
        }

        public class SelectData2
        {
            public string Type { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Case1()
        {
            var whenThen = Db<DB>.Sql(db => When(db.tbl_staff.id == 3).Then("x"));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData2
                {
                    Type = Case<string>() +
                                whenThen +
                                When(db.tbl_staff.id == 4).Then("y") +
                                Else("z") +
                            End()
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0)
		THEN @p_1
		WHEN (tbl_staff.id) = (@p_2)
		THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff",
 3, "x", 4, "y", "z");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Add_Empty()
        {
            var empty = new Sql();
            var empty2 = new Sql<string>();
            var from = Db<DB>.Sql(db => From(db.tbl_remuneration));

            var query = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   PaymentDate = db.tbl_remuneration.payment_date,
                   Money = db.tbl_remuneration.money,
               })
               +
               from
               +
               empty
               +
               empty2
               +
               Where(0 < db.tbl_remuneration.id)
            );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);

            query.Gen(_connection);

            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
WHERE (@p_0) < (tbl_remuneration.id)", 0);
        }
        //TODO +演算

        //TODO ★足したものが正しくサブクエリの時に括弧がつくか！

        //TODO ExpressionToObjectがテストされるだけのテストを書くこと
        //new 引数あり + 初期化

        //TODO is null で両方パラメータの場合・・・、無益やけど

        //TODO is null でnullに括弧つけまくりとか

        //TODO BinaryExpressionのテスト
    }

    public static class TestExpressionEx
    {
        public static int GetValue(this TestExpression exp, int value) => value;
        public static int GetValue() => 1;
    }


    //TODO とくにかく、全部のメソッドを一回は実行する コードレンズで見ればいいか
}
