using Microsoft.AspNetCore.Http;

namespace CodeZero.Helpers;

/// <summary>
/// Provides access to the current <see cref="IHttpContextAccessor"/>, 
/// if one is available.
/// </summary>
public class HttpContextHelper : IHttpContextHelper
{
    public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;

        Current = this;
    }

    public IHttpContextAccessor HttpContextAccessor { get; set; }
    public static HttpContextHelper Current { get; private set; } = default!;
}

public interface IHttpContextHelper
{
    IHttpContextAccessor HttpContextAccessor { get; set; }
}