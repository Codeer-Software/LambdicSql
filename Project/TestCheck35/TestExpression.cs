using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using System;

namespace TestCheck35
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

        class IntObjectImplicit
        {
            public static implicit operator int(IntObjectImplicit src) => 1;
        }

        class IntObjectExplicit
        {
            public static explicit operator int(IntObjectExplicit src) => 1;
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Instance()
        {
            var condition = new ValuesInstance();

            var query = Sql<DB>.Create(db =>
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
            var query = Sql<DB>.Create(db =>
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
        public void Test_Cast()
        {
            long val1 = 1;
            int val2 = 1;
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(val1 == (long)(short)val2));

            var datas = _connection.Query(query).ToList();
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@val1) = (@val2)", new Params { { "@val1", (long)1 }, { "@val2", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator1()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == new IntObjectImplicit()));

            var datas = _connection.Query(query).ToList();
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ImplicitConvertOperator2()
        {
            var obj = new IntObjectImplicit();
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (IntObjectImplicit)obj));

            var datas = _connection.Query(query).ToList();
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { {"@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ExplicitConvertOperator1()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)new IntObjectExplicit()));
            var datas = _connection.Query(query).ToList();
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@p_1)", 1, 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_ExplicitConvertOperator2()
        {
            var obj = new IntObjectExplicit();
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)(IntObjectExplicit)obj));
            var datas = _connection.Query(query).ToList();
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@p_0) = (@obj)", new Params { { "@p_0", 1 }, { "@obj", 1 } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_New()
        {
            var query = Sql<DB>.Create(db =>
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
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.fieldObject.fieldValue.field == 1));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_staff
WHERE (@field) = (@p_0)", new Params { { "@field", 1}, { "@p_0", 1} });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Nest_Property()
        {
            var obj = new Objects();

            var query = Sql<DB>.Create(db =>
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

            var query = Sql<DB>.Create(db =>
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
            var query = Sql<DB>.Create(db => (string)null);
            AssertEx.AreEqual(query, _connection, @"@p_0", new Params { { @"@p_0", null } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void TestNull2()
        {
            string val = null;
            var query = Sql<DB>.Create(db => val);
            AssertEx.AreEqual(query, _connection, @"@val", new Params { { @"@val", null } });
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_New_Object()
        {
            var query = Sql<Data>.Create(db => new DateTime(1999, 1, 1));
            AssertEx.AreEqual(query, _connection, @"@p_0", new DateTime(1999, 1, 1));
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NestMethods_Static()
        {
            var query = Sql<Data>.Create(db => this.GetValue(TestExpressionEx.GetValue()));
            AssertEx.AreEqual(query, _connection, @"@p_0", 1);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_NestMethods_Instance()
        {
            var instance = new ValuesInstance();
            var query = Sql<Data>.Create(db => instance.GetValue(instance.GetValue()));
            AssertEx.AreEqual(query, _connection, @"@p_0", 1);
        }

        //TODO キャストを絡めた式はもう少し書いた方がいい
    }

    public static class TestExpressionEx
    {
        public static int GetValue(this TestExpression exp, int value) => value;
        public static int GetValue() => 1;
    }
}
