﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using LambdicSql.QueryBase;
using LambdicSql.Inside;

namespace LambdicSql.Clause.InsertInto
{
    public class InsertIntoClause<TDB, TTable> : IClause
            where TDB : class
            where TTable : class
    {
        public Func<TDB, TTable> GetTableFunc { get; }
        Expression<Func<TDB, TTable>> _getTable;
        Expression<Func<TTable, object>>[] _getElements;
        IEnumerable<TTable> _values;

        class NameAndGetValue
        {
            public Func<TTable, object> GetValue { get; set; }
            public string Name { get; set; }
        }

        public InsertIntoClause(Expression<Func<TDB, TTable>> getTable, Expression<Func<TTable, object>>[] getElements)
        {
            _getTable = getTable;
            _getElements = getElements;
            GetTableFunc = Expression.Lambda<Func<TDB, TTable>>(getTable.Body, getTable.Parameters).Compile();
        }
        
        public IClause Clone()
        {
            var clone = new InsertIntoClause<TDB, TTable>(_getTable, _getElements);
            clone._values = _values;
            return clone;
        }

        public string ToString(ISqlStringConverter decoder)
        {
            if (_values == null)
            {
                return string.Empty;
            }

            NameAndGetValue[] cols = null;
            if (_getElements.Length == 0)
            {
                cols = typeof(TTable).GetProperties().Select(e =>
                {
                    var tbl = Expression.Parameter(typeof(TTable), "tbl");
                    return new NameAndGetValue()
                    {
                        Name = e.Name,
                        GetValue = Expression.Lambda<Func<TTable, object>>(Expression.Convert(Expression.Property(tbl, e), typeof(object)), new[] { tbl }).Compile()
                    };
                }).ToArray();
            }
            else
            {
                cols = _getElements.Select(e => new NameAndGetValue()
                {
                    Name = decoder.ToString(e.Body),
                    GetValue = Expression.Lambda<Func<TTable, object>>(e.Body, e.Parameters).Compile()
                }).ToArray();
            }

            var db = decoder.ToString(_getTable.Body);
            var query = new List<string>();
            var insert = "INSERT INTO " + db + " (" + string.Join(", ", cols.Select(e => e.Name).ToArray()) + ")";
            foreach (var val in _values)
            {
                var values = "VALUES(" + string.Join(", ", cols.Select(col=> decoder.ToString(col.GetValue(val))).ToArray()) + ")";
                query.Add(insert);
                query.Add(values);
            }
            return string.Join(Environment.NewLine, query.ToArray());
        }

        internal void Values(IEnumerable<TTable> values) => _values = values;
    }
}