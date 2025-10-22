using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zain.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string TypeUser { get; set; } // user , client
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
