using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EniProjetMvc.Startup))]
namespace EniProjetMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
