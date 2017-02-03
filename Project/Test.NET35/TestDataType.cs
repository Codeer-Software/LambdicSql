using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestDataType
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_SqlServer()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                 CreateTable(db.table3, 
                    new Column(db.table3.obj1, DataType.Bit()),
                    new Column(db.table3.obj2, DataType.Int()),
                    new Column(db.table3.obj3, DataType.SmallInt()),
                    new Column(db.table3.obj4, DataType.TinyInt()),
                    new Column(db.table3.obj5, DataType.Bit()),
                    new Column(db.table3.obj6, DataType.Decimal()),
                    new Column(db.table3.obj7, DataType.Numeric()),
                    new Column(db.table3.obj8, DataType.Float()),
                    new Column(db.table3.obj9, DataType.Real()),
                    new Column(db.table3.obj10, DataType.Money()),
                    new Column(db.table3.obj11, DataType.SmallMoney()),
                    new Column(db.table3.obj12, DataType.Date()),
                    new Column(db.table3.obj13, DataType.Time()),
                    new Column(db.table3.obj14, DataType.DateTime()),
                    new Column(db.table3.obj15, DataType.DateTime2()),
                    new Column(db.table3.obj16, DataType.SmallDateTime()),
                    new Column(db.table3.obj17, DataType.DateTimeOffset()),
                    new Column(db.table3.obj18, DataType.Char(1)),
                    new Column(db.table3.obj19, DataType.VarChar(1)),
                    new Column(db.table3.obj20, DataType.Text()),
                    new Column(db.table3.obj21, DataType.NChar(1)),
                    new Column(db.table3.obj22, DataType.NVarChar(1)),
                    new Column(db.table3.obj23, DataType.NText()),
                    new Column(db.table3.obj24, DataType.Binary()),
                    new Column(db.table3.obj25, DataType.VarBinary()),
                    new Column(db.table3.obj26, DataType.Image())
                 ));

            _connection.Execute(sql);
            AssertEx.AreEqual(sql, _connection,
@"CREATE TABLE table3(
	obj1 BIT,
	obj2 INT,
	obj3 SMALLINT,
	obj4 TINYINT,
	obj5 BIT,
	obj6 DECIMAL,
	obj7 NUMERIC,
	obj8 FLOAT,
	obj9 REAL,
	obj10 MONEY,
	obj11 SMALLMONEY,
	obj12 DATE,
	obj13 TIME,
	obj14 DATETIME,
	obj15 DATETIME2,
	obj16 SMALLDATETIME,
	obj17 DATETIMEOFFSET,
	obj18 CHAR(1),
	obj19 VARCHAR(1),
	obj20 TEXT,
	obj21 NCHAR(1),
	obj22 NVARCHAR(1),
	obj23 NTEXT,
	obj24 BINARY,
	obj25 VARBINARY,
	obj26 IMAGE)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_Oracle()
        {
            if (!_connection.IsTarget(TargetDB.Oracle)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                CreateTable(db.table3,
                    new Column(db.table3.obj1, DataType.VarChar2(1)),
                    new Column(db.table3.obj2, DataType.NVarChar2(1)),
                    new Column(db.table3.obj3, DataType.Char(1)),
                    new Column(db.table3.obj4, DataType.NChar(1)),
                    new Column(db.table3.obj5, DataType.Number(1, 2)),
                    new Column(db.table3.obj6, DataType.Binary_Float()),
                    new Column(db.table3.obj7, DataType.Binary_Double()),
                    new Column(db.table3.obj8, DataType.Date()),
                    new Column(db.table3.obj9, DataType.TimeStamp(1)),
                //    new Column(db.table3.obj10, DataType.Raw()),
                //    new Column(db.table3.obj11, DataType.LongRaw()),
                    new Column(db.table3.obj12, DataType.BFile()),
                    new Column(db.table3.obj13, DataType.Blob()),
                    new Column(db.table3.obj14, DataType.Long()),
                    new Column(db.table3.obj15, DataType.Clob()),
                    new Column(db.table3.obj16, DataType.NClob()),
                    new Column(db.table3.obj17, DataType.XmlType())
                ));

            _connection.Execute(sql);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_Postgre()
        {
            if (!_connection.IsTarget(TargetDB.Postgre)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                CreateTable(db.table3,
                    new Column(db.table3.obj1, DataType.BigInt()),
                    new Column(db.table3.obj2, DataType.BigSerial()),
                    new Column(db.table3.obj3, DataType.Bit(1)),
                    new Column(db.table3.obj4, DataType.BitVarying(1)),
                    new Column(db.table3.obj5, DataType.Boolean()),
                    new Column(db.table3.obj6, DataType.Box()),
                    new Column(db.table3.obj7, DataType.Bytea()),
                    new Column(db.table3.obj8, DataType.Character(1)),
                    new Column(db.table3.obj9, DataType.CharacterVarying(1)),
                    new Column(db.table3.obj10, DataType.Cidr()),
                    new Column(db.table3.obj11, DataType.Circle()),
                    new Column(db.table3.obj12, DataType.Date()),
                    new Column(db.table3.obj13, DataType.DoublePrecision()),
                    new Column(db.table3.obj14, DataType.Inet()),
                    new Column(db.table3.obj15, DataType.Integer()),
                 //   new Column(db.table3.obj16, DataType.Interval(IntervalType.Day, 1)),
                    new Column(db.table3.obj17, DataType.Json()),
                    new Column(db.table3.obj18, DataType.JsonB()),
                    new Column(db.table3.obj19, DataType.Line()),
                    new Column(db.table3.obj20, DataType.Lseg()),
                    new Column(db.table3.obj21, DataType.MacAddr()),
                    new Column(db.table3.obj22, DataType.Money()),
            //        new Column(db.table3.obj23, DataType.Numeric(1, 2)),
                    new Column(db.table3.obj24, DataType.Path()),
                    new Column(db.table3.obj25, DataType.Pg_Lsn()),
                    new Column(db.table3.obj26, DataType.Point()),
                    new Column(db.table3.obj27, DataType.Polygon()),
                    new Column(db.table3.obj28, DataType.Real()),
                    new Column(db.table3.obj29, DataType.SmallInt()),
                    new Column(db.table3.obj30, DataType.SmallSerial()),
                    new Column(db.table3.obj31, DataType.Serial()),
                    new Column(db.table3.obj32, DataType.Text()),
                    new Column(db.table3.obj33, DataType.Time()),
                    new Column(db.table3.obj34, DataType.Time(1)),
                    new Column(db.table3.obj35, DataType.TimeStamp()),
                    new Column(db.table3.obj36, DataType.TimeStamp(1)),
                    new Column(db.table3.obj37, DataType.TsQuery()),
                    new Column(db.table3.obj38, DataType.TsVector()),
                    new Column(db.table3.obj39, DataType.Txid_Snapshot()),
                    new Column(db.table3.obj40, DataType.Uuid()),
                    new Column(db.table3.obj41, DataType.Xml())
                ));
            //time zone

            _connection.Execute(sql);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_MySQL()
        {
            if (!_connection.IsTarget(TargetDB.MySQL)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                CreateTable(db.table3,
                    new Column(db.table3.obj1, DataType.TinyInt()),
                    new Column(db.table3.obj2, DataType.SmallInt()),
                    new Column(db.table3.obj3, DataType.MediumInt()),
                    new Column(db.table3.obj4, DataType.Int()),
                    new Column(db.table3.obj5, DataType.BigInt()),
                    new Column(db.table3.obj6, DataType.Float()),
                    new Column(db.table3.obj7, DataType.Double()),
                    new Column(db.table3.obj8, DataType.Decimal()),
                    new Column(db.table3.obj9, DataType.Char(1)),
                    new Column(db.table3.obj10, DataType.VarChar(1)),
                    new Column(db.table3.obj11, DataType.TinyBlob()),
                    new Column(db.table3.obj12, DataType.Blob()),
                    new Column(db.table3.obj13, DataType.MediumBlob()),
                    new Column(db.table3.obj14, DataType.LongBlob()),
                    new Column(db.table3.obj15, DataType.TinyText()),
                    new Column(db.table3.obj16, DataType.Text()),
                    new Column(db.table3.obj17, DataType.MediumText()),
                    new Column(db.table3.obj18, DataType.LongText()),
                    new Column(db.table3.obj19, DataType.Enum("a", "b")),
                    new Column(db.table3.obj20, DataType.Set("a", "b")),
                    new Column(db.table3.obj21, DataType.Date()),
                    new Column(db.table3.obj22, DataType.Time()),
                    new Column(db.table3.obj23, DataType.DateTime()),
                    new Column(db.table3.obj24, DataType.TimeStamp()),
                    new Column(db.table3.obj25, DataType.Year())
                ));

            _connection.Execute(sql);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_SQLite()
        {
            if (!_connection.IsTarget(TargetDB.SQLite)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                CreateTable(db.table3,
                    new Column(db.table3.obj1, DataType.Int()),
                    new Column(db.table3.obj2, DataType.Integer()),
                    new Column(db.table3.obj3, DataType.TinyInt()),
                    new Column(db.table3.obj4, DataType.SmallInt()),
                    new Column(db.table3.obj5, DataType.MediumInt()),
                    new Column(db.table3.obj6, DataType.BigInt()),
                    new Column(db.table3.obj7, DataType.UnsignedBigInt()),
                    new Column(db.table3.obj8, DataType.Int2()),
                    new Column(db.table3.obj9, DataType.Int8()),
                    new Column(db.table3.obj10, DataType.Character(1)),
                    new Column(db.table3.obj11, DataType.VarChar(1)),
                    new Column(db.table3.obj12, DataType.VaryingCharacter(1)),
                    new Column(db.table3.obj13, DataType.NChar(1)),
                    new Column(db.table3.obj14, DataType.NativeCharacter(1)),
                    new Column(db.table3.obj15, DataType.NVarChar()),
                    new Column(db.table3.obj16, DataType.Text()),
                    new Column(db.table3.obj17, DataType.Clob()),
                    new Column(db.table3.obj18, DataType.Blob()),
                    new Column(db.table3.obj19, DataType.Real()),
                    new Column(db.table3.obj20, DataType.Double()),
                    new Column(db.table3.obj21, DataType.DoublePrecision()),
                    new Column(db.table3.obj22, DataType.Float()),
                    new Column(db.table3.obj23, DataType.Numeric()),
                    new Column(db.table3.obj24, DataType.Decimal()),//(10, 5)
                    new Column(db.table3.obj25, DataType.Boolean()),
                    new Column(db.table3.obj26, DataType.Date()),
                    new Column(db.table3.obj27, DataType.DateTime())
                ));

            _connection.Execute(sql);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CreateTable_DB2()
        {
            if (!_connection.IsTarget(TargetDB.DB2)) return;

            CleanUpCreateDropTestTable();

            var sql = Db<DBForCreateTest>.Sql(db =>
                CreateTable(db.table3,
                    new Column(db.table3.obj1, DataType.BigInt()),
                    new Column(db.table3.obj2, DataType.Binary()),
                    new Column(db.table3.obj3, DataType.Blob()),
                    new Column(db.table3.obj4, DataType.Char(1)),
                    new Column(db.table3.obj5, DataType.Clob()),
           //         new Column(db.table3.obj6, DataType.Currency()),
                    new Column(db.table3.obj7, DataType.Date()),
            //        new Column(db.table3.obj8, DataType.DateTime()),
                    new Column(db.table3.obj9, DataType.DbClob()),
                    new Column(db.table3.obj10, DataType.Double()),
                    new Column(db.table3.obj11, DataType.Float()),
                    new Column(db.table3.obj12, DataType.Graphic()),
                    new Column(db.table3.obj13, DataType.Int()),
                    new Column(db.table3.obj14, DataType.Integer()),
                //    new Column(db.table3.obj15, DataType.LongVarchar(1)),
               //     new Column(db.table3.obj16, DataType.LongVarGraphic()),
                    new Column(db.table3.obj17, DataType.Numeric()),
                    new Column(db.table3.obj18, DataType.Real()),
                    new Column(db.table3.obj19, DataType.SmallInt()),
             //       new Column(db.table3.obj20, DataType.Text()),
                    new Column(db.table3.obj21, DataType.Time()),
                    new Column(db.table3.obj22, DataType.TimeStamp()),
                    new Column(db.table3.obj23, DataType.VarChar(1))
               //     new Column(db.table3.obj24, DataType.VarGraphic())
                ));

            _connection.Execute(sql);
        }

        void CleanUpCreateDropTestTable()
        {
            try
            {
                var sql = Db<DBForCreateTest>.Sql(db => DropTable(db.table3));
                _connection.Execute(sql);
            }
            catch { }
        }
    }
}
