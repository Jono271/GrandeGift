using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.........................
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class ProductListViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Total { get; set; }
    }
}
