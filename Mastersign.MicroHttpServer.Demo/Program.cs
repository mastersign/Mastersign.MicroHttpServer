using System;

namespace Mastersign.MicroHttpServer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using var svr = new HttpServer();
            svr.LogToConsole(LogLevel.Verbose);

            svr.Use(new ExceptionHandler());
            svr.Use(new TimingMiddleware(LogLevel.Information));
            svr.Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()));
            svr.PathRouter()
               .With("/", ctx => "Index")
               .With("files", new FileHandler(@"F:\"))
               .With("redirect", (ctx, _) => ctx.RedirectTemporarily("/about"))
               .With("about", ctx => "About")
               .PathRouter("api")
                    .With("info", (ctx, _) => ctx.Respond(StringHttpResponse.Text("Info")))
                    .Without(ctx => "API");
            svr.Use(new NotFoundHandler());

            svr.ListenToLoopback();
            svr.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
