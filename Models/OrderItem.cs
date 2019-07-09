using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAFE2018GrandeGiftFinal.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
            
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

    }
}
