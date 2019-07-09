using Microsoft.AspNetCore.Mvc;
//..............
using Microsoft.AspNetCore.Identity;
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace TAFE2018GrandeGiftFinal.Controllers
{
    public class AddressController : Controller
    {
        private IDataService<Address> _addressService;
        private UserManager<IdentityUser> _userManagerService; 
        private IDataService<Customer> _customerService;

        public AddressController(IDataService<Address> addressService,
                                 IDataService<Customer> customerService,
                                 UserManager<IdentityUser> userManagerService)
        {
            _addressService = addressService;
            _customerService = customerService;
            _userManagerService = userManagerService;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Address> addList = _addressService.GetAll();

            AddressListViewModel vm = new AddressListViewModel
            {
                Total = addList.Count(),
                Addresses = addList
            };

            return View(vm);
        }
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            var userID = _userManagerService.GetUserId(User);
            Customer cust = _customerService.GetQuery(c => c.UserId == userID);
            IEnumerable<Address> addList = _addressService.GetAll();

            AddressCreateViewModel vm = new AddressCreateViewModel
            {
                CustomerId = cust.CustomerId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(AddressCreateViewModel vm)
        {
            var userID = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetQuery(c => c.UserId == userID);

            if (ModelState.IsValid)
            {
                Address add = new Address
                {
                    HouseAddress =  vm.HouseAddress,
                    Suburb = vm.Suburb,
                    State = vm.State,
                    Postcode = vm.Postcode,
                    CustomerId = vm.CustomerId
                    
                };
                _addressService.Create(add);

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        public IActionResult Delete(AddressListViewModel vm)
        {
            Address address = _addressService.GetSingle(a => a.AddressId == vm.AddressId);

            _addressService.Delete(address);

            return RedirectToAction("Index", "Home");
        }

        //public IActionResult Checkout()
        //{

        //}
    }
}