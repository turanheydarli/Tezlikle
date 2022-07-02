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

namespace Application.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection InjectApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
    public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services)
    {
        
        services.AddDbContext<ProjectDbContext>(ServiceLifetime.Transient);

        services.AddTransient<DbContext>(provider => provider.GetService<ProjectDbContext>());

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
            options.Cookie.Name = "UZ_User";
            options.ClaimsIssuer = tokenOptions.Issuer;
        });

        return services;
    }
}