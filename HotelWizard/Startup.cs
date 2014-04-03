using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelWizard.Startup))]
namespace HotelWizard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
