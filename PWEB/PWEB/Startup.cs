using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PWEB.Startup))]
namespace PWEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
