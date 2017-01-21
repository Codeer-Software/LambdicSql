using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.ConverterServices.Inside
{
    class OperatorCode
    {
        internal static readonly ICode Equal = " = ".ToCode();
        internal static readonly ICode NotEqual = " <> ".ToCode();
        internal static readonly ICode LessThan = " < ".ToCode();
        internal static readonly ICode LessThanOrEqual = " <= ".ToCode();
        internal static readonly ICode GreaterThan = " > ".ToCode();
        internal static readonly ICode GreaterThanOrEqual = " >= ".ToCode();
        internal static readonly ICode Add = " + ".ToCode();
        internal static readonly ICode AddString = new StringAddOperatorCode();
        internal static readonly ICode Subtract = " - ".ToCode();
        internal static readonly ICode Multiply = " * ".ToCode();
        internal static readonly ICode Divide = " / ".ToCode();
        internal static readonly ICode Modulo = " % ".ToCode();
        internal static readonly ICode And = " AND ".ToCode();
        internal static readonly ICode AndAlso = " AND ".ToCode();
        internal static readonly ICode Or = " OR ".ToCode();
        internal static readonly ICode OrElse = " OR ".ToCode();
    }
}
