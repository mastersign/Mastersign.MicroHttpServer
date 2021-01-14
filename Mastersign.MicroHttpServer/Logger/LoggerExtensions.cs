using System;

namespace Mastersign.MicroHttpServer
{
    internal static class LoggerExtensions
    {
        public static void Fatal(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Fatal, message, e);

        public static void Error(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Error, message, e);

        public static void Warn(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Warning, message, e);

        public static void Info(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Information, message, e);

        public static void Debug(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Debug, message, e);

        public static void Trace(this ILogger logger, string message, Exception e = null)
            => logger?.Log(LogLevel.Verbose, message, e);
    }
}
