using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.................
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CartCheckoutViewModel
    {
        public int OrderId { get; set; }

        public int AddressId { get; set; }
        public SelectList Addresses { get; set; }

        public List<OrderItem> Items { get; set; }

        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal { get; set; }   
    }
}
