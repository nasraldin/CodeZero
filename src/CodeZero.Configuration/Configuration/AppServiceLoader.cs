using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace CodeZero.Configuration;

/// <summary>
/// Represents the most common app settings ared used in the application
/// </summary>
public class AppServiceLoader : IAppServiceLoader
{
    public static AppServiceLoader Instance { get; private set; } = default!;

    public AppServiceLoader(IWebHostEnvironment env, IConfiguration configuration)
    {

        Environment = env;
        Configuration = configuration;

        Instance = this;
    }

    public IWebHostEnvironment Environment { get; set; }
    public IConfiguration Configuration { get; set; }
    public virtual IFeatureManager FeatureManager { get; set; } = default!;
}