using CodeZero;

namespace Microsoft.Extensions.Hosting;

public static class HostingEnvironmentExtensions
{
    /// <summary>
    /// Checks if the current host environment name is dev
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is dev, otherwise false.</returns>
    public static bool IsDev(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.Dev);
    }

    /// <summary>
    /// Checks if the current host environment name is prod
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is prod, otherwise false.</returns>
    public static bool IsProd(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.Prod);
    }

    /// <summary>
    /// Checks if the current host environment name is stag
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is stag, otherwise false.</returns>
    public static bool IsStag(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.Stag);
    }

    /// <summary>
    /// Checks if the current host environment name qa dev
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is qa, otherwise false.</returns>
    public static bool IsQA(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.QA);
    }

    /// <summary>
    /// Checks if the current host environment name is uat
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is uat, otherwise false.</returns>
    public static bool IsUAT(IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.UAT);
    }

    /// <summary>
    /// Checks if the current host environment name is test
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns> True if the environment name is test, otherwise false.</returns>
    public static bool IsTest(IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.IsEnvironment(AppConst.Environments.Test);
    }
}