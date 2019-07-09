using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//......................
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CartIndexViewModel
    {
        public int OrderId { get; set; }

        public List<CartIndexItemViewModel> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class CartIndexItemViewModel
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
