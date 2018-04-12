using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigitalWorldStore.Domain.Entities
{
   public class Product
    {
        [Key]
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        //public DateTime ProductionDate { get; set; }
        //public DateTime ExpirationDate { get; set; }
        //public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
