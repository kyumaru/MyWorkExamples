using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gyoza.Startup))]
namespace Gyoza
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
