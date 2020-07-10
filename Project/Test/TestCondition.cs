using LambdicSql;
using LambdicSql.BuilderServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestCondition
    {
        class A
        {
            public int Id { get; set; }
        }

        class DB
        {
            public A A { get; set; }
        }

        [TestMethod]
        public void Test1()
        {
            var condition = new Sql<bool>();

            foreach (var e in new[] { 1, 2, 3 })
            {
                condition = Db<DB>.Sql(db => condition && db.A.Id == e);
            }
            var text = condition.Build(new DialectOption()).Text;
            var expected = @"((A.Id = @e) AND A.Id = @e_) AND A.Id = @e__";
            Assert.AreEqual(expected, text);
        }

        [TestMethod]
        public void Test2()
        {
            var condition = new Sql<bool>();

            foreach (var e in new[] { 1, 2, 3 })
            {
                var value = e;
                condition = Db<DB>.Sql(db => condition && db.A.Id == value.NoName());
            }
            var text = condition.Build(new DialectOption()).Text;
            var expected = @"((A.Id = @p_0) AND A.Id = @p_1) AND A.Id = @p_2";
            Assert.AreEqual(expected, text);
        }
    }

    internal static class XX
    {
        internal static T NoName<T>(this T value) => value;
    }
}
