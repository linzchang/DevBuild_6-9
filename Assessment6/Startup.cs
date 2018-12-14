using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Assessment6.Startup))]

namespace Assessment6
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChangIdentityPartyDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));

            app.CreatePerOwinContext<UserManager<IdentityUser>>((options, context)
                => new UserManager<IdentityUser>(
                    new UserStore<IdentityUser>(context.Get<IdentityDbContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Identity/Login")
            });
        }
    }
}
