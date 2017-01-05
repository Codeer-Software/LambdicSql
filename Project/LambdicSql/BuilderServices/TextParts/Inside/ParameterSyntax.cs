using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Syntaxes.Inside
{
    class ParameterSyntax : TextPartsBase
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;

        DbParam _param;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _displayValue;

        internal ParameterSyntax(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParam() {Value = value };
        }

        internal ParameterSyntax(string name = null, MetaId metaId = null, DbParam param = null)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterSyntax(string name, MetaId metaId, DbParam param, string front, string back, bool displayValue)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
            _front = front;
            _back = back;
            _displayValue = displayValue;
        }

        public override bool IsEmpty => false;

        public override bool IsSingleLine(BuildingContext context) => true;

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => SyntaxUtils.GetIndent(indent) + _front + GetDisplayText(context) + _back;

        public override TextPartsBase ConcatAround(string front, string back) => new ParameterSyntax(Name, MetaId, _param, front + _front, _back + back, _displayValue);

        public override TextPartsBase ConcatToFront(string front) => new ParameterSyntax(Name, MetaId, _param, front + _front, _back, _displayValue);

        public override TextPartsBase ConcatToBack(string back) => new ParameterSyntax(Name, MetaId, _param, _front, _back + back, _displayValue);

        public override TextPartsBase Customize(ISyntaxCustomizer customizer) => customizer.Custom(this);

        internal TextPartsBase ToDisplayValue() => new ParameterSyntax(Name, MetaId, _param, _front, _back, true);

        string GetDisplayText(BuildingContext context) => _displayValue ? Value.ToString() : context.ParameterInfo.Push(_param.Value, Name, MetaId, _param);
    }
}
