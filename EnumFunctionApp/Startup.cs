using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace EnumFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder
                .Services
                .AddMvcCore()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.Converters.Add(
                        new StringEnumConverter());
                });
        }
    }
}
