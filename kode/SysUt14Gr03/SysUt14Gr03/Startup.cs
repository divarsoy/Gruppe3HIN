using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysUt14Gr03.Startup))]
namespace SysUt14Gr03
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
