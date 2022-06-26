using System.Text;
using CodeZero.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Authentication.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAuth(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var authConfig = configuration.GetSection(nameof(Authentication)).Get<Authentication>();

        if (authConfig is null)
        {
            throw new CodeZeroException($"Configure {nameof(Authentication)} settings in appsettings.Environment.json");
        }

        // Add AuthorizeFilter to all actions
        services.AddMvc(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.Authority = authConfig.Authority;
            jwtOptions.Audience = authConfig.Audience;
            jwtOptions.SaveToken = authConfig.SaveToken;

            jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = authConfig.Authority,
                ValidAudience = authConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.ClientSecret)),
            };
        });


        ////    .AddOpenIdConnect(jwtOptions =>
        ////{
        ////    jwtOptions.Authority = authConfig.Authority;
        ////    jwtOptions.ClientId = authConfig.ClientId;
        ////    jwtOptions.ClientSecret = authConfig.ClientSecret;
        ////    jwtOptions.SaveTokens = authConfig.SaveToken;
        ////    jwtOptions.ResponseType = OpenIdConnectResponseType.Code;
        ////    jwtOptions.RequireHttpsMetadata = false; // dev only
        ////    jwtOptions.GetClaimsFromUserInfoEndpoint = true;

        ////    //jwtOptions.Scope.Add("openid");
        ////    //jwtOptions.Scope.Add("profile");
        ////    //jwtOptions.Scope.Add("email");
        ////    //jwtOptions.Scope.Add("claims");

        ////    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        ////    {
        ////        ValidateIssuer = true,
        ////        ValidIssuer = authConfig.Authority,
        ////        ValidateAudience = true,
        ////        ValidAudience = authConfig.Audience,
        ////        ValidateLifetime = true,
        ////        RequireExpirationTime = true,
        ////        ValidateIssuerSigningKey = true,
        ////        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.ClientSecret)),
        ////    };
        ////});

        ////services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

        services.AddAuthorization();

        // IAuthenticationSchemeProvider is already registered at the host level.
        // We need to register it again so it is taken into account at the service level
        // because it holds a reference to an underlying dictionary, responsible of storing
        // the registered schemes which need to be distinct for each service.
        services.AddSingleton<IAuthenticationSchemeProvider, AuthenticationSchemeProvider>();

        return services;
    }
}
