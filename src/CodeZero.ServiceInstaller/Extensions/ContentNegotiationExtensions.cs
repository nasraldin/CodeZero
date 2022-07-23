namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Content Negotiation configure.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddContentNegotiation([NotNull] this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            // Add XML Content Negotiation
            config.RespectBrowserAcceptHeader = true;

            // tells the server that if the client tries to negotiate for
            // the media type the server doesn’t support,
            // it should return the 406 Not Acceptable status code.
            config.ReturnHttpNotAcceptable = true;

        }).AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        })
        .AddXmlSerializerFormatters()
        // support XML formatters
        .AddXmlDataContractSerializerFormatters();

        return services;
    }
}