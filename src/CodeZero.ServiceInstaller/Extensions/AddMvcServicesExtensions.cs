using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds MVC framework services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddMvcServices(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        services.AddMvc(mvcOptions =>
        {
            //mvcOptions.OutputFormatters.RemoveType<TextOutputFormatter>();
            //mvcOptions.OutputFormatters.RemoveType<StreamOutputFormatter>();
            mvcOptions.RespectBrowserAcceptHeader = true;
        })
        //.AddFluentValidation(fvc =>
        //{
        //    fvc.RegisterValidatorsFromAssemblyContaining<Program>();
        //})
        .AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        })
        //.ConfigureApplicationPartManager(apm => apm.FeatureProviders.Add(new CustomControllerFeatureProvider(AppServiceLoader.Instance.FeatureManager)))
        .AddControllersAsServices();

        return services;
    }
}
