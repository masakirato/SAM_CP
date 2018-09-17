using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAM.Startup))]
namespace SAM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
