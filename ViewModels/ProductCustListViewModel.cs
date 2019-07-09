using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...................
using TAFE2018GrandeGiftFinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class ProductCustListViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public SelectList Categories { get; set; }
        public int CategoryId { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
