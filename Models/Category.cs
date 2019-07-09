using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.......................
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TAFE2018GrandeGiftFinal.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
