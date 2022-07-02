using Application.Services.Authorization;
using Application.Services.Authorization.Interfaces;
using Application.Services.Catalog;
using Application.Services.Catalog.Interfaces;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Shared.Utilities.Interceptors;

namespace Application.DependencyResolvers.Autofac;

public class AutofacBusinessModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<AuthService>().As<IAuthService>();
        
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}