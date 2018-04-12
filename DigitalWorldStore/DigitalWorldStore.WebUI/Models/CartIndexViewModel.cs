using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        //public ShoppingCart Cart { get; set; }
        //public string ReturnUrl { get; set; }

        public List<CartList> CartItems { get; set; }
        public decimal CartTotal { get; set; }

        public string Message { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }

        public string ReturnUrl { get; set; }

    }
}