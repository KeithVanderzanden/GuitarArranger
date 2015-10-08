using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuitarArranger.Startup))]
namespace GuitarArranger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
