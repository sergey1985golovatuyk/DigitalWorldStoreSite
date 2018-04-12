using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.Domain.Abstract_Repository
{
    // Interface for Product repository
   public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
    }
}
