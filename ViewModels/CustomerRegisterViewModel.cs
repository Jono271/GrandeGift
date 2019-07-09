using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.............
using System.ComponentModel.DataAnnotations;
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class CustomerRegisterViewModel
    {
        [Required (ErrorMessage = "Please enter a valid UserName.")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required (ErrorMessage = "Password must contain at least 6 characters & contain 1 digit.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required (ErrorMessage = "The passwords do not match.")] 
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required (ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Please enter your First Name.")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Please enter your Last Name.")]
        public string LastName { get; set; }

        [Required  (ErrorMessage = "Please enter your Address.")]
        public string Address { get; set; }

        [Required  (ErrorMessage = "Please entera valid Suburb.")]
        public string Suburb { get; set; }

        [Required (ErrorMessage = "Please select your State.")]
        public StateSelectEnum State { get; set; }

        [Required (ErrorMessage = "Please enter a valid Australian postcode (4 Digits)")]
        [MaxLength(4)]
        public string Postcode { get; set; }
    }
}