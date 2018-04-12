using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartId = shoppingCart.GetCartId(context);
            return shoppingCart;
        }

        private const string sessionKey = "ShoppingCart";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Get shopping cart from the session

            //ShoppingCart shoppingCart = (ShoppingCart)controllerContext.HttpContext.Session[sessionKey];

            ShoppingCart shoppingCart = (ShoppingCart)controllerContext.HttpContext.Session["ShoppingCart"];




            // Create new shopping cart if there is no one in the session
            if(shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                controllerContext.HttpContext.Session[sessionKey] = shoppingCart;
               // shoppingCart.MigrateCart(HttpContext.Current.User.Identity.Name, shoppingCart);
            }

            // Return shopping cart
            return shoppingCart;
        }
    }
}