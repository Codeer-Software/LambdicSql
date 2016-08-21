using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using TestCore;
using static Test.Helper.DBProviderInfo;

namespace Test
{
    [TestClass]
    public class SamplesWrap
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;
        Samples _core;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext.DataRow[0]);
            _connection.Open();
            _core = new Samples();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Check() => _core.Check();

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void StandardNoramlType() => _core.TestStandard();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectFrom() => _core.TestSelectFrom();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void GroupBy() => _core.TestGroupBy();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void GroupByPredicateDistinct() => _core.TestGroupByPredicateDistinct();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void GroupByPredicateAll() => _core.TestGroupByPredicateAll();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Having() => _core.TestHaving();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Like() => _core.TestLike();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void In() => _core.TestIn();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Between() => _core.TestBetween();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Exists() => _core.TestExists();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectPredicateDistinct() => _core.TestSelectPredicateDistinct();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectPredicateAll() => _core.TestSelectPredicateAll();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Delete() => _core.TestDelete();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void DeleteWhere() => _core.TestDeleteWhere();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Insert() => _core.TestInsert();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Update() => _core.TestUpdate();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void UpdateUsingTableValue() => _core.TestUpdateUsingTableValue();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void IsNull() => _core.TestIsNull();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void IsNotNull() => _core.TestIsNotNull();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Nullable() => _core.TestNullable();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void StringCalc() => _core.TestStringCalc();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void WhereEx() => _core.TestWhereEx();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Case1() => _core.TestCase1();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Case2() => _core.TestCase2();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void QueryConcat() => _core.TestQueryConcat();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SqlExtension() => _core.TestSqlExpression();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void WhereInSubQuery() => _core.TestSubQueryAtWhere();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectSubQuery() => _core.TestSubQuerySelect();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void FromSubQuery() => _core.TestSubQueryAtFrom();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void FormatText() => _core.TestFormatText();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Format2WaySql() => _core.TestFormat2WaySql();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Window() => _core.TestWindow();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void CheckOperatior() => _core.CheckOperatior();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void FromMany() => _core.FromMany();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Funcs() => _core.Funcs();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Union() => _core.TestUnion();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Intersect() => _core.TestIntersect();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Except() => _core.TestExcept();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Minus() => _core.TestMinus();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void GroupByEx() => _core.TestGroupByEx();
    }
}
