using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mastersign.MicroHttpServer.Test.Security
{
    public class HeaderTests
    {
        [Fact]
        public async void DoAllowRequestLineEqualLimit()
        {
            var protocolOverhead = "GET / HTTP/1.1\r\n".Length;
            var lineLimit = protocolOverhead + 200;
            var requestProvider = new HttpRequestProvider { LineLimit = lineLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10001);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10001/" + new string('T', 100));
                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
                }
            }
        }

        [Fact]
        public async void DoNotAllowRequestLineGreaterLimit()
        {
            var protocolOverhead = "GET / HTTP/1.1\r\n".Length;
            var lineLimit = protocolOverhead + 200;
            var requestProvider = new HttpRequestProvider { LineLimit = lineLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10002);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10002/" + new string('T', 200 + 1));
                    await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(async () =>
                    {
                        await client.SendAsync(request);
                    });
                }
            }
        }

        [Fact]
        public async void DoAllowSingleHeaderLineEqualLimit()
        {
            var headerOverhead = "X-Test: \r\n".Length;
            var lineLimit = headerOverhead + 200;
            var requestProvider = new HttpRequestProvider { LineLimit = lineLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10003);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10003");
                    request.Headers.Add("X-Test", new string('T', 200));
                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
                }
            }
        }

        [Fact]
        public async void DoNotAllowSingleHeaderLineGreaterLimit()
        {
            var headerOverhead = "X-Test: \r\n".Length;
            var lineLimit = headerOverhead + 200;
            var requestProvider = new HttpRequestProvider { LineLimit = lineLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10004);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10004");
                    request.Headers.Add("X-Test", new string('T', 200 + 1));
                    await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(async () =>
                    {
                        await client.SendAsync(request);
                    });
                }
            }
        }

        [Fact]
        public async void DoAllowTotalHeaderEqualLimit()
        {
            int protocolLineLength = "GET / HTTP/1.1\r\n".Length;
            int hostHeaderLength = "Host: 127.0.0.1:10005\r\n".Length;
            int keepAliveHeaderLength = "Connection: Keep-Alive\r\n".Length;
            int totalLimit = protocolLineLength + hostHeaderLength + keepAliveHeaderLength + 200;
            var requestProvider = new HttpRequestProvider { TotalHeaderLimit = totalLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10005);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10005");
                    // is required for compatibility, because net461 always sets it by it self
                    request.Headers.Add("Connection", "Keep-Alive");
                    for (var i = 0; i < 10; i++)
                    {
                        // 20 chars per header line
                        //     8 chars for "X-T-00: "
                        //     10 chars for the value
                        //     2 chars for \r\n
                        request.Headers.Add($"X-T-{i:00}", new string('T', 10));
                    }
                    
                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
                }
            }
        }

        [Fact]
        public async void DoNotAllowTotalHeaderGreaterLimit()
        {
            int protocolLineLength = "GET / HTTP/1.1\r\n".Length;
            int hostHeaderLength = "Host: 127.0.0.1:10006\r\n".Length;
            int keepAliveHeaderLength = "Connection: Keep-Alive\r\n".Length;
            int totalLimit = protocolLineLength + hostHeaderLength + keepAliveHeaderLength + 200 - 1;
            var requestProvider = new HttpRequestProvider { TotalHeaderLimit = totalLimit };
            using (var server = new HttpServer(requestProvider: requestProvider))
            {
                server.ListenToLoopback(10006);
                server.Use(ctx => EmptyHttpResponse.Create(HttpResponseCode.NoContent, keepAlive: false));
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10006");
                    // is required for compatibility, because net461 always sets it by it self
                    request.Headers.Add("Connection", "Keep-Alive");
                    for (var i = 0; i < 10; i++)
                    {
                        // 20 chars per header line
                        //     8 chars for "X-T-00: "
                        //     10 chars for the value
                        //     2 chars for \r\n
                        request.Headers.Add($"X-T-{i:00}", new string('T', 10));
                    }
                    await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(async () =>
                    {
                        await client.SendAsync(request);
                    });
                }
            }
        }
    }
}
