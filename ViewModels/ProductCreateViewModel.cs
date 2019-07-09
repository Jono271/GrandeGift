using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//....................
using TAFE2018GrandeGiftFinal.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class ProductCreateViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsDiscontinued { get; set; }

        //Images
        public string Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
    }
}
