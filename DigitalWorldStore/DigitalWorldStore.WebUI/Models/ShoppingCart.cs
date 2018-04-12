using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Real_Repository;

namespace DigitalWorldStore.Domain.Entities
{
    public class ShoppingCart
    {

        EFDbContext context = new EFDbContext();

        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.ShoppingCartId = shoppingCart.GetCartId(context);
            return shoppingCart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }


        public void AddToCart(Product product)
        {
            // Get the matching cart and product instances
            var cartItem = context.CartLists.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartList
                {
                    ProductId = (int)product.ProductId,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    Created = DateTime.Now
                };
                context.CartLists.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Quantity++;
            }
            // Save changes
            context.SaveChanges();
        }


        public int RemoveFromCart(Product product)
        {
            // Get the cart
            var cartItem = context.CartLists.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ProductId == product.ProductId);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    context.CartLists.Remove(cartItem);
                }
                // Save changes
                context.SaveChanges();
            }
            return itemCount;
        }

        // Remove ProductCart from Shopping Cart
        public void RemoveProductCart(Product product)
        {
            var cartItem = context.CartLists.FirstOrDefault(
                 cart => cart.CartId == ShoppingCartId
                 && cart.ProductId == product.ProductId);
            context.CartLists.Remove(cartItem);
            context.SaveChanges();
        }


        public void EmptyWholeCart()
        {
            var cartItems = context.CartLists.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                context.CartLists.Remove(cartItem);
            }
            // Save changes
            context.SaveChanges();
        }


        public List<CartList> GetCartItems()
        {
            return context.CartLists.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }


        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in context.CartLists
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Quantity).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }


        public decimal GetTotalPrice()
        {
            //Calulate total count of products
            decimal? total = (from cartItems in context.CartLists
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    ProductUnitPrice = item.Product.Price,
                    Quantity = item.Quantity
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Quantity * item.Product.Price);

                context.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.TotalPrice = orderTotal;
            // Save the order
            context.SaveChanges();
            // Empty the shopping cart
            EmptyWholeCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = context.CartLists.Where(
                c => c.CartId == ShoppingCartId);

            foreach (CartList item in shoppingCart)
            {
                item.CartId = userName;
            }
            context.SaveChanges();
        }

    }
}
