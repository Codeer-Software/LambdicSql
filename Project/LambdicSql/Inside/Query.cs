using LambdicSql.QueryInfo;
using System;

namespace LambdicSql.Inside
{
    class Query<TDB, TSelect> : IQueryStart<TDB, TSelect>, 
                                IQueryFrom<TDB, TSelect>,
                                IQueryWhere<TDB, TSelect>,
                                IQueryHaving<TDB, TSelect>,
                                IQueryOrderBy<TDB, TSelect>,
                                IQueryInfo<TSelect>
        where TDB : class
        where TSelect : class
    {
        public Func<IDbResult, TSelect> Create { get; set; }
        public DbInfo Db { get; set; }
        public SelectInfo Select { get; set; }
        public FromInfo From { get; set; }
        public ConditionClauseInfo Where { get; set; }
        public GroupByInfo GroupBy { get; set; }
        public ConditionClauseInfo Having { get; set; }
        public OrderByInfo OrderBy { get; set; }

        internal Query() { }

        internal Query<TDB, TSelect> Clone()
        {
            return new Query<TDB, TSelect>()
            {
                Create = Create,
                Db = Db,
                Select = Select,
                From = From == null ? null : From.Clone(),
                Where = Where == null ? null : Where.Clone(),
                GroupBy = GroupBy,
                Having = Having == null ? null : Having.Clone(),
                OrderBy = OrderBy == null ? null : OrderBy.Clone()
            };
        }

        internal Query<TDB, TSelectNew> ConvertType<TSelectNew>(Func<IDbResult, TSelectNew> define)
                where TSelectNew : class
        {
            return new Query<TDB, TSelectNew>()
            {
                Create = define,
                Db = Db,
                Select = Select,
                From = From == null ? null : From.Clone(),
                Where = Where == null ? null : Where.Clone(),
                GroupBy = GroupBy,
                Having = Having == null ? null : Having.Clone(),
                OrderBy = OrderBy == null ? null : OrderBy.Clone()
            };
        }
    }
}
