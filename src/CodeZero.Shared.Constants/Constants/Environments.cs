namespace CodeZero;

public static partial class AppConst
{
    public static readonly string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

    /// <summary>
    /// Environment names
    /// </summary>
    public static partial class Environments
    {
        // This from Microsoft.AspNetCore.Hosting.Environments
        public static readonly string Development = "Development";
        public static readonly string Production = "Production";
        public static readonly string Staging = "Staging";

        // more env names by CodeZero
        public static readonly string Dev = "dev";
        public static readonly string Prod = "prod";
        public static readonly string Stag = "stag";
        public static readonly string Test = "test";
        public static readonly string QA = "qa";
        public static readonly string UAT = "uat";
    }
}