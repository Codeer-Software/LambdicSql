using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// It represents assignment. It is used in the Set clause.
    /// new Assign(db.tbl_staff.name, name) -> tbl_staff.name = "@name"
    /// </summary>
    [SqlSyntax]
    public class Assign
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rhs">Rvalue</param>
        /// <param name="lhs">Lvalue</param>
        public Assign(object rhs, object lhs) { InvalitContext.Throw("new " + nameof(Assign)); }
        
        static IText ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var src = converter.UsingColumnNameOnly;
            IText arg1 = null;
            try
            {
                converter.UsingColumnNameOnly = true;
                arg1 = converter.ToString(exp.Arguments[0]);
            }
            finally
            {
                converter.UsingColumnNameOnly = src;
            }
            return new HText(arg1, "=", converter.ToString(exp.Arguments[1])) { Separator = " " };
        }
    }
}
