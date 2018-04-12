using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DigitalWorldStore.WebUI.Infrastructure;
using DigitalWorldStore.WebUI.Binders;
using DigitalWorldStore.Domain.Entities;
using System.Data.Entity;
using DigitalWorldStore.Domain.Real_Repository;

namespace DigitalWorldStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EFDbContext context = new EFDbContext();
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer(new DBInitializer());

            // Set new controllers factory
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            // Using CartModelBinder class for creation new ShoppingCart objects
            ModelBinders.Binders.Add(typeof(ShoppingCart), new CartModelBinder());
        }
    }
}
