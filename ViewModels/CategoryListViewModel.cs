using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//........................
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CategoryListViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int Total { get; set; }
    }
}
