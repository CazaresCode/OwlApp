using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Owl.WebMVC.Startup))]
namespace Owl.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
