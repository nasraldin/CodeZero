﻿using CodeZero.Configuration.Models;

namespace CodeZero.Configuration;

/// <summary>
/// Represents Swagger configuration parameters
/// </summary>
public partial class SwaggerConfig
{
    public string RouteTemplate { get; set; } = "swagger/{documentName}/swagger.json";
    public string RoutePrefix { get; set; } = "swagger.json";
    public string UiEndpoint { get; set; } = "swagger";
    public string AuthorizationUrl { get; set; } = default!;
    public List<Scopes> Scopes { get; set; } = default!;
    public int DefaultModelsExpandDepth { get; set; } = 1;
}