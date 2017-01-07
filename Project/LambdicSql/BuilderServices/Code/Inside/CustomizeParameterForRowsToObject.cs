﻿namespace LambdicSql.BuilderServices.Code.Inside
{
    class CustomizeParameterForRowsToObject : IPartsCustomizer
    {
        public Parts Custom(Parts src)
        {
            var param = src as ParameterParts;
            return param == null ? src : new RowsParameterParts(param);
        }
    }
}