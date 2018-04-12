using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.WebUI.Models
{
    // Class collects all data about product that will be passed to view
    public class ProductListViewModel
    {

        public IEnumerable<Product> Products { get; set; } // Enumerator of products in current view

        public PagingInfo PagingInfo { get; set; } // Paging view property

        public string CurrentCategory { get; set; }  // Current category on view

    }
}