using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLCuaHangVali.Startup))]
namespace QLCuaHangVali
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
