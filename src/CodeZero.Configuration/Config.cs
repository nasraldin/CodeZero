using Microsoft.Extensions.Configuration;

namespace CodeZero.Configuration;

/// <inheritdoc cref="IConfig{T}"/>
public class Config<T> : IConfig<T>
{
    public Config(IConfiguration configuration)
    {
        Options = configuration.GetSection(typeof(T).Name).Get<T>();
    }

    /// <inheritdoc/>
    public virtual T Options { get; set; }
}