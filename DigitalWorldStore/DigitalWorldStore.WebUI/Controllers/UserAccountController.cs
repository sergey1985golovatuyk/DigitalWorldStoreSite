using DigitalWorldStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DigitalWorldStore.Domain.Entities;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace DigitalWorldStore.WebUI.Controllers
{
    public class UserAccountController : Controller
    {

        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        // Methods for registration
        public ActionResult Register()
        {
            //ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    UserFirstName = registerModel.UserName,
                    UserLastName = registerModel.UserLastName
                    
                };
                IdentityResult identityResult = await UserManager.CreateAsync(user, registerModel.Password);
                if (identityResult.Succeeded)
                {
                    MigrateShoppingCart(registerModel.Email);
                    return RedirectToAction("Login", "UserAccount");
                    
                }
                else
                {
                    foreach(string error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(registerModel);
        }

        // Methods for auth
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();             

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                DigitalWorldStore.Domain.Entities.User user = await UserManager.FindAsync(loginModel.Email, loginModel.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claims = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claims);
                    MigrateShoppingCart(loginModel.Email);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }     
}