using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DigitalWorldStore.Domain.Real_Repository;

namespace DigitalWorldStore.Domain.Entities
{
    public class DBInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            base.Seed(context);
        }
    }
}
