﻿namespace LambdicSql.BuilderServices.Code.Inside
{
    class CustomizeParameterToObject : IPartsCustomizer
    {
        public Parts Custom(Parts src)
        {
            var param = src as ParameterParts;
            return param == null ? src : param.ToDisplayValue();
        }
    }
}
