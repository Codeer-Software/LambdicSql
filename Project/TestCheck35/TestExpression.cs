using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

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
            internal Objects fieldObject = new Objects();
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

        //TODO Under consideration
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_ImplicitConvertOperator()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)new IntObjectImplicit()));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();

        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_ExplicitConvertOperator()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(1 == (int)new IntObjectExplicit()));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_New()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(new ValuesInstance().field == 1));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_Nest_Field()
        {
            var obj = new Objects();

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.fieldObject.fieldValue.field == 1));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_Nest_Property()
        {
            var obj = new Objects();

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.PropertyObject.PropertyValue.Property == 1));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        [Ignore]
        public void Test_Nest_Method()
        {
            var obj = new Objects();

            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(obj.MethodObject().MethodValue().Method() == 1));

            query.Gen(_connection);
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
        }
    }
}
