using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Uploads.Startup))]
namespace Uploads
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
