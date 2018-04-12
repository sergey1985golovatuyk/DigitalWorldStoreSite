using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWorldStore.Domain.Entities;
using DigitalWorldStore.Domain.Abstract_Repository;

namespace DigitalWorldStore.Domain.Real_Repository
{
    public class EFOrderRepository : IOrderRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Order> Orders
        { get { return context.Orders; } }
    }
}
