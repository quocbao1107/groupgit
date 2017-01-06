using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanBanh.Startup))]
namespace WebBanBanh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
