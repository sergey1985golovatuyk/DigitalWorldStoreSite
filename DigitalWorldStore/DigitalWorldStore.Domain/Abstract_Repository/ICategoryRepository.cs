using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWorldStore.Domain.Entities;

namespace DigitalWorldStore.Domain.Abstract_Repository
{
    // Interface for category repository
   public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
    }
}
