using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    [DebuggerDisplay("Static Files Handler: {RootDirectory,nq}")]
    public class FileHandler : IHttpRequestHandler
    {
        private static readonly string DIR_SEPARATOR = new string(Path.DirectorySeparatorChar, 1);

        public static IDictionary<string, string> MimeTypes { get; }

        static FileHandler()
        {
            MimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
                {".css", "text/css"},
                {".gif", "image/gif"},
                {".htm", "text/html"},
                {".html", "text/html"},
                {".jpg", "image/jpeg"},
                {".js", "application/javascript"},
                {".png", "image/png"},
                {".svg", "image/svg+xml" },
                {".txt", "text/plain" },
                {".xml", "application/xml"},
            };
        }

        public string RootDirectory { get; set; }

        public string DefaultMimeType { get; set; }

        public List<string> DefaultFiles { get; set; }

        public FileHandler(
            string rootDirectory = ".",
            string defaultMimeType = "application/octet-stream")
        {
            RootDirectory = rootDirectory;
            DefaultMimeType = defaultMimeType;
            DefaultFiles = new List<string> {
                "index.html",
                "index.htm",
                "default.html",
                "default.htm",
            };
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            // prevent escaping root directory by normalizing with Uri.AbsolutePath
            var requestPath = string.Join(DIR_SEPARATOR, context.Route);

            if (string.IsNullOrEmpty(requestPath))
            {
                requestPath = GetDefault();
            }
            if (string.IsNullOrEmpty(requestPath))
            {
                await next().ConfigureAwait(false);
                return;
            }

            var httpRoot = Path.GetFullPath(RootDirectory ?? ".");
            var path = Path.GetFullPath(Path.Combine(httpRoot, requestPath));

            if (!File.Exists(path))
            {
                context.Logger.Trace($"Static file: {context.Route} => {path} NOT FOUND");
                await next().ConfigureAwait(false);
                return;
            }

            context.Logger.Trace($"Static file: {context.Route} => {path}");

            context.Response = StreamHttpResponse.Create(
                File.OpenRead(path),
                HttpResponseCode.OK,
                contentType: GetContentType(path),
                keepAlive: context.Request.KeepAliveConnection());
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path) ?? string.Empty;
            return MimeTypes.TryGetValue(extension, out var mimeType)
                ? mimeType
                : DefaultMimeType;
        }

        private string GetDefault()
        {
            var httpRoot = Path.GetFullPath(RootDirectory ?? ".");
            foreach (var defaultFile in DefaultFiles)
            {
                var path = Path.GetFullPath(Path.Combine(httpRoot, defaultFile));
                if (File.Exists(path)) return defaultFile;
            }
            return string.Empty;
        }
    }
}