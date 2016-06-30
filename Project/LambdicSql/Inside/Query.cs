using LambdicSql.QueryInfo;
using System;

namespace LambdicSql.Inside
{
    class Query<TDB, TSelect> : IQueryFrom<TDB, TSelect>,
                                IQueryWhere<TDB, TSelect>,
                                IQueryHaving<TDB, TSelect>,
                                IQueryGroupBy<TDB, TSelect>,
                                IQueryOrderBy<TDB, TSelect>,
                                IQueryInfo<TSelect>
        where TDB : class
        where TSelect : class
    {
        public Func<IDbResult, TSelect> Create { get; set; }
        public DbInfo Db { get; set; }
        public SelectClause Select { get; set; }
        public FromClause From { get; set; }
        public WhereClause Where { get; set; }
        public GroupByClause GroupBy { get; set; }
        public HavingClause Having { get; set; }
        public OrderByClause OrderBy { get; set; }

        internal Query() { }

        internal Query<TDB, TSelect> Clone()
        {
            return new Query<TDB, TSelect>()
            {
                Create = Create,
                Db = Db,
                Select = Select,
                From = From == null ? null : (FromClause)From.Clone(),
                Where = Where == null ? null : (WhereClause)Where.Clone(),
                GroupBy = GroupBy,
                Having = Having == null ? null : (HavingClause)Having.Clone(),
                OrderBy = OrderBy == null ? null : (OrderByClause)OrderBy.Clone()
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
                From = From == null ? null : (FromClause)From.Clone(),
                Where = Where == null ? null : (WhereClause)Where.Clone(),
                GroupBy = GroupBy,
                Having = Having == null ? null : (HavingClause)Having.Clone(),
                OrderBy = OrderBy == null ? null : (OrderByClause)OrderBy.Clone()
            };
        }
    }
}
