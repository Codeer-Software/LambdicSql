using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class SelectElementInfoFunction : ISelectElementInfo
    {
        public string Function { get; set; }
        public IEnumerable<object> Arguments { get; set; }

        public SelectElementInfoFunction(string function, IEnumerable<object> arguments)
        {
            Function = function;
            Arguments = arguments;
        }
    }
}
