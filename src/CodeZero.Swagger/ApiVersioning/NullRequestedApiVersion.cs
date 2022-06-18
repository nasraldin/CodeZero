namespace CodeZero.ApiVersioning;

public class NullRequestedApiVersion : IRequestedApiVersion
{
    public static NullRequestedApiVersion Instance { get; set; } = new();

    public string Current => null!;

    private NullRequestedApiVersion() { }
}