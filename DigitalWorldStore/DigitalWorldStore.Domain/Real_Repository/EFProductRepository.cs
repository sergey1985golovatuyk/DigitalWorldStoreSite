using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWorldStore.Domain.Abstract_Repository;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.Domain.Real_Repository
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

    }
}
