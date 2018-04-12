using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DigitalWorldStore.Domain.Entities
{
   public class User:IdentityUser
    {

        public int Year { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public User()
        {

        }

        //public static string GetActivateUrl()
        //{
        //    return Guid.NewGuid().ToString("N");
        //}

        //public string ConfirmPassword { get; set; }
    }
}
