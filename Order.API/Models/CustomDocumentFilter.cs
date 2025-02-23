﻿using Microsoft.OpenApi.Models;
using Order.API.Models.Exception;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Order.API.Models
{
    public class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            ResponseClassType.GetReponseClassType().ForEach(x => context.SchemaGenerator.GenerateSchema(x, context.SchemaRepository));
            ResponseClassType.GetReponseClassType().ForEach(x => context.SchemaGenerator.GenerateSchema(typeof(ExceptionResponseModel), context.SchemaRepository));
        }
    }
}
