using LambdicSql.Inside;
using LambdicSql.Clause.From;
using System;
using System.Linq;
using LambdicSql.Clause.Select;

namespace LambdicSql.QueryBase
{
    //TODO rename
    public class QueryDecoder
    {
        DbInfo _db;
        ExpressionDecoder _parser;

        public string ToString(IQueryInfo query)
        {
            return ToStringCore(query) + ";";
        }

        public virtual string CustomOperator(Type type1, string @operator, Type type2) => @operator;

        internal string ToStringCore(IQueryInfo query)
        {
            //TODO@@ init query info.

            _db = query.Db;
            _parser = new ExpressionDecoder(_db, this);
            return string.Join(Environment.NewLine, new[] {
                Adjust(query.Select).ToString(_parser),
                Adjust(query.From).ToString(_parser),
                query.Where?.ToString(_parser),
                query.GroupBy?.ToString(_parser),
                query.Having?.ToString(_parser),
                query.OrderBy?.ToString(_parser),
            }.Where(e => !string.IsNullOrEmpty(e)).ToArray());
        }


        //TODO delete spec.
        SelectClause Adjust(SelectClause select)
        {
            if (select != null)
            {
                return select;
            }
            select = new SelectClause();
            foreach (var e in _db.GetLambdaNameAndColumn())
            {
                select.Add(new SelectElement(e.Key, null));
            }
            return select;
        }

        FromClause Adjust(FromClause from)
        {
            if (from != null)
            {
                return from;
            }

            //table count must be 1.
            if (_db.GetLambdaNameAndTable().Count != 1)
            {
                throw new NotSupportedException();
            }
            return new FromClause(_db.GetLambdaNameAndTable().First().Value.SqlFullName);
        }
    }
}
