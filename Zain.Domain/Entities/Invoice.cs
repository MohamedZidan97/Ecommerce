using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zain.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PaymentId { get; set; }

        public int OrderItemsNumber { get; set; }
    }
}
