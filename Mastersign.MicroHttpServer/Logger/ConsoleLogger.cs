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

        private LogLevel MinLevel { get; set; }

        private bool WithColor { get; set; }

        public ConsoleLogger(LogLevel minLevel = LogLevel.Warning, bool withColor = true)
        {
            MinLevel = minLevel;
            WithColor = withColor;
        }

        public void Log(LogLevel level, string message, Exception e)
        {
            Task.Run(() => InternalLog(level, message, e));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InternalLog(LogLevel level, string message, Exception e)
        {
            if (level < MinLevel) return;
            var ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var levelLabel = LevelLabels[(int)level];
            var levelColor = LevelColors[(int)level];
            if (WithColor)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(ts);
            Console.Write(" [");
            if (WithColor) Console.ForegroundColor = levelColor;
            Console.Write(levelLabel);
            if (WithColor) Console.ResetColor();
            Console.Write("] ");
            if (WithColor && level > LogLevel.Information)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(message);
            if (WithColor) Console.ResetColor();
            Console.WriteLine();
            if (e != null)
            {
                if (WithColor) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(e.ToString());
                if (WithColor) Console.ResetColor();
            }
        }
    }
}
