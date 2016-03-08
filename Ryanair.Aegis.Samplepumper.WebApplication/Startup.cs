using Hangfire;
using Microsoft.Owin;
using Owin;
using Ryanair.Aegis.Samplepumper.WebApplication;

[assembly: OwinStartup(typeof (Startup))]

namespace Ryanair.Aegis.Samplepumper.WebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}