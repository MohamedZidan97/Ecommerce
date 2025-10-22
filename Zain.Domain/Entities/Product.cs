using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zain.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public int Quentity { get; set; }
        public decimal UnitPrice { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int SubCategoryId
        {
            get; set;

        }
        public SubCategory SubCategory { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
