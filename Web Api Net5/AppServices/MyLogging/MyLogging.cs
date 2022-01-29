using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AppServices.MyLogging
{
    public static class MyLogging
    {
        public static Logger MyConfigureLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "c:\\TestNet5\\logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                    ).CreateLogger(); ;
            return logger;
        }
    }
}
