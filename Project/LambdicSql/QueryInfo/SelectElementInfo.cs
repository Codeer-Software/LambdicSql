using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    //TODO change to polymorphism code.
    public class SelectElementInfo
    {
        public bool IsDbColumn => !string.IsNullOrEmpty(DbColumn);
        public string DbColumn { get; private set; }
        public string Function { get; private set; }
        public IEnumerable<object> Arguments { get; private set; }

        SelectElementInfo() { }

        public static SelectElementInfo DbColumnElement(string dbColumn)
            => new SelectElementInfo() { DbColumn = dbColumn };

        public static SelectElementInfo FunctionElement(string function, IEnumerable<object> arguments)
            => new SelectElementInfo() { Function = function, Arguments = arguments };
    }
}
