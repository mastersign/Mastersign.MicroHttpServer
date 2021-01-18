using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public class ConsoleLogger : ILogger
    {
        private static readonly string[] LevelLabels = new[] {
            "VRB",
            "DEB",
            "INF",
            "WRN",
            "ERR",
            "FTL",
        };

        private static readonly ConsoleColor[] LevelColors = new[] {
            ConsoleColor.Gray,
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Magenta,
        };

        public LogLevel MinLevel { get; set; }

        public bool WithColor { get; set; }

        public string TimestampFormat { get; set; }

        public ConsoleLogger(LogLevel minLevel = LogLevel.Warning, string timestampFormat = "yyyy-MM-dd HH:mm:ss", bool withColor = true)
        {
            MinLevel = minLevel;
            TimestampFormat = timestampFormat;
            WithColor = withColor;
        }

        public void Dispose() { }

        public void Log(LogEvent logEvent)
        {
            if (WithColor)
                InternalLogWithColor(logEvent);
            else
                InternalLog(logEvent);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InternalLog(LogEvent logEvent)
        {
            if (logEvent.Level < MinLevel) return;
            var levelLabel = LevelLabels[(int)logEvent.Level];
            Console.WriteLine($"{logEvent.Timestamp.ToString(TimestampFormat)} [{levelLabel}] {logEvent.Message}");
            if (logEvent.Exception != null)
            {
                Console.WriteLine(logEvent.Exception.ToString());
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InternalLogWithColor(LogEvent logEvent)
        {
            if (logEvent.Level < MinLevel) return;
            var levelLabel = LevelLabels[(int)logEvent.Level];
            var levelColor = LevelColors[(int)logEvent.Level];
            if (WithColor)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(logEvent.Timestamp.ToString(TimestampFormat));
            Console.Write(" [");
            if (WithColor) Console.ForegroundColor = levelColor;
            Console.Write(levelLabel);
            if (WithColor) Console.ResetColor();
            Console.Write("] ");
            if (WithColor && logEvent.Level > LogLevel.Information)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(logEvent.Message);
            if (WithColor) Console.ResetColor();
            Console.WriteLine();
            if (logEvent.Exception != null)
            {
                if (WithColor) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(logEvent.Exception.ToString());
                if (WithColor) Console.ResetColor();
            }
        }
    }
}
