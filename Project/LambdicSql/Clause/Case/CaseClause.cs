using System;
using System.Linq.Expressions;
using LambdicSql.QueryBase;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql.Clause.Case
{
    public class CaseClause : IExpressionClause
    {
        Expression _caseTarget;
        object _else;
        List<WhenThenElement> _whenThen = new List<WhenThenElement>();

        public CaseClause() { }
        protected CaseClause(CaseClause src)
        {
            _caseTarget = src._caseTarget;
            _else = src._else;
            _whenThen = src._whenThen.ToList();
        }

        public CaseClause(Expression caseTarget)
        {
            _caseTarget = caseTarget;
        }

        public virtual IClause Clone() => new CaseClause(this);

        public string ToString(ISqlStringConverter decoder)
        {
            var text = "CASE " + ((_caseTarget == null) ? string.Empty : decoder.ToString(_caseTarget));
            var whenThen = Environment.NewLine + "\t" + string.Join(Environment.NewLine + "\t", _whenThen.Select(e => "WHEN " + decoder.ToString(e.Condition) + " THEN " + decoder.ToString(e.Value)).ToArray());
            text += whenThen;
            if (_else != null) text += (Environment.NewLine + "\t" + "ELSE "+ decoder.ToString(_else));
            return text + Environment.NewLine + "END";
        }

        internal void WhenThen(object condition, object value)
        {
            _whenThen.Add(new WhenThenElement() { Condition = condition, Value = value });
        }

        internal void Else(object @else)
        {
            _else = @else;
        }
    }

    public class CaseClause<T> : CaseClause
    {
        public CaseClause(Expression caseTarget) : base(caseTarget) { }
        CaseClause(CaseClause src) : base(src) { }

        public override IClause Clone()
        {
            return new CaseClause<T>(this);
        }
    }
}
