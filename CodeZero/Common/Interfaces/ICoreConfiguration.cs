namespace CodeZero.Common.Interfaces
{
    public interface ICoreConfiguration
    {
        ApplicationConfiguration Application { get; set; }
        HostingConfiguration Hosting { get; set; }
    }

    public class ApplicationConfiguration
    {
        public string Name { get; set; }
        public ApplicationSettings Settings { get; set; }
    }

    public class ApplicationSettings
    {
        public string Url { get; set; }
    }

    /// <summary>
    /// Only used in Azure WebApp hosted deployments.
    /// Returns info on the WebApp instance for the current process. 
    /// Can be used to log which WebApp instance a process ran on.
    /// </summary>
    public class HostingConfiguration
    {
        public string SiteName { get; set; }
        public string InstanceId { get; set; }
    }
}
