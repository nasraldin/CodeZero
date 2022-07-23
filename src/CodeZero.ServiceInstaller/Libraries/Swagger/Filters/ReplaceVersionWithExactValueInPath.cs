using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

public class ReplaceVersionWithExactValueInPath : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc?.Paths != null)
        {
            var paths = new OpenApiPaths();

            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("{version}", swaggerDoc.Info.Version,
                    StringComparison.Ordinal), path.Value);
            }

            swaggerDoc.Paths = paths;
        }
    }
}