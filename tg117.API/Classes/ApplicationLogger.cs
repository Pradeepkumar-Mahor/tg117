using Serilog;
using Serilog.Formatting.Json;

namespace tg117.API.Classes
{
    public static class ApplicationLogger
    {
        public static void ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((ctx, lc) =>
            {
                lc.WriteTo.File(new JsonFormatter(), "Log.json", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error);
            });
        }
    }
}