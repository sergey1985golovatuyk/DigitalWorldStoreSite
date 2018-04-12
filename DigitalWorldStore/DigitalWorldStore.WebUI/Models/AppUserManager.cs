using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using DigitalWorldStore.Domain.Entities;
using DigitalWorldStore.Domain.Real_Repository;


namespace DigitalWorldStore.WebUI.Models
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            EFDbContext eFDbContext = context.Get<EFDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(eFDbContext));
            return manager;
        }
    }
}