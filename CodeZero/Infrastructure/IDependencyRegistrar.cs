using CodeZero.Configuration;

namespace CodeZero.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        ///// <summary>
        ///// Register services and interfaces
        ///// </summary>
        ///// <param name="builder">Container builder</param>
        ///// <param name="typeFinder">Type finder</param>
        ///// <param name="appSettings">App settings</param>
        //void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppSettings appSettings);

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }


    ///// <summary>
    ///// Add and configure services
    ///// </summary>
    ///// <param name="services">Collection of service descriptors</param>
    ///// <param name="configuration">Configuration of the application</param>
    //void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    ///// <summary>
    ///// Configure HTTP request pipeline
    ///// </summary>
    ///// <param name="application">Builder for configuring an application's request pipeline</param>
    //void ConfigureRequestPipeline(IApplicationBuilder application);
}
