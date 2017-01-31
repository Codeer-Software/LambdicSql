namespace LambdicSql.ConverterServices.Inside
{
    class DbParamValueOnly : IDbParam
    {
        public object Value { get; set; }

        public IDbParam ChangeValue(object value) => new DbParamValueOnly { Value = value };
    }
}
