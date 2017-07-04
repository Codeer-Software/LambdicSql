using LambdicSql;
using LambdicSql.BuilderServices;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using Xunit;

namespace Test.NetCore
{
    public class BaseClassTest
    {
        class A
        {
            public int Id { get; set; }
        }

        class B : A
        {
            public string Text { get; set; }
        }

        class DB
        {
            public B B{ get; set; }
        }

        [ClauseStyleConverter]
        public static DataTypeElement NText() { throw new InvalitContextException(nameof(NText)); }

        [ClauseStyleConverter]
        public static DataTypeElement Int() { throw new InvalitContextException(nameof(Int)); }

        [MethodFormatConverter(Format = "CREATE TABLE [0](|[#$<,>1])", FormatDirection = FormatDirection.Vertical)]
        public static Clause<Non> CreateTable(object table, params TableDefinitionElement[] designer) { throw new InvalitContextException(nameof(CreateTable)); }

        [Fact]
        public void Test()
        {
            var text = Db<DB>.Sql(db => CreateTable(db.B, 
                new Column(db.B.Id, Int()),
                new Column(db.B.Text, NText()))).Build(new DialectOption()).Text;
        }
    }
}
