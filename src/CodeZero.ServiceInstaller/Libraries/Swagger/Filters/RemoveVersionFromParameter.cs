using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

public class RemoveVersionFromParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParameter = operation?.Parameters?
            .SingleOrDefault(p => p.Name == "version");

        if (versionParameter is not null)
            operation?.Parameters.Remove(versionParameter);
    }
}