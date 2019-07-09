using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.......................
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TAFE2018GrandeGiftFinal.Models
{
    public class Product
    {
        public int ProductId { get; set; } //PK
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscontinued { get; set; }

        //Image
        public string Image { get; set; }

        public int CategoryId { get; set; }
    }
}
