using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAFE2018GrandeGiftFinal.Models
{
    public class Address
    {
        public int AddressId { get; set; } //Primary Key
        public string HouseAddress { get; set; }
        public string Suburb { get; set; }
        public StateSelectEnum State { get; set; }
        public string Postcode { get; set; }

        
        public int CustomerId { get; set; } //Foreign Key
    }
}
