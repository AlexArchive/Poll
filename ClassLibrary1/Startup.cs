using System.Web.Http;
using Microsoft.Owin;
using Owin;
using PollApi;

[assembly: OwinStartup(typeof(Startup))]

namespace PollApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            new Bootstrap().Configure(config);
            app.UseWebApi(config);
        }
    }
}