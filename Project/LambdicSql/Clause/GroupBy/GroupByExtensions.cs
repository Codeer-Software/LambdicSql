﻿using System.Linq.Expressions;
using System.Linq;
using System;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using LambdicSql.Clause.GroupBy;

namespace LambdicSql
{
    public static class GroupByExtensions
    {
        public static IQuery<TDB, TSelect> GroupBy<TDB, TSelect>(this IQuery<TDB, TSelect> query, params Expression<Func<TDB, object>>[] targets)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.GroupBy = new GroupByClause(targets.Select(e => e.Body).ToArray()));
    }
}