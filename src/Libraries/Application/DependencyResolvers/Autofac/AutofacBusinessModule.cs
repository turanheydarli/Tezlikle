using Application.Services.Authentication;
using Application.Services.Authentication.Interfaces;
using Application.Services.Catalog;
using Application.Services.Catalog.Interfaces;
using Application.Services.Media;
using Application.Services.Media.Interfaces;
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
        
        builder.RegisterType<AdSpaceService>().As<IAdSpaceService>();
        
        builder.RegisterType<CategoryService>().As<ICategoryService>();
        builder.RegisterType<PictureService>().As<IPictureService>();
        builder.RegisterType<ProductService>().As<IProductService>();
        
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}