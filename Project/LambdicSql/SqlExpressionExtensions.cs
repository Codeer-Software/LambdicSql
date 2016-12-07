using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// Enhancement of ISqlExpressionBase.
    /// </summary>
    public static class SqlExpressionExtensions
    {
        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpressionBase<IClauseChain<TSelected>> expression, Type connectionType)
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpressionBase)expression, connectionType));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpressionBase expression, Type connectionType)
            => ToSqlInfo(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpressionBase expression, SqlConvertOption option)
            => ToSqlInfo(expression, option, null);

        static SqlInfo ToSqlInfo(this ISqlExpressionBase exp, SqlConvertOption option, ISqlSyntaxCustomizer customizer)
        {
            var context = new SqlConvertingContext(exp.DbInfo, option.ParameterPrefix);
            var converter = new SqlStringConverter(context, option, customizer);
            var text = exp.ToString(converter);

            text = AdjustText(text);

            return new SqlInfo(exp.DbInfo, text, context.ObjectCreateInfo, context.Parameters);
        }

        //TODO
        static string AdjustText(string text)
        {
            //adjust. remove empty line.
            //ここはこだわりの世界やけど、括弧を前詰めにするか？
            var list = new List<string>();
            var insert = string.Empty;
            foreach (var l in text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var check = l.Trim();
                if (string.IsNullOrEmpty(check)) continue;
                check = insert + check;
                if (string.IsNullOrEmpty(check.Replace("(", string.Empty).Trim()))
                {
                    insert = check.Replace(" ", string.Empty).Replace("\t", string.Empty);
                    continue;
                }
                list.Add(InsertTextToTop(l, insert));
                insert = string.Empty;
            }
            
            text = string.Join(Environment.NewLine, list.ToArray());
            return text;
        }

        static string InsertTextToTop(string line, string insertText)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '\t')
                {
                    return line.Substring(0, i) + insertText + line.Substring(i);
                }
            }
            return insertText + line;
        }
    }
}
