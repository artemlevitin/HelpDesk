using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpDesk_AdminLte.Startup))]
namespace HelpDesk_AdminLte
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            new Utilites.DBInitializer().exec(new Models.ApplicationDbContext());
        }
    }
}
