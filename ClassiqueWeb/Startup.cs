using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassiqueWeb.Startup))]
namespace ClassiqueWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
