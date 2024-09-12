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
        public async void PostText()
        {
            var text = "Line ①\nLĩne 2\n";
            var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            var textBytes = encoding.GetBytes(text);
            var expectedContentLength = textBytes.LongLength;
            long actualContentLength = -1L;
            long beginStreamPosition = -1L;
            long endStreamPosition = -1L;
            byte[] receivedTextBytes = null;
            var readBytes = 0L;

            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10103);
                server.PostAll(ctx =>
                {
                    actualContentLength = ctx.Request.ContentLength();
                    receivedTextBytes = new byte[actualContentLength];
                    beginStreamPosition = ctx.Request.ContentStream.Position;
                    for (var i = 0L; i <= actualContentLength; i++)
                    {
                        var b = ctx.Request.ContentStream.ReadByte();
                        if (b < 0) break;
                        receivedTextBytes[i] = (byte)(b & 0xFF);
                        readBytes++;
                    }
                    endStreamPosition = ctx.Request.ContentStream.Position;
                    return "OK";
                });
                server.Start();

                using (var client = new System.Net.Http.HttpClient() { Timeout = TimeSpan.FromSeconds(1) })
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Post, "http://127.0.0.1:10103/");
                    request.Content = new System.Net.Http.StringContent(text, encoding, "text/plain");

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(expectedContentLength, actualContentLength);
                    Assert.Equal(0L, beginStreamPosition);
                    Assert.Equal(expectedContentLength, endStreamPosition);
                    Assert.Equal(textBytes, receivedTextBytes);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("OK", content);
                }
            }
        }
    }
}
