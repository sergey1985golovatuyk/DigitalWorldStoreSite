using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Abstract_Repository;
using DigitalWorldStore.Domain.Real_Repository;
using DigitalWorldStore.Domain.Entities;
using DigitalWorldStore.WebUI.Models;

namespace DigitalWorldStore.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        EFDbContext context = new EFDbContext();

        private IOrderRepository orderRepository;

        public ShoppingCartController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new CartIndexViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotalPrice()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/id
        public ActionResult AddToCart(Product product)
        {
            // Retrieve the product from database
            var addedProduct = context.Products
                .Single(p => p.ProductId == product.ProductId);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/id
        [HttpPost]
        public ActionResult RemoveFromCart(Product product)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            var productName = context.CartLists
                .FirstOrDefault(p => p.ProductId == product.ProductId).Product;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(productName);

            // Display the confirmation message
            var results = new CartIndexViewModel
            {
                Message = Server.HtmlEncode(productName.Name) +
                    " был удален из корзины.",
                CartTotal = cart.GetTotalPrice(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = (int)product.ProductId
            };
            //return Json(results);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveCart(Product product)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.RemoveProductCart(product);

            return RedirectToAction("Index");
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

        public ActionResult Checkout()
        {
            Order order = new Order();
            TryUpdateModel(order);
            EFDbContext context = new EFDbContext();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                order.CustomerEmail = context.Users.Where(p => p.UserName == HttpContext.User.Identity.Name).Single().Email;
                
            }

            return View(order);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {

            Order order = new Order();
            TryUpdateModel(order);

            try
            {
                order.CustomerEmail = User.Identity.Name;
                order.Created = DateTime.Now;
                order.UserId = context.Users.Where(p => p.UserName == HttpContext.User.Identity.Name).Single().Id;

                // Save order
                context.Orders.Add(order);
                context.SaveChanges();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);
                return RedirectToAction("CheckoutComplete", new { id = order.OrderId });
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }                          

        }

        public ActionResult CheckoutComplete(int id)
        {
            // Validate customer owns this order
            bool isValid = context.Orders.Any(
                o => o.OrderId == id &&
                o.CustomerEmail == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Произошла ошибка при оформлении заказа! Попробуйте еще раз...");
            }


        }
    }
}