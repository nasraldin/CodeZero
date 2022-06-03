using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

// Inform the AutoRest tool how enums should be modelled when it generates the API client.
// (see https://github.com/Azure/autorest/blob/master/docs/extensions/readme.md#x-ms-enum)
public class AutoRestSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (type.IsEnum)
        {
            schema.Extensions.Add(
                "x-ms-enum",
                new OpenApiObject
                {
                    ["name"] = new OpenApiString(type.Name),
                    ["modelAsString"] = new OpenApiBoolean(true)
                }
            );
        }
    }
}