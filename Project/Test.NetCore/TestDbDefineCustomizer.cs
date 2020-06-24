using LambdicSql;
using LambdicSql.BuilderServices;
using System;
using System.Text.RegularExpressions;
using Xunit;
using static Test.Symbol;

namespace Test
{
    public class TestDbDefineCustomizer
    {
        public class Staff
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Remuneration
        {
            public int id { get; set; }
            public int staff_id { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        public class DB1
        {
            public Staff TblStaff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
        }
        public class DB2
        {
            public Staff TblStaff { get; set; }
        }

        [Fact]
        public void Test1()
        {
            Db<DB1>.DefinitionCustomizer = p => null;

            var sql = Db<DB1>.Sql(db =>
                Select(new
                {
                    name = db.TblStaff.Name,
                }).
                From(db.TblStaff));

            var txt = sql.Build(new DialectOption()).Text;
            var expected = @"SELECT
	TblStaff.Name AS name
FROM TblStaff";
            Assert.Equal(expected, txt);
        }

        [Fact]
        public void Test2()
        {
            Db<DB2>.DefinitionCustomizer = p =>
            {
                var input = p.Name;
                if (string.IsNullOrEmpty(input)) { return input; }
                var startUnderscores = Regex.Match(input, @"^_+");
                return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            };

            var sql = Db<DB2>.Sql(db =>
                Select(new
                {
                    name = db.TblStaff.Name,
                }).
                From(db.TblStaff));

            var txt = sql.Build(new DialectOption()).Text;
            var expected = @"SELECT
	tbl_staff.Name AS name
FROM tbl_staff";
            Assert.Equal(expected, txt);
        }

    }
}
