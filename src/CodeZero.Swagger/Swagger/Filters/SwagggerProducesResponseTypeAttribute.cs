using System.Reflection.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

public class SwagggerProducesResponseTypeAttribute : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation is null || context is null || context.MethodInfo.DeclaringType is null)
            return;

        var authAttributes = context.MethodInfo.DeclaringType
            .GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<CustomAttribute>();

        if (authAttributes.Any())
        {
            operation.Responses.Add("400", new OpenApiResponse { Description = "Bad Request" });
            operation.Responses.Add("404", new OpenApiResponse { Description = "Request Not Found" });
            operation.Responses.Add("500", new OpenApiResponse { Description = "Internal Server Error" });
        }
    }
}