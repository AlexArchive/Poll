using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Poll.BoundaryTest
{
    public class TestBootstrap : Bootstrap
    {
        protected override void SetControllerActivator(HttpConfiguration config)
        {
            config.Services.Replace(
                typeof(IHttpControllerActivator), 
                new TestCompositionRoot());
        }
    }
}