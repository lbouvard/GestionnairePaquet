using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionnairePaquet.Startup))]
namespace GestionnairePaquet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
