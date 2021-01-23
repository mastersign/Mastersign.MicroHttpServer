using System;
using System.Net;

namespace Mastersign.MicroHttpServer.Benchmark
{
    class ArgumentParser
    {
        private IPAddress _host = IPAddress.Loopback;
        public IPAddress Host => _host;

        private ushort _port = 8080;
        public int Port => _port;

        private LogLevel _logLevel = LogLevel.Verbose;
        public LogLevel LogLevel => _logLevel;

        public bool LogToConsole { get; }

        public bool LogToConsoleWithColors { get; }

        private int _logBuffer = 1000;
        public int LogBuffer => _logBuffer;

        public bool NoDelay { get; }

        public string Job { get; }

        public ArgumentParser(string[] args)
        {
            var p = 0;
            while (p < args.Length)
            {
                switch (args[p])
                {
                    case "-Host":
                        p++;
                        if (p == args.Length) break;
                        if (!IPAddress.TryParse(args[p], out _host))
                        {
                            Console.Error.WriteLine("Invalid value for option -Host: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    case "-Port":
                        p++;
                        if (p == args.Length) break;
                        if (!ushort.TryParse(args[p], out _port))
                        {
                            Console.Error.WriteLine("Invalid value for option -Port: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    case "-LogToConsole":
                        LogToConsole = true;
                        break;
                    case "-LogWithColors":
                        LogToConsoleWithColors = true;
                        break;
                    case "-LogLevel":
                        p++;
                        if (p == args.Length) break;
                        if (!Enum.TryParse(args[p], out _logLevel))
                        {
                            Console.Error.WriteLine("Invalid value for option -LogLevel: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    case "-LogBuffer":
                        p++;
                        if (p == args.Length) break;
                        if (!int.TryParse(args[p], out _logBuffer))
                        {
                            Console.Error.WriteLine("Invalid value for option -LogBuffer: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    case "-NoDelay":
                        NoDelay = true;
                        break;
                    default:
                        if (args[p].StartsWith("-"))
                        {
                            Console.Error.WriteLine("Unknown option or switch: " + args[p]);
                            Environment.Exit(1);
                        }
                        else if (Job == null)
                        {
                            Job = args[p];
                        }
                        else
                        {
                            Console.Error.WriteLine("Only one job is accepted.");
                            Environment.Exit(1);
                        }
                        break;
                }
                p++;
            }
            if (Job == null)
            {
                Console.Error.WriteLine("You must specify at least one job.");
                Environment.Exit(1);
            }
        }
    }
}
