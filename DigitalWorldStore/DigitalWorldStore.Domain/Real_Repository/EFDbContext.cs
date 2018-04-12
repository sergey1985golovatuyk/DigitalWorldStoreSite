using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DigitalWorldStore.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DigitalWorldStore.Domain.Real_Repository
{
    public class EFDbContext:IdentityDbContext<User>
    {

        public EFDbContext(): base("name = DigitalWorldStoreDB") { }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartList> CartLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}
