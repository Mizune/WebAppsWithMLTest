using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MLTest2.Startup))]
namespace MLTest2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
