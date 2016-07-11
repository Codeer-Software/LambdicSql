using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Performance
{
   // [TestClass]
    public class InitDB
    {
        [TestMethod]
        public void Init()
        {
            Sql.Query<DB>().Delete().From(db => db.TableValues).ToExecutor(TestEnvironment.Adapter).Write();

            for (int i = 0; i < 10000; i += 100)
            {
                var values = Enumerable.Range(i, 100).Select(e => new TableValues()
                {
                    IntVal = e,
                    FloatVal = e,
                    DoubleVal = e,
                    DecimalVal = e,
                    StringVal = e.ToString()
                });
                Sql.Query<DB>().InsertInto(db => db.TableValues).Values(values).ToExecutor(TestEnvironment.Adapter).Write();
            }
        }
    }
}
