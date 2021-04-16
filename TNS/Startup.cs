using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TNS.Startup))]
namespace TNS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
