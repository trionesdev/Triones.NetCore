using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Triones.Extensions.beans;
using Triones.Extensions.Stereotype;

namespace Triones.Extensions.Services;

public static class ComponentScan
{
    
    public static void AddComponentsScan(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddControllers().AddControllersAsServices();
        builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
        {
            cb.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t =>
                {
                    return  t.GetCustomAttributes( false).Any(attr => attr is ComponentAttribute || attr.GetType().IsDefined(typeof(ComponentAttribute), true));
                })
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(new AutowiredSelector());
        });
    }

}