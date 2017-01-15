using LambdicSql.ConverterServices.Inside.CodeParts;

namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// 
    /// </summary>
    public class AroundCode : Code
    {
        Code _core;
        string _front = string.Empty;
        string _back = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="core"></param>
        /// <param name="front"></param>
        /// <param name="back"></param>
        public AroundCode(Code core, string front, string back)
        {
            _core = core;
            _front = front;
            _back = back;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override bool IsEmpty => _core.IsEmpty && string.IsNullOrEmpty(_front) && string.IsNullOrEmpty(_back);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool IsSingleLine(BuildingContext context) => _core.IsSingleLine(context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTopLevel"></param>
        /// <param name="indent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override string ToString(bool isTopLevel, int indent, BuildingContext context)
        {
            var text = _core.ToString(isTopLevel, indent, context);
            
            int index = 0;
            for (index = 0; index < text.Length; index++)
            {
                switch (text[index])
                {
                    case ' ':
                    case '\t':
                        continue;
                    default:
                        break;
                }
                break;
            }

            return text.Substring(0, index) + _front + text.Substring(index) + _back;
        }
        
        //TODO 外側にチェックが行かないけど。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customizer"></param>
        /// <returns></returns>
        public override Code Customize(ICodeCustomizer customizer) => new AroundCode(_core.Customize(customizer), _front, _back);
    }
}
