using LambdicSql.Inside;
using System.Linq;
using System;

namespace LambdicSql.SqlBuilder.ExpressionElements.Inside
{
    class ParameterText : ExpressionElement
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;

        DbParam _param;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _displayValue;

        internal ParameterText(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParam() {Value = value };
        }

        internal ParameterText(string name = null, MetaId metaId = null, DbParam param = null)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterText(string name, MetaId metaId, DbParam param, string front, string back, bool displayValue)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
            _front = front;
            _back = back;
            _displayValue = displayValue;
        }

        public override bool IsSingleLine(ExpressionConvertingContext context) => true;

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + GetDisplayText(context) + _back;

        public override ExpressionElement ConcatAround(string front, string back)
            => new ParameterText(Name, MetaId, _param, front + _front, _back + back, _displayValue);

        public override ExpressionElement ConcatToFront(string front)
            => new ParameterText(Name, MetaId, _param, front + _front, _back, _displayValue);

        public override ExpressionElement ConcatToBack(string back)
            => new ParameterText(Name, MetaId, _param, _front, _back + back, _displayValue);

        public override ExpressionElement Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);

        internal ExpressionElement ToDisplayValue() =>
            new ParameterText(Name, MetaId, _param, _front, _back, true);

        string GetDisplayText(ExpressionConvertingContext context)
        {
            return _displayValue ? Value.ToString() : context.ParameterInfo.Push(_param.Value, Name, MetaId, _param);
        }


    }
}
