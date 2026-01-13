using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Triones.NetCore.Boot.Properties;

public static class ConfigurationPropertiesRegister
{
    public static void AddConfigurationProperties(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        if (assemblies is not { Length: > 0 }) return;
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(t =>
                t.GetCustomAttribute<ConfigurationPropertiesAttribute>() != null &&
                t is { IsClass: true, IsAbstract: false });
            if (!types.Any()) continue;
            foreach (var type in types)
            {
                var configurationPropertiesAttribute = type.GetCustomAttribute<ConfigurationPropertiesAttribute>();
                var section = configuration.GetSection(configurationPropertiesAttribute!.Value);
                
                var method = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethods()
                    .FirstOrDefault(m => 
                        m is { Name: nameof(OptionsServiceCollectionExtensions.Configure), IsGenericMethodDefinition: true } && // 必须是泛型定义
                        m.GetParameters().Length == 2 && 
                        m.GetParameters()[1].ParameterType == typeof(IConfiguration));
                
                var genericMethod = method.MakeGenericMethod(type);
                genericMethod.Invoke(null, [builder.Services, section]);
            }

            // builder.Services.Configure<>(configuration.GetSection(""));
        }
    }
}