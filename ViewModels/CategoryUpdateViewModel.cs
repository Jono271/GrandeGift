using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//......................
using System.ComponentModel.DataAnnotations;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CategoryUpdateViewModel
    {
        public int CategoryId { get; set; }

        [Required (ErrorMessage = "Please enter at least 3 Characters for the Name!")]
        [MinLength(3)]
        public string Name { get; set; }

        public string Details { get; set; }
    }
}
