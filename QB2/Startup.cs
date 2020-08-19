using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QB2.Startup))]
namespace QB2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
 
