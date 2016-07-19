using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using TestCore;

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
        public void TestCleanup()
        {
            _connection.Dispose();
        }

        [TestMethod]
        public void LambdaOnly() => _core.LambdaOnly();

        [TestMethod]
        public void StandardNoramlType() => _core.StandardNoramlType();
        
        [TestMethod]
        public void SelectAll() => _core.SelectAll();
        
        [TestMethod]
        public void SelectFrom() => _core.SelectFrom();
        
        [TestMethod]
        public void GroupBy() => _core.GroupBy();
        
        [TestMethod]
        public void Having() => _core.Having();
        
        [TestMethod]
        public void WhereAndOr() => _core.WhereAndOr();
        
        [TestMethod]
        public void Like() => _core.Like();

        [TestMethod]
        public void In() => _core.In();

        [TestMethod]
        public void Between() => _core.Between();
        
        [TestMethod]
        public void WhereInSubQuery() => _core.WhereInSubQuery();

        [TestMethod]
        public void SelectSubQuery() => _core.SelectSubQuery();

        [TestMethod]
        public void FromSubQuery() => _core.FromSubQuery();
        
        [TestMethod]
        public void Distinct() => _core.Distinct();

        [TestMethod]
        public void Delete() => _core.Delete();

        [TestMethod]
        public void DeleteWhere() => _core.DeleteWhere();

        [TestMethod]
        public void Insert() => _core.Insert();

        [TestMethod]
        public void InsertSelectedData() => _core.InsertSelectedData();

        [TestMethod]
        public void InsertUsingAnonymousType() => _core.InsertUsingAnonymousType();

        [TestMethod]
        public void Update() => _core.Update();

        [TestMethod]
        public void UpdateUsingTableValue() => _core.UpdateUsingTableValue();

        [TestMethod]
        public void IsNull() => _core.IsNull();

        [TestMethod]
        public void IsNotNull() => _core.IsNotNull();

        [TestMethod]
        public void Nullable() => _core.Nullable();

        /*@@@ TODO
        [TestMethod]
        public void LambdaOnlySelectFrom() => _core.LambdaOnlySelectFrom();*/
    }
}
