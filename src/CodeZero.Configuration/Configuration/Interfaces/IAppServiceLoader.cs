using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace CodeZero.Configuration;

/// <summary>
/// Load <see cref="IConfiguration"/>, 
/// <see cref="IWebHostEnvironment"/>, 
/// <see cref="IFeatureManager"/>. at runtime.
/// </summary>
public interface IAppServiceLoader
{
    IWebHostEnvironment Environment { get; set; }
    IConfiguration Configuration { get; set; }
    IFeatureManager FeatureManager { get; set; }
}