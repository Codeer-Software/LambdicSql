namespace LambdicSql.BuilderServices.CodeParts
{
    /// <summary>
    /// Enclose the central code.
    /// </summary>
    public class AroundCode : ICode
    {
        ICode _center;
        string _front = string.Empty;
        string _back = string.Empty;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="center">Central code.</param>
        /// <param name="front">Front code.</param>
        /// <param name="back">Back code.</param>
        public AroundCode(ICode center, string front, string back)
        {
            _center = center;
            _front = front;
            _back = back;
        }

        /// <summary>
        /// Is empty.
        /// </summary>
        public bool IsEmpty => _center.IsEmpty && string.IsNullOrEmpty(_front) && string.IsNullOrEmpty(_back);

        /// <summary>
        /// Is single line.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Is single line.</returns>
        public bool IsSingleLine(BuildingContext context) => _center.IsSingleLine(context);

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Text.</returns>
        public string ToString(BuildingContext context)
        {
            var conterText = _center.ToString(context);

            int index = FindNotEmptyIndex(conterText);

            if (index == 0)
            {
                return _front + conterText + _back;
            }
            return conterText.Substring(0, index) + _front + conterText.Substring(index) + _back;
        }

        /// <summary>
        /// Accept customizer.
        /// </summary>
        /// <param name="customizer">Customizer.</param>
        /// <returns>Destination.</returns>
        public ICode Accept(ICodeCustomizer customizer)
        {
            var dst = customizer.Visit(this);
            if (!ReferenceEquals(this, dst)) return dst;
            return new AroundCode(_center.Accept(customizer), _front, _back);
        }

        static int FindNotEmptyIndex(string conterText)
        {
            int index = 0;
            for (index = 0; index < conterText.Length; index++)
            {
                switch (conterText[index])
                {
                    case ' ':
                    case '\t':
                        continue;
                    default:
                        break;
                }
                break;
            }
            return index;
        }
    }
}
