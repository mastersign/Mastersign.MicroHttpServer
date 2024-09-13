using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mastersign.MicroHttpServer.Test.Basics
{
    public class SimpleRequestTests
    {
        [Fact]
        public async void GetString()
        {
            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10101);
                server.GetAll("Hello World");
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(1) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10101/");

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
                    
                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("Hello World", content);
                }
            }
        }

        [Fact]
        public async void Get100kBytes()
        {
            var payload = new byte[100 * 1024];
            var rand = new Random();
            rand.NextBytes(payload);

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10102);
                server.GetAll(payload);
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(1) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10102/");

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    var content = await response.Content.ReadAsByteArrayAsync();
                    Assert.Equal(payload, content);
                }
            }
        }

        [Fact]
        public async void PostBytesReadIndividually()
        {
            var payload = new byte[23 * 77 * 123]; // some random length, not fitting into a power of two buffer scheme
            var rand = new Random();
            rand.NextBytes(payload);

            var expectedContentLength = payload.LongLength;
            var expectedContentType = "application/x-test-bytes";

            long actualContentLength = -1L;
            string actualContentType = null;
            long beginStreamPosition = -1L;
            long endStreamPosition = -1L;
            byte[] receivedPayload = null;
            var readBytes = 0L;

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10111);
                server.PostAll(ctx =>
                {
                    actualContentLength = ctx.Request.ContentLength();
                    actualContentType = ctx.Request.ContentType();
                    receivedPayload = new byte[actualContentLength];
                    beginStreamPosition = ctx.Request.ContentStream.Position;
                    for (var i = 0L; i <= actualContentLength; i++)
                    {
                        var b = ctx.Request.ContentStream.ReadByte();
                        if (b < 0) break;
                        receivedPayload[i] = (byte)(b & 0xFF);
                        readBytes++;
                    }
                    endStreamPosition = ctx.Request.ContentStream.Position;
                    return "OK";
                });
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(2) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Post, "http://127.0.0.1:10111/");
                    request.Content = new System.Net.Http.ByteArrayContent(payload);
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(expectedContentType);

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(expectedContentLength, actualContentLength);
                    Assert.Equal(expectedContentType, actualContentType);
                    Assert.Equal(0L, beginStreamPosition);
                    Assert.Equal(expectedContentLength, endStreamPosition);
                    Assert.Equal(payload, receivedPayload);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("OK", content);
                }
            }
        }

        [Fact]
        public async void PostBytesReadSync()
        {
            //var payload = new byte[23 * 77 * 123]; // some random length, not fitting into a power of two buffer scheme
            var payload = new byte[512]; // some random length, not fitting into a power of two buffer scheme
            var rand = new Random();
            rand.NextBytes(payload);

            var expectedContentType = "application/x-test-bytes";

            byte[] receivedPayload = null;
            string actualContentType = null;

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10112);
                server.PostAll(ctx =>
                {
                    actualContentType = ctx.Request.ContentType();
                    var contentLength = ctx.Request.ContentLength();
                    receivedPayload = ctx.Request.ReadContentAsBytes();
                    return "OK";
                });
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(2) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Post, "http://127.0.0.1:10112/");
                    request.Content = new System.Net.Http.ByteArrayContent(payload);
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(expectedContentType);

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(expectedContentType, actualContentType);
                    Assert.Equal(payload, receivedPayload);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("OK", content);
                }
            }
        }

        [Fact]
        public async void PostBytesReadAsync()
        {
            var payload = new byte[23 * 77 * 123]; // some random length, not fitting into a power of two buffer scheme
            var rand = new Random();
            rand.NextBytes(payload);

            var expectedContentType = "application/x-test-bytes";

            byte[] receivedPayload = null;
            string actualContentType = null;

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10113);
                server.PostAll(async ctx =>
                {
                    actualContentType = ctx.Request.ContentType();
                    receivedPayload = await ctx.Request.ReadContentAsBytesAsync();
                    return "OK";
                });
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(2) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Post, "http://127.0.0.1:10113/");
                    request.Content = new System.Net.Http.ByteArrayContent(payload);
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(expectedContentType);

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(expectedContentType, actualContentType);
                    Assert.Equal(payload, receivedPayload);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("OK", content);
                }
            }
        }

        [Fact]
        public async void PostText()
        {
            var text = "Line ①\nLĩne 2\n";
            var receivedText = string.Empty;

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10114);
                server.PostAll(ctx =>
                {
                    receivedText = ctx.Request.ReadContentAsString();
                    return "OK";
                });
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(2) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Post, "http://127.0.0.1:10114/");
                    request.Content = new System.Net.Http.StringContent(text, Encoding.UTF8, "text/plain");

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(text, receivedText);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("OK", content);
                }
            }
        }
    }
}
