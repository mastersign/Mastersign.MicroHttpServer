﻿using System;
using System.Collections.Generic;
using System.Net;

namespace Mastersign.MicroHttpServer.Benchmark
{
    class ArgumentParser
    {
        private IPAddress _host = IPAddress.Loopback;
        public IPAddress Host => _host;

        private ushort _port;
        public int Port => _port;

        private LogLevel _logLevel = LogLevel.Verbose;
        public LogLevel LogLevel => _logLevel;

        public bool LogToConsole { get; }

        public bool LogToConsoleWithColors { get; }

        public IList<string> Setups { get; } = new List<string>();

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
                            Console.WriteLine("Invalid value for option -Host: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    case "-Port":
                        p++;
                        if (p == args.Length) break;
                        if (!ushort.TryParse(args[p], out _port))
                        {
                            Console.WriteLine("Invalid value for option -Port: " + args[p]);
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
                            Console.WriteLine("Invalid value for option -LogLevel: " + args[p]);
                            Environment.Exit(1);
                        }
                        break;
                    default:
                        if (args[p].StartsWith("-"))
                        {
                            Console.WriteLine("Unknown option or switch: " + args[p]);
                            Environment.Exit(1);
                        }
                        Setups.Add(args[p]);
                        break;
                }
                p++;
            }
        }
    }
}
