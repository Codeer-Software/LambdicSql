using LambdicSql.ConverterServices;
using LambdicSql.Specialized.SymbolConverters;

namespace LambdicSql
{
    /// <summary>
    /// Condition building helper.
    /// condition is used if enable is valid.
    /// It can only be used within lambda of the LambdicSql.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Condition building helper.
        /// condition is used if enable is valid.
        /// </summary>
        /// <param name="enable">Whether condition is valid.</param>
        /// <param name="condition">Condition expression.</param>
        [ConditionConverter]
        public Condition(object enable, object condition) { throw new InvalitContextException("new " + nameof(Condition)); }

        /// <summary>
        /// Implicit conversion to bool type.
        /// </summary>
        /// <param name="src">Condition</param>
        public static implicit operator bool(Condition src) { throw new InvalitContextException("implicit operator bool(Condition src)"); }
    }
}
