using System;

namespace LambdicSql
{
    //TODO やっぱり、普通に使える変数名のコンバーターアトリビュートとかにするか　あれ、それって今あるメンバーに関するConveter属性でいいんでは？

    /// <summary>
    /// Give members a name to be used for conversion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConvertingNameAttribute : Attribute
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
    }
}
