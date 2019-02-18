using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMS.Web.Two.Startup))]
namespace SMS.Web.Two
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
