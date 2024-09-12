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
    }
}
