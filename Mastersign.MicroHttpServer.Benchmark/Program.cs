﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Mastersign.MicroHttpServer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ArgumentParser(args);
            Console.Title = "Mastersign Micro HTTP Server Benchmark: " + config.Job;
            Console.WriteLine("Benchmark Job: " + config.Job);

            using var svr = new HttpServer(logBuffer: config.LogBuffer);

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
                svr.LogToConsole(minLevel: config.LogLevel, withColor: config.LogToConsoleWithColors);
            }
            X509Certificate serverCert = null;
            if (config.TLS)
            {
                serverCert = Certificates.BuildSelfSignedServerCertificate();
            }
            svr.ListenTo(config.Host, config.Port, serverCertificate: serverCert, noDelay: config.NoDelay);
            svr.Start();

            Console.WriteLine("Press ESC to stop...");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
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
            svr.GetAll(new ConstStringHttpRequestHandler("OK"));
        }

        public static void Infrastructure(IHttpServer svr)
        {
            svr.Use(new ExceptionHandler());
            svr.Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()));
            svr.GetAll(new ConstStringHttpRequestHandler("infrastructure"));
            svr.Use(new NotFoundHandler());
        }

        #endregion
    }
}
