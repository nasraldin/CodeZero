using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeZero.Swagger.Filters;

/// <summary>
/// Applies Security Requirements
/// </summary>
public class SecurityRequirements : IOperationFilter
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

        if (operation.Responses.ContainsKey(ErrorCode.BadRequest.Id.ToString()))
        {
            operation.Responses.Add(ErrorCode.BadRequest.Id.ToString(),
                new OpenApiResponse { Description = ErrorCode.BadRequest.Name });
        }
        if (operation.Responses.ContainsKey(ErrorCode.NotFound.Id.ToString()))
        {
            operation.Responses.Add(ErrorCode.NotFound.Id.ToString(),
                new OpenApiResponse { Description = ErrorCode.NotFound.Name.ToString() });
        }
        if (operation.Responses.ContainsKey(ErrorCode.InternalServerError.Id.ToString()))
        {
            operation.Responses.Add(ErrorCode.InternalServerError.Id.ToString(),
                new OpenApiResponse { Description = ErrorCode.InternalServerError.Name.ToString() });
        }

        // only add authorization specification information if there is 
        // at least one 'Authorize' in the chain and NO method-level 'AllowAnonymous'
        if (!anonMethodScope.Any() && !anonControllerScope.Any())
        {
            // add generic message if the controller methods dont already specify the response type

            // If Authorization header not present, has no value or no valid jwt bearer token
            if (!operation.Responses.ContainsKey(ErrorCode.Unauthorized.Id.ToString()))
            {
                operation.Responses.Add(ErrorCode.Unauthorized.Id.ToString(),
                    new OpenApiResponse { Description = ErrorCode.Unauthorized.Name.ToString() });
            }

            // If user not authorized to perform requested action
            if (!operation.Responses.ContainsKey(ErrorCode.Forbidden.Id.ToString()))
            {
                operation.Responses.Add(ErrorCode.Forbidden.Id.ToString(),
                    new OpenApiResponse { Description = ErrorCode.Forbidden.Name.ToString() });
            }

            var jwtAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AppConst.AuthSchemes.Bearer, // oauth2
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    //[ jwtAuthScheme ] = new List<string>()
                    [jwtAuthScheme] = requiredScopes.ToList()
                    //  new OpenApiSecurityRequirement {[oAuthScheme] = requiredScopes.ToList()}
                }
            };
        }
    }
}