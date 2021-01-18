using System;

namespace Mastersign.MicroHttpServer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using var svr = new HttpServer()
                .LogToConsole(LogLevel.Debug)
                .ListenToLoopback();

            // close regex pattern at the and when not diving

            svr
                .Use(new ExceptionHandler())
                .Use(new TimingMiddleware(LogLevel.Information))
                .Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()))
                .Dive("/files").Get(new FileHandler(@"F:\")).Ascent(new NotFoundHandler())
                .UseWhen("redirect", (ctx, _) => ctx.RedirectTemporarily("about"))
                .Get("about", ctx => "About")
                .Dive("api")
                    .Get("info", (ctx, _) => ctx.Respond(StringHttpResponse.Text("Info")))
                    .Ascent(ctx => "API")
                .Get("other", ctx => "Other")
                .Get("/", ctx => "Index")
                .Use(new NotFoundHandler());

            svr.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
