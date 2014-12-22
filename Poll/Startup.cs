using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Poll;

[assembly: OwinStartup(typeof(Startup))]

namespace Poll
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
