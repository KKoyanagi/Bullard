using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bullard_Inc.Startup))]
namespace Bullard_Inc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
