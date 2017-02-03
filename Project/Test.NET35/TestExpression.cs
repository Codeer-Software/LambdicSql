using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

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
        public void Test_Object_Instance()
        {
            var instance = new ValuesInstance();

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(instance.field == 1 || instance.Property == 20 || instance.Method() == 30));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Object_Static()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(ValuesStatic.field == 1 || ValuesStatic.Property == 20 || ValuesStatic.Method() == 30));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_Object_Static_Property()
        {
            var sql = Db<Data>.Sql(db => DateTime.Now);
            var ret = sql.Build(typeof(SqlConnection));
            Assert.AreEqual(ret.Text, "@Now");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_New()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(new ValuesInstance().field == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@field) = (@p_0)", new Params { { "@field", 1 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_Nest_Field()
        {
            var obj = new Objects();
            obj.fieldObject = new Objects();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.fieldObject.fieldValue.field == 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@field) = (@p_0)", new Params { { "@field", 1 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_Nest_Property()
        {
            var obj = new Objects();

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.PropertyObject.PropertyValue.Property != 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
            @"SELECT *
FROM tbl_staff
WHERE (@Property) <> (@p_0)", new Params { { "@Property", 2 }, { "@p_0", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_Nest_Method()
        {
            var obj = new Objects();

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.MethodObject().MethodValue().Method() != 1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) <> (@p_1)", 3, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_Null_1()
        {
            var sql = Db<DB>.Sql(db => (string)null);
            AssertEx.AreEqual(sql, _connection, @"@p_0", new Params { { @"@p_0", null } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Object_Null_2()
        {
            string val = null;
            var sql = Db<DB>.Sql(db => val);
            AssertEx.AreEqual(sql, _connection, @"@val", new Params { { @"@val", null } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ObjectNestMethods_Static()
        {
            var sql = Db<Data>.Sql(db => this.GetValue(TestExpressionEx.GetValue()));
            AssertEx.AreEqual(sql, _connection, @"@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ObjectNestMethods_Instance()
        {
            var instance = new ValuesInstance();
            var sql = Db<Data>.Sql(db => instance.GetValue(instance.GetValue()));
            AssertEx.AreEqual(sql, _connection, @"@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_Normal()
        {
            long val1 = 1;
            int val2 = 1;
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(val1 == (long)(short)val2));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@val1) = (@val2)", new Params { { "@val1", (long)1 }, { "@val2", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ImplicitConvertOperator_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == new IntObjectImplicit1()));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ImplicitConvertOperator_2()
        {
            var obj = new IntObjectImplicit1();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (IntObjectImplicit1)obj));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ImplicitConvertOperator_3()
        {
            var obj = new IntObjectImplicit2();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (IntObjectImplicit1)obj));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ImplicitConvertOperator_4()
        {
            var obj = new IntObjectImplicit1();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (obj as IntObjectImplicit1)));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ExplicitConvertOperator_1()
        {
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)new IntObjectExplicit1()));
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ExplicitConvertOperator_2()
        {
            var obj = new IntObjectExplicit1();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)(IntObjectExplicit1)obj));
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_ExplicitConvertOperator_3()
        {
            var obj = new IntObjectExplicit2();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)(IntObjectExplicit1)(IntObjectExplicit2)obj));
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Cast_Mix()
        {
            int val1 = 6;
            long val2 = 1;
            var obj1 = new IntObjectExplicit2();
            var obj2 = new IntObjectImplicit2();
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(val1 == (int)(short)val2 + 3 + (IntObjectImplicit1)obj2 + (int)(IntObjectExplicit1)obj1));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            var sql = Db<Data>.Sql(db => new DateTime(1999, 1, 1));
            AssertEx.AreEqual(sql, _connection, @"@p_0", new DateTime(1999, 1, 1));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Expression()
        {
            var condition = new ValuesInstance();

            var exp1 = Db<DB>.Sql(db => condition.field == 1 || condition.Property == 20 || condition.Method() == 30);
            int a = 1;
            var exp2 = Db<DB>.Sql(db => exp1 && a == 1);
            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(exp2));
            
            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
            var sql = Db<Data>.Sql(db => x.Length);
            AssertEx.AreEqual(sql, _connection, "@p_0", 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ArrayIndex()
        {
            var x = new[] { 1, 2, 3 };
            var sql = Db<Data>.Sql(db => x[0]);
            AssertEx.AreEqual(sql, _connection, "@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ListCount()
        {
            var x = new List<int> { 1, 2, 3 };
            var sql = Db<Data>.Sql(db => x.Count);
            AssertEx.AreEqual(sql, _connection, "@Count", new Params { { "@Count", 3 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ListIndex()
        {
            var x = new List<int> { 1, 2, 3 };
            var sql = Db<Data>.Sql(db => x[0]);
            AssertEx.AreEqual(sql, _connection, "@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_SymbolMember()
        {
            var data = new
            {
                val = DateTimeElement.Day
            };
            var sql = Db<Data>.Sql(db => data.val);
            AssertEx.AreEqual(sql, _connection, "DAY");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Additional_Operator()
        {
            var from = Db<DB>.Sql(db => From(db.tbl_remuneration));

            var sql = Db<DB>.Sql(db =>
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

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
WHERE (@p_0) < (tbl_remuneration.id)", 0);
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
        public void Test_Static_Add()
        {
            var sql = Select + From;

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        public class SelectData2
        {
            public string Type { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Case()
        {
            var whenThen = Db<DB>.Sql(db => When(db.tbl_staff.id == 3).Then("x"));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData2
                {
                    Type = Case<string>() +
                                whenThen +
                                When(db.tbl_staff.id == 4).Then("y") +
                                Else("z") +
                            End()
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
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
        public void Test_AdditionalOperator_Empty()
        {
            var empty = new Sql();
            var empty2 = new Sql<string>();
            var from = Db<DB>.Sql(db => From(db.tbl_remuneration));

            var sql = Db<DB>.Sql(db =>
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

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
WHERE (@p_0) < (tbl_remuneration.id)", 0);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_SubQuery_1()
        {
            var select = Db<DB>.Sql(db => Select(Sum(db.tbl_remuneration.money)));
            var where = Db<DB>.Sql(db => From(db.tbl_remuneration));
            var sub = select + where;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Money = sub
                }).
                From(db.tbl_remuneration));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Money
FROM tbl_remuneration");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_SubQuery_2()
        {
            var select = Db<DB>.Sql(db => Select(Sum(db.tbl_remuneration.money)));
            var where = Db<DB>.Sql(db => From(db.tbl_remuneration));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Money = select + where
                }).
                From(db.tbl_remuneration));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_SubQuery_3()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Money = Select(Sum(db.tbl_remuneration.money)) + From(db.tbl_remuneration)
                }).
                From(db.tbl_remuneration));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Union_1()
        {
            var sub1 = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union());

            var sub2 = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)));

            var sub3 = sub1 + sub2;

            var sub4 = Db<DB>.Sql(db => From(db.tbl_staff));

            var sql = sub3 + sub4;

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Union_2()
        {
            var sub1 = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)).From(db.tbl_staff));

            var sub2 = Db<DB>.Sql(db => Union());
            var sub3 = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)));

            var sub4 = sub2 + sub3;
            var sub5 = sub1 + sub4;

            var sub6 = Db<DB>.Sql(db => From(db.tbl_staff));

            var sql = sub5 + sub6;

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM tbl_staff
UNION
SELECT *
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Union_3()
        {
            var sub1 = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(db.tbl_staff).
                Union());

            var sub2 = Db<DB>.Sql(db => Select(Asterisk(db.tbl_staff)));
            var sub3 = sub1 + sub2;
            var sub4 = Db<DB>.Sql(db => From(db.tbl_staff));
            var sub5 = sub3 + sub4;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_staff)).From(sub5));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);

            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM
	(SELECT *
	FROM tbl_staff
	UNION
	SELECT *
	FROM tbl_staff) sub5");
        }



        public class SelectData3
        {
            public decimal Val { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Over()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData3
                {
                    Val = Sum(db.tbl_remuneration.money) + 
                            Over(PartitionBy(db.tbl_remuneration.payment_date),
                                OrderBy(Asc(db.tbl_remuneration.money)),
                                Rows(1, 5))
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	SUM(tbl_remuneration.money)
	OVER(
		PARTITION BY
			tbl_remuneration.payment_date
		ORDER BY
			tbl_remuneration.money ASC
		ROWS BETWEEN 1 PRECEDING AND 5 FOLLOWING) AS Val
FROM tbl_remuneration");
        }

        public class Table1
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public char[] val2 { get; set; }
        }
        public class Table2
        {
            public int id { get; set; }
            public int table1Id { get; set; }
            public int val1 { get; set; }
        }

        public class DBForCreateTest
        {
            public Table1 table1 { get; set; }
            public Table2 table2 { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AdditionalOperator_Constraint()
        {
            try
            {
                _connection.Execute(Db<DBForCreateTest>.Sql(db => DropTable(db.table1)));
            }
            catch { }

            var sql = Db<DBForCreateTest>.Sql(db =>
                   CreateTable(db.table1,
                       new Column(db.table1.id, DataType.Int(), Default(10), NotNull(), PrimaryKey()),
                       new Column(db.table1.val1, DataType.Int()),
                       new Column(db.table1.val2, DataType.Char(10), Default("abc"), NotNull()),
                       Constraint("xxx") + Check(db.table1.id < 100),
                       Unique(db.table1.val2)
                   ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table1(
	id INT DEFAULT 10 NOT NULL PRIMARY KEY,
	val1 INT,
	val2 CHAR(10) DEFAULT 'abc' NOT NULL,
	CONSTRAINT xxx
	CHECK((id) < (100)),
	UNIQUE(val2))");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Aliase()
        {
            var a = Db<DB>.Sql(db => db.tbl_remuneration);

            var sql = Db<DB>.Sql(db =>
               Select(new SelectData
               {
                   PaymentDate = a.Body.payment_date,
                   Money = a.Body.money,
               }).
               From(a).
               Where(0 < a.Body.id)
            );

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	a.payment_date AS PaymentDate,
	a.money AS Money
FROM tbl_remuneration a
WHERE (@p_0) < (a.id)", 0);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_IsEmpty()
        {
            var empty1 = new Sql();
            var empty2 = new Sql<string>();
            var from = Db<DB>.Sql(db => From(db.tbl_remuneration));
            Assert.IsTrue(empty1.IsEmpty);
            Assert.IsTrue(empty2.IsEmpty);
            Assert.IsFalse(from.IsEmpty);
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

        //TODO new 引数あり + 初期化
        //TODO タイプを全種類網羅しておくか・・・
        //TODO ExpressionToObjectがテストされるだけのテストを書くこと
        //TODO BinaryExpressionのテスト
    }

    public static class TestExpressionEx
    {
        public static int GetValue(this TestExpression exp, int value) => value;
        public static int GetValue() => 1;
    }
}
