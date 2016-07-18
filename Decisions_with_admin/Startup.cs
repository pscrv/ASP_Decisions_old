using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Decisions_with_admin.Startup))]
namespace Decisions_with_admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
