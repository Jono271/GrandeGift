using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//......................
using System.ComponentModel.DataAnnotations;
using TAFE2018GrandeGiftFinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CustomerEditViewModel
    {
        [Required (ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }

        //[Required  (ErrorMessage = "Please enter your Address.")]
        public string Address { get; set; }

        //[Required  (ErrorMessage = "Please entera valid Suburb.")]
        public string Suburb { get; set; }

        //[Required (ErrorMessage = "Please select your State.")]
        public StateSelectEnum State { get; set; }

        //[Required (ErrorMessage = "Please enter a valid Australian postcode (4 Digits)")]
        //[MaxLength(4)]
        public string Postcode { get; set; }

        public int CustomerID { get; set; }

        public int AddressId { get; set; } 
        public SelectList AddressList { get; set; }
  
    }
}
