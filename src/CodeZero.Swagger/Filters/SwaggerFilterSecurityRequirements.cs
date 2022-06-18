using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

//public class SwaggerFilterSecurityRequirements : IOperationFilter
//{
//    public void Apply(OpenApiOperation operation, OperationFilterContext context)
//    {
//        if (operation is null || context is null)
//        {
//            return;
//        }

//        // Policy names map to scopes
//        IEnumerable<string> requiredScopes = context.MethodInfo
//            .GetCustomAttributes(true)
//            .OfType<AuthorizeAttribute>()
//            .Select(attr => attr.Policy)
//            .Distinct();

//        if (!requiredScopes.Any())
//        {
//            return;
//        }

//        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
//        operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

//        var oAuthScheme = new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
//        };

//        operation.Security = new List<OpenApiSecurityRequirement>
//        {
//            new OpenApiSecurityRequirement {[oAuthScheme] = requiredScopes.ToList()}
//        };
//    }
//}

/// <summary>
/// Applies Security Requirements
/// </summary>
public class SwaggerFilterSecurityRequirements : IOperationFilter
{
    /// <summary>
    /// Applies this filter on swagger documentation generation.
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation is null || context is null || context.MethodInfo.DeclaringType is null)
        {
            return;
        }

        // then check if there is a method-level 'AllowAnonymous',
        // as this overrides any controller-level 'Authorize'
        var anonControllerScope = context.MethodInfo
                .DeclaringType
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>();

        var anonMethodScope = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>();

        // Policy names map to scopes
        var requiredScopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy)
            .Distinct();

        if (!requiredScopes.Any())
        {
            return;
        }

        // only add authorization specification information if there is 
        // at least one 'Authorize' in the chain and NO method-level 'AllowAnonymous'
        if (!anonMethodScope.Any() && !anonControllerScope.Any())
        {
            // add generic message if the controller methods dont already specify the response type

            // If Authorization header not present, has no value or no valid jwt bearer token
            if (!operation.Responses.ContainsKey("401"))
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

            // If user not authorized to perform requested action
            if (!operation.Responses.ContainsKey("403"))
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            var jwtAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    //[ jwtAuthScheme ] = new List<string>()
                    [jwtAuthScheme] = requiredScopes.ToList()
                }
            };
        }
    }
}