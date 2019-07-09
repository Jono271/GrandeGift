using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//........................
using System.ComponentModel.DataAnnotations;
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CustomerLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
