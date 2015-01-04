using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Testing;
using Owin;

namespace PollApi.BoundaryTest.Support
{
    public static class HttpClientFactory
    {
        public static HttpClient Create()
        {
            var server = TestServer.Create(Configure);
            return server.HttpClient;
        }

        private static void Configure(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            new TestBootstrap().Configure(config);
            app.UseWebApi(config);
        }
    }
}