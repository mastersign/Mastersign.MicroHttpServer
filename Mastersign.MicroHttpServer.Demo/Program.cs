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

            svr
                .Use(new ExceptionHandler())
                .Use(new TimingMiddleware(LogLevel.Information))
                .Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()))
                .Branch("/files").Get(new FileHandler(@"F:\")).EndWith(new NotFoundHandler())
                .UseWhen("redirect", (ctx, _) => ctx.RedirectTemporarily("about"))
                .Get("about", ctx => "About")
                .Branch("api")
                    .Get("info", (ctx, _) => ctx.Respond(StringHttpResponse.Text("Info")))
                    .EndWith(ctx => "API")
                .Get("other", ctx => "Other")
                .Get("/", ctx => "Index")
                .UseWhen("my-app", MyApp())
                .EndWith(new NotFoundHandler());

            svr.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }

        static HttpApp MyApp()
        {
            var app = new HttpApp("my app");
            app
                .Post("/", ctx => "Got it!")
                .Get("/", ctx => "My App");
            return app;
        }
    }
}
