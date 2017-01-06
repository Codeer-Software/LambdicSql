using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.BuilderServices.Code.Inside
{
    class ParameterParts : Parts
    {
        internal string Name { get; private set; }
        internal MetaId MetaId { get; private set; }
        internal object Value => _param.Value;

        DbParam _param;
        string _front = string.Empty;
        string _back = string.Empty;
        bool _displayValue;

        internal ParameterParts(object value)
        {
            Name = null;
            MetaId = null;
            _param = new DbParam() {Value = value };
        }

        internal ParameterParts(string name = null, MetaId metaId = null, DbParam param = null)
        {
            Name = name;
            MetaId = metaId;
            _param = param;
        }

        ParameterParts(string name, MetaId metaId, DbParam param, string front, string back, bool displayValue)
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

        public override string ToString(bool isTopLevel, int indent, BuildingContext context) => PartsUtils.GetIndent(indent) + _front + GetDisplayText(context) + _back;

        public override Parts ConcatAround(string front, string back) => new ParameterParts(Name, MetaId, _param, front + _front, _back + back, _displayValue);

        public override Parts ConcatToFront(string front) => new ParameterParts(Name, MetaId, _param, front + _front, _back, _displayValue);

        public override Parts ConcatToBack(string back) => new ParameterParts(Name, MetaId, _param, _front, _back + back, _displayValue);

        public override Parts Customize(IPartsCustomizer customizer) => customizer.Custom(this);

        internal Parts ToDisplayValue() => new ParameterParts(Name, MetaId, _param, _front, _back, true);

        string GetDisplayText(BuildingContext context) => _displayValue ? Value.ToString() : context.ParameterInfo.Push(_param.Value, Name, MetaId, _param);
    }
}
