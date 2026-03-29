using Microsoft.AspNetCore.Builder;
using Triones.Extensions.Services;
using Triones.NetCore.Boot.Properties;

namespace Triones.NetCore.Boot
{
    public static class NetCoreBootExtensions
    {
        public static void NetCoreBoot(this WebApplicationBuilder builder)
        {
            builder.AddComponentsScan();
            builder.AddConfigurationProperties(builder.Configuration);
        }
    }
}
