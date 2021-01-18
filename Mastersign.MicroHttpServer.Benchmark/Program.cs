using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mastersign.MicroHttpServer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ArgumentParser(args);
            Console.Title = "Mastersign Micro HTTP Server Benchmark: " + config.Job;
            Console.WriteLine("Benchmark Job: " + config.Job);

            using var svr = new HttpServer();

            if (Jobs.TryGetValue(config.Job, out var jobMethod))
            {
                jobMethod.Invoke(null, new object[] { svr });
            }
            else
            {
                Console.Error.WriteLine("Does not support job: " + config.Job);
                Environment.Exit(1);
                return;
            }

            if (config.LogToConsole)
            {
                svr.LogToConsole(config.LogLevel, config.LogToConsoleWithColors);
            }
            svr.ListenTo(config.Host, config.Port);
            svr.Start();

            Console.WriteLine("Press ESC to stop...");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
        }

        public static IDictionary<string, MethodInfo> Jobs => typeof(Program)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(mi =>
                    mi.GetParameters().Length == 1 &&
                    mi.GetParameters()[0].ParameterType == typeof(IHttpServer))
                .ToDictionary(mi => mi.Name, mi => mi);

        #region Jobs

        public static void Minimal(IHttpServer svr)
        {
            svr.Get(new ConstStringHttpRequestHandler("OK"));
        }

        public static void Infrastructure(IHttpServer svr)
        {
            svr.Use(new ExceptionHandler());
            svr.Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()));
            svr.Get(new ConstStringHttpRequestHandler("infrastructre"));
            svr.Use(new NotFoundHandler());
        }

        #endregion
    }
}
