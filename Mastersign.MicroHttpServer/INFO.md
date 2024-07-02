# Mastersign.MicroHttpServer

> A very lightweight & simple embedded HTTP server for C#

This library is a derivative of the [µHttpSharp](https://github.com/jcaillon/uHttpSharp) by [jcaillon](https://github.com/jcaillon), which is a fork of [µHttpSharp](https://github.com/bonesoul/uhttpsharp). This project has a large number of [forks](https://github.com/bonesoul/uhttpsharp/network).
It seems a lot of folks like to have their own customized tiny embedded HTTP server 🙂

In that tradition, _Mastersign.MicroHttpServer_ is another twist on the subject.

## Goals

* .NET Standard 2.0 as only requirement and therefore, usable in .NET Framework ≥ 4.6.1 and .NET ≥ 5
* SSL/TLS support
* Reasonably fast
* Full support for asynchronous handlers
* Fluent API with composable routing inspired by ExpressJS, koa, and others
* Class and function handlers
* Heavily overloaded methods for a variety of simple use cases

## Limitations

* Only basic HTTP features
* **NOT** battle tested
* Only recommended for non-public services

## Example

The most simple example, binding to <http://127.0.0.1:8080>:

```cs
using Mastersign.MicroHttpServer;

using var svr = new HttpServer().ListenToLoopback();
svr.Get("/", "Hello World!");
svr.Start();

Console.WriteLine("Press ESC to stop...");
while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
```

Another example with a couple of different ways of defining routes and sub-routes:

```cs
using Mastersign.MicroHttpServer;

using var svr = new HttpServer()
    .LogToConsole(LogLevel.Debug)
    .ListenTo("127.0.0.1", 8080);

svr
    // register default middleware for simple exception report
    .Use(new ExceptionHandler())  
    // register middleware for logging request timing
    .Use(new TimingMiddleware(LogLevel.Information))
    // register middleware for content compression
    .Use(new CompressionMiddelware(new GZipCompressor(), new DeflateCompressor()))

    // register GET handler, returning a constant string
    .Get("/", "Index")
    // register GET handler, returning text composed by using query arguments
    .Get("about", ctx => $"About (Locale = {ctx.Request.Query.GetByNameOrDefault("l", "en")})")
    // register HTTP method agnostic handler, redirecting to another route
    .UseWhen("redirect", (ctx, _) => ctx.RedirectTemporarily("about"))

    // start a branch /files/...
    .Branch("files")
        // registering a static file handler for all sub-routes
        .GetAll(new FileHandler(@"F:\"))
        // end the branch with a 404 for everything, the file handler handle
        .EndWith(new NotFoundHandler())

    // start another branch /api/...
    .Branch("api")
        // register an anonymous delegate as handler, responding with a constant text
        .Get("info", (ctx, _) => ctx.Respond(StringHttpResponse.Text("Info")))
        // end the branch by responding with a constant string to anything other then /api/info
        .EndWith("API")

    // register a route with a placeholder
    .Get("item/{No}", ctx => $"Item No. {ctx.RouteParameters.GetByName("No")}")

    // register an app (sub-router) under a prefix route
    .UseWhen("my-app", MyApp())

    // register fallback handler
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
```
