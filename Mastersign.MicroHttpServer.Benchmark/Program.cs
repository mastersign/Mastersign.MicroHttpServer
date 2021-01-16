using System;

namespace Mastersign.MicroHttpServer.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ArgumentParser(args);
            Console.Title = "Mastersign Micro HTTP Server Benchmark: " + string.Join(", ", config.Setups);

            using var svr = new HttpServer();

            foreach(var setup in config.Setups)
            {
                switch (setup)
                {
                    case "Minimal":
                        Minimal(svr);
                        break;
                    case "Infrastructure":
                        Infrastructure(svr);
                        break;
                }
            }
            if (config.Setups.Count == 0)
            {
                Console.WriteLine("You need to specify at least one setup.");
                Environment.Exit(1);
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



        static void Minimal(IHttpServer svr)
        {
            svr.Get(new ConstStringHttpRequestHandler("OK"));
        }

        static void Infrastructure(IHttpServer svr)
        {
            svr.Use(new ExceptionHandler());
            svr.Use(new CompressionMiddelware(new GZipCompressor()));
            svr.Get(new ConstStringHttpRequestHandler("infrastructre"));
            svr.Use(new NotFoundHandler());
        }
    }
}
