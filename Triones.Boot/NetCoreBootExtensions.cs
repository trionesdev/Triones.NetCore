using Microsoft.AspNetCore.Builder;
using Triones.NetCore.Boot.Properties;
using Triones.NetCore.Extensions.Services;

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
