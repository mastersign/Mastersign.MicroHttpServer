using Mastersign.MicroHttpServer;

using var svr = new HttpServer()
    .LogToConsole(LogLevel.Debug)
    .ListenToLoopback();

svr
    .Use(new ExceptionHandler())
    .Use(new TimingMiddleware(LogLevel.Information))
    .Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()))
    .Get("/", "Index")
    .Get("about", ctx => $"About (Locale = {ctx.Request.Query.GetByNameOrDefault("l", "en")})")
    .UseWhen("redirect", (ctx, _) => ctx.RedirectTemporarily("about"))
    .Branch("files")
        .GetAll(new FileHandler(@"F:\"))
        .EndWith(new NotFoundHandler())
    .Branch("api")
        .Get("info", (ctx, _) => ctx.Respond(StringHttpResponse.Text("Info")))
        .EndWith("API")
    .Get("item/{No}", ctx => $"Item No. {ctx.RouteParameters.GetByName("No")}")
    .UseWhen("my-app", MyApp())
    .EndWith(new NotFoundHandler());

svr.Start();

Console.WriteLine("Press ESC to stop...");
while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }


static HttpApp MyApp()
{
    var app = new HttpApp("my app");
    app
        .Post("/", async ctx => {
            string text;
            using (var r = new StreamReader(ctx.Request.ContentStream))
            {
                text = await r.ReadToEndAsync();
            }
            return "<h1>Got It!</h1><pre><code>" + text + "</code></pre>";
        })
        .Get("/", "My App");
    return app;
}
