using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zain.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CartItemsNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
