using System.Reflection;
using Application.Utilities.Security.JWT;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Persistence;
using Infrastructure.Persistence.EntityFramework;
using Infrastructure.Persistence.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CrossCuttingConcerns.Caching;
using Shared.CrossCuttingConcerns.Caching.Microsoft;
using Shared.Utilities;

namespace Application.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection InjectApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
        
        ServiceTool.Create(services, services.BuildServiceProvider());
        
        return services;
    }
    public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services)
    {
        
        services.AddDbContext<ProjectDbContext>(ServiceLifetime.Scoped);

        services.AddScoped<DbContext>(provider => provider.GetService<ProjectDbContext>());

        return services;
    }
    
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutofac();
        services.AddMemoryCache();


        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();


        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = "/explore";
            options.LogoutPath = "/auth/logout";
            options.Cookie.Name = "uz_user";
            options.ClaimsIssuer = tokenOptions.Issuer;
        });

        return services;
    }
}