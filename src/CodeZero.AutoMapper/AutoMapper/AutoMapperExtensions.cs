using System.Reflection;
using AutoMapper;
using CodeZero.AutoMapper;
using JetBrains.Annotations;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add AutoMapper.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAutoMapperConfig([NotNull] this IServiceCollection services)
    {
        // AutoMapper Configurations
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddGlobalIgnore("Id");
            mc.AddGlobalIgnore("CreatedBy");
            mc.AddGlobalIgnore("CreatedAt");
            mc.AddGlobalIgnore("UpdatedBy");
            mc.AddGlobalIgnore("UpdatedAt");
            mc.AddGlobalIgnore("IsDeleted");
            mc.AddGlobalIgnore("DeletedBy");
            mc.AddGlobalIgnore("DeletionTime");
            mc.AddGlobalIgnore("IsActive");
            mc.AddGlobalIgnore("ConcurrencyStamp");
            mc.AddGlobalIgnore("DisplaySequence");
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
