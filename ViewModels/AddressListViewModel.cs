using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.........................
using TAFE2018GrandeGiftFinal.Models;

namespace TAFE2018GrandeGiftFinal.ViewModels
{
    public class AddressListViewModel
    {
        public int AddressId { get; set; } //Primary Key
        public string HouseAddress { get; set; }
        public string Suburb { get; set; }
        public StateSelectEnum State { get; set; }
        public string Postcode { get; set; }
        public IEnumerable<Address> Addresses { get; set; }

        
        public int CustomerId { get; set; } //Foreign Key

        public int Total { get; set; }
    }
}
