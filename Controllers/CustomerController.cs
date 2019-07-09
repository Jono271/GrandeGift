using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//.......................
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TAFE2018GrandeGiftFinal.Controllers
{
    public class CustomerController :  Controller
    {
        private UserManager<IdentityUser> _userManagerService; 
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Address> _addressService;
        private IDataService<Customer> _customerService;

        public CustomerController(UserManager<IdentityUser> userManagerService, 
                                SignInManager<IdentityUser> signInManagerService,
                                RoleManager<IdentityRole> roleManagerService,
                                IDataService<Customer> customerService,
                                IDataService<Address> addressService)
        {
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
            _roleManagerService = roleManagerService;
            _customerService = customerService;
            _addressService = addressService;
        }
        //End Injection

        public IActionResult Index() => View(); //Shorter way of returning the view
        // {
        //    return View();
        // }

        public IActionResult Register() => View();
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Add a new Customer
                IdentityUser user = new IdentityUser(vm.UserName);
                user.Email = vm.Email;

                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password); 

                if (result.Succeeded)
                {
                    Customer cust = new Customer
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Email = vm.Email,
                        Username = vm.UserName,
                        Password = vm.Password,
                        ConfirmPassword = vm.ConfirmPassword,
                        Address = vm.Address,
                        Suburb = vm.Suburb,
                        State = vm.State,
                        Postcode = vm.Postcode
                    };
                    //Save to db
                    _customerService.Create(cust);

                    //Return to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Display error
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username or password is not correct");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Login", "Customer");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var userID = _userManagerService.GetUserId(User);
            Customer cust = _customerService.GetQuery(c => c.UserId == userID);
            IEnumerable<Address> addList = _addressService.GetAll();

            CustomerEditViewModel vm = new CustomerEditViewModel
            {
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                Email = cust.Email,
                Address = cust.Address,
                Suburb = cust.Suburb,
                State = cust.State,
                Postcode = cust.Postcode
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(CustomerEditViewModel vm)
        {
            var userID = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetQuery(c => c.UserId == userID);

            if (ModelState.IsValid && customer != null)
            {
                customer.FirstName = vm.FirstName;
                customer.LastName = vm.LastName;
                customer.Email = vm.Email;
                customer.Address = vm.Address;
                customer.Suburb = vm.Suburb;
                customer.State = vm.State;
                customer.Postcode = vm.Postcode;

                //Call the "Update" service
                _customerService.Update(customer);

                //Return to page
                return RedirectToAction("Index", "Home");
            }
            //If invalid
            return View(vm);
        }
    }
}
