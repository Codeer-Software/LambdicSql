using System;

namespace LambdicSql
{
    public class Parameter
    {
        public object Value { get; }
        Parameter(object value) { Value = value; }
        public static implicit operator Parameter(string src) => new Parameter(src);
        public static implicit operator Parameter(bool src) => new Parameter(src);
        public static implicit operator Parameter(bool? src) => new Parameter(src);
        public static implicit operator Parameter(byte src) => new Parameter(src);
        public static implicit operator Parameter(byte? src) => new Parameter(src);
        public static implicit operator Parameter(short src) => new Parameter(src);
        public static implicit operator Parameter(short? src) => new Parameter(src);
        public static implicit operator Parameter(int src) => new Parameter(src);
        public static implicit operator Parameter(int? src) => new Parameter(src);
        public static implicit operator Parameter(long src) => new Parameter(src);
        public static implicit operator Parameter(long? src) => new Parameter(src);
        public static implicit operator Parameter(float src) => new Parameter(src);
        public static implicit operator Parameter(float? src) => new Parameter(src);
        public static implicit operator Parameter(double src) => new Parameter(src);
        public static implicit operator Parameter(double? src) => new Parameter(src);
        public static implicit operator Parameter(decimal src) => new Parameter(src);
        public static implicit operator Parameter(decimal? src) => new Parameter(src);
        public static implicit operator Parameter(DateTime src) => new Parameter(src);
        public static implicit operator Parameter(DateTime? src) => new Parameter(src);
        public static implicit operator string(Parameter src) => default(string);
        public static implicit operator bool(Parameter src) => default(bool);
        public static implicit operator bool? (Parameter src) => default(bool?);
        public static implicit operator byte(Parameter src) => default(byte);
        public static implicit operator byte? (Parameter src) => default(byte?);
        public static implicit operator short(Parameter src) => default(short);
        public static implicit operator short? (Parameter src) => default(short?);
        public static implicit operator int(Parameter src) => default(int);
        public static implicit operator int? (Parameter src) => default(int?);
        public static implicit operator long(Parameter src) => default(long);
        public static implicit operator long? (Parameter src) => default(long?);
        public static implicit operator float(Parameter src) => default(float);
        public static implicit operator float? (Parameter src) => default(float?);
        public static implicit operator double(Parameter src) => default(double);
        public static implicit operator double? (Parameter src) => default(double?);
        public static implicit operator decimal(Parameter src) => default(decimal);
        public static implicit operator decimal? (Parameter src) => default(decimal?);
        public static implicit operator DateTime(Parameter src) => default(DateTime);
        public static implicit operator DateTime? (Parameter src) => default(DateTime?);
    }

    public class Parameters
    {
        public Parameter _0 { get; set; }
        public Parameter _1 { get; set; }
        public Parameter _2 { get; set; }
        public Parameter _3 { get; set; }
        public Parameter _4 { get; set; }
        public Parameter _5 { get; set; }

        public object this[string name]
        {
            get
            {
                switch (name)
                {
                    case "_0": return _0.Value;
                    case "_1": return _1.Value;
                    case "_2": return _2.Value;
                    case "_3": return _3.Value;
                    case "_4": return _4.Value;
                    case "_5": return _5.Value;
                }
                throw new NotSupportedException();
            }
        }
    }
}
