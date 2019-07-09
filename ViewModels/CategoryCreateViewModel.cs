using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.....................
using System.ComponentModel.DataAnnotations;
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required (ErrorMessage = "Category name must have a minimum of 3 characters!")]
        [MinLength(3)] //Must have a minimum of 3 characters
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
