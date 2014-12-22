using System.Net.Http;
using Microsoft.Owin.Testing;

namespace Poll.BoundaryTest
{
    public static class HttpClientFactory
    {
        public static HttpClient Create()
        {
            var server = TestServer.Create<Startup>();
            return server.HttpClient;
        }
    }
}