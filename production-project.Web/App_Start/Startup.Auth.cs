namespace production_project.Web
{
    using System;
    using System.Configuration;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using production_project.Web.Identity;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(IdentityUserContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieHttpOnly = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["accessTokenExpireTimeSpan"])),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        if (!context.Request.Path.StartsWithSegments(new PathString("/api")))
                        {
                            context.Response.Redirect(context.RedirectUri);
                        }
                    }
                }
            });
        }
    }
}
