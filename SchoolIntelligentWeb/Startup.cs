using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolIntelligentWeb.Startup))]
namespace SchoolIntelligentWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
