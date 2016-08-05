using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Test.Helper
{
  //  [TestClass]
    public class CodeGenerate
    {
        [TestMethod]
        public void CreateGetter()
        {
            string template = @"
            class GetterCore<[Ts]> : IGetter
            {
                public delegate object Func([DefArgs]);
                Func _func;
                public void Init(Expression exp, ParameterExpression[] param) => _func = Expression.Lambda<Func>(exp, param).Compile();
                public object GetMemberObject(object[] arguments) => _func([Args]);
            }";

            for (int i = 1; i < 31; i++)
            {
                var ts = string.Join(",", Enumerable.Range(0, i).Select(e => "T" + e));
                var defArgs = string.Join(",", Enumerable.Range(0, i).Select(e => "T" + e + " t" + e));
                var args = string.Join(",", Enumerable.Range(0, i).Select(e => "(T" + e + ")arguments[" + e + "]"));
                Debug.Print(template.Replace("[Ts]", ts).Replace("[Args]", args).Replace("[DefArgs]", defArgs));
            }
        }

        [TestMethod]
        public void CreateSwitch()
        {
            string template = @"case [Len]: return Activator.CreateInstance(typeof(GetterCore<[Count]>).MakeGenericType(args), true) as IGetter;";
            for (int i = 1; i < 30; i++)
            {
                var count = string.Empty;
                Enumerable.Range(0, i - 1).ToList().ForEach(e => count += ",");
                Debug.Print(template.Replace("[Len]", i.ToString()).Replace("[Count]", count));
            }
        }
    }
}
