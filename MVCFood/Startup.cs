using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCFood.Startup))]
namespace MVCFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
