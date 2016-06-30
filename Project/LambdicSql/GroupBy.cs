using System.Linq.Expressions;
using System.Linq;
using System;
using LambdicSql.Inside;

namespace LambdicSql
{
    public class GroupByClause : IClause
    {
        Expression[] _elements;
        public Expression[] GetElements() => _elements.ToArray();

        public GroupByClause(Expression[] elements)
        {
            _elements = elements;
        }

        public string ToString(IExpressionDecoder decoder)
            => GetElements().Length == 0 ?
                string.Empty :
                "GROUP BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", GetElements().Select(e => decoder.ToString(e)).ToArray());

        public IClause Clone() => this;
    }

    public interface IQueryGroupBy<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }

    public static class GroupByExtensions
    {
        public static IQueryGroupBy<TDB, TSelect> GroupBy<TDB, TSelect>(this IQuery<TDB, TSelect> query, params Expression<Func<TDB, object>>[] targets)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.GroupBy = new GroupByClause(targets.Select(e => e.Body).ToArray()));
    }
}
