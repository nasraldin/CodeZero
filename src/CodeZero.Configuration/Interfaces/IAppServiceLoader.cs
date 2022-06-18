using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CodeZero.Configuration;

/// <summary>
/// Load <see cref="IConfiguration"/>, 
/// <see cref="IWebHostEnvironment"/> at runtime.
/// </summary>
public interface IAppServiceLoader
{
    IWebHostEnvironment Environment { get; set; }
    IConfiguration Configuration { get; set; }
}