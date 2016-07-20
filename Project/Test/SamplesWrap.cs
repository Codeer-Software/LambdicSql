using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
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
            _connection = new SqlConnection(TestEnvironment.ConnectionString);
            _connection.Open();
            _core = new Samples();
            _core.TestInitialize(TestContext.TestName, _connection);
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void LambdaOnly() => _core.LambdaOnly();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void StandardNoramlType() => _core.StandardNoramlType();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectAll() => _core.SelectAll();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectFrom() => _core.SelectFrom();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void GroupBy() => _core.GroupBy();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Having() => _core.Having();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void WhereAndOr() => _core.WhereAndOr();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Like() => _core.Like();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void In() => _core.In();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Between() => _core.Between();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void WhereInSubQuery() => _core.WhereInSubQuery();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void SelectSubQuery() => _core.SelectSubQuery();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void FromSubQuery() => _core.FromSubQuery();
        
        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Distinct() => _core.Distinct();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Delete() => _core.Delete();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void DeleteWhere() => _core.DeleteWhere();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Insert() => _core.Insert();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void InsertSelectedData() => _core.InsertSelectedData();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void InsertUsingAnonymousType() => _core.InsertUsingAnonymousType();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Update() => _core.Update();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void UpdateUsingTableValue() => _core.UpdateUsingTableValue();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void IsNull() => _core.IsNull();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void IsNotNull() => _core.IsNotNull();

        [TestMethod, DataSource(Type, Connection, Sheet, Method)]
        public void Nullable() => _core.Nullable();
    }
}
