using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.......................
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TAFE2018GrandeGiftFinal.Models
{
    public enum StateSelectEnum
    {
        NSW,
        WA,
        QLD,
        VIC,
        SA,
        TAS
    }

    public class Customer
    {
        public int CustomerId { get; set; } //Primary Key
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public StateSelectEnum State { get; set; }
        public string Postcode { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
