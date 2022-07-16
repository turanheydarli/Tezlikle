using Microsoft.Extensions.DependencyInjection;

namespace Shared.Utilities;


public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static IServiceCollection Create(IServiceCollection services, IServiceProvider provider = null, IServiceScope scope = null)
    {
        ServiceProvider = scope == null ? provider : scope.ServiceProvider;

        return services;
    }

}