using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWorldStore.Domain.Entities;
using DigitalWorldStore.Domain.Abstract_Repository;

namespace DigitalWorldStore.Domain.Real_Repository
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }
    }
}
