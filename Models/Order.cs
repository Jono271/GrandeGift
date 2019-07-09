using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAFE2018GrandeGiftFinal.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
