using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using DigitalWorldStore.Domain.Real_Repository;
using DigitalWorldStore.WebUI.Models;

[assembly: OwinStartup(typeof(DigitalWorldStore.WebUI.App_Start.Startup))]

namespace DigitalWorldStore.WebUI.App_Start
{
    public class Startup
    {
        // Set up context and manager
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<EFDbContext>(EFDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/UserAccount/Login"),
            });
        }
    }
}