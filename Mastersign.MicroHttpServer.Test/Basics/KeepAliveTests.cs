using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mastersign.MicroHttpServer.Test.Basics
{
    public class KeepAliveTests
    {
        [Fact]
        public async void ConnectionStaysOpenWithKeepAlive()
        {
            using (var server = new HttpServer())
            {
                server.ListenToLoopback(10201);
                server.GetAll("content");
                server.Start();

                using (var client = new System.Net.Http.HttpClient())
                {
                    var request = new System.Net.Http.HttpRequestMessage(
                        System.Net.Http.HttpMethod.Get, "http://127.0.0.1:10201/");

                    var response = await client.SendAsync(request);
                    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Equal("content", content);
                }
            }
        }
    }
}
