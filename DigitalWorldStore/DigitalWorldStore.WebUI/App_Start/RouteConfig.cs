using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DigitalWorldStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // The route which is higher has higher priority

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*** Produst Controller's routes ***/
            routes.MapRoute(null, "",
                new
                {
                    controller = "Home",
                    action = "Index"
                });

            //// Route to registration form
            //routes.MapRoute(null, "",
            //                new
            //                {
            //                    controller = "UserAccount",
            //                    action = "Register"

            //                });

            // Route to login form
            routes.MapRoute(null, "UserAccount/Login",
                            new
                            {
                                controller = "UserAccount",
                                action = "Login"
                            });


            // Route returns all products for all categories for Page 1
            routes.MapRoute(null, "",
                            new
                            {
                                controller = "Product",
                                action = "List",
                                category = (string)null,
                                page = 1
                            });

            // Route returns all products for all categories for selected page
            routes.MapRoute(null, "Page{page}",
                            new
                            {
                                controller = "Product",
                                action = "List",
                                category = (string)null
                            },
                            new { page = @"\d+" });

            // Route returns all products from selected category for Page 1
            routes.MapRoute(null, "{category}",
                            new { controller = "Product", action = "List", page = 1 });


            // Route returns all products from selected category for decided page
            routes.MapRoute(null, "{category}/Page{page}",
                            new { controller = "Product", action = "List" },
                            new { page = @"\d+" });

//----------------------------------------------------------------------------------------------


            routes.MapRoute(null, "{controller}/{action}");

            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new { Controller = "Product", action = "List" }
            //    );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);
        }
    }
}
