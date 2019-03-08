using ECommerce.Services.Menu;
using ECommerce.Services.Menu.Impl;
using Lamar;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ECommerce
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            // Create a Lamar.ServiceRegistry object
            // and define your service registrations
            //var registry = new ServiceRegistry();

            //var container = new Container(x =>
            //{
            //    x.AddTransient<IMenuService, IMenuService>();
            //});

            // Or use StructureMap style registration syntax
            // as an alternative or to use more advanced usage
            //registry.For<IMenuService>()
            //    .Use<MenuService>()
            //    .Singleton();

            //var container = new Container(registry);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            app.UseGoogleAuthentication();
        }
    }
}