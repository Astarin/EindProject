using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EindProjectMVC.Startup))]
namespace EindProjectMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
