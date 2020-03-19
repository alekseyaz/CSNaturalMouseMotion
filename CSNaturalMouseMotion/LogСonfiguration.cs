using NLog;
using NLog.Config;
using NLog.Targets;

namespace CSNaturalMouseMotion
{
    public static class LogСonfiguration
    {
        /// <summary>
        /// Setup custom targets and rules
        /// </summary>
        public static void SetupConfig()
        {

            var config = new LoggingConfiguration();

            // Targets where to log to: Console
            var logconsole = new ColoredConsoleTarget("logconsole")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${message} ${exception}"
            };
            config.AddTarget(logconsole);

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);

            // Apply config           
            LogManager.Configuration = config;

        }
    }
}
