using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...............................
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using Microsoft.AspNetCore.Hosting;
using TAFE2018GrandeGiftFinal.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TAFE2018GrandeGiftFinal.Controllers
{
    public class CartController : Controller
    {
        private IDataService<Product> _productService;
        //A Helper to identify Hosting side path info 
        private readonly IHostingEnvironment _hostingEnvironment;

        private IDataService<Order> _orderService;
        private IDataService<OrderItem> _orderItemService;
        private IDataService<Address> _addressService;

        public CartController(IHostingEnvironment hostingEnvironment,
                              IDataService<Product> productService,
                              IDataService<Order> orderService,
                              IDataService<OrderItem> orderItemService,
                              IDataService<Address> addressService)
        {
            _hostingEnvironment = hostingEnvironment;
            _productService = productService;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
            //ViewBag.cart = cart;
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var vm = new CartIndexViewModel();
                var collection = new List<CartIndexItemViewModel>();

                foreach (var item in cart)
                {
                    var cartItem = new CartIndexItemViewModel()
                    {
                        Name = item.Product.Name,
                        Image = item.Product.Image,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        SubTotal = item.SubTotal
                    };

                    collection.Add(cartItem);
                }

                vm.Items = collection;
                vm.Total = collection.Sum(s => s.Price * s.Quantity);


                return View("Index", vm);
            }
        }
        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            //Checks if theres no item 
            //If adding to exisiting object
            if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItem> cart = new List<OrderItem>();
                cart.Add(new OrderItem { Product = _productService.GetSingle(p => p.ProductId == id), Quantity = 1 });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                List<OrderItem> cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
                int index = isExist(id);
                //If none, we need to add
                if (index == -1)
                {
                    cart.Add(new OrderItem { Product = _productService.GetSingle(p => p.ProductId == id), Quantity = 1 });
                }
                else
                {
                    cart[index].Quantity++;
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("CustList", "Product");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<OrderItem> cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    cart.Remove(cart[i]);
                }
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        //The following method checks if the item is already in the cart or not
        private int isExist(int id)
        {
            List<OrderItem> cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
            Product product = _productService.GetSingle(p => p.ProductId == id);
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            IEnumerable<Address> addList = _addressService.GetAll();
            IEnumerable<Product> proList = _productService.GetAll();

            CartCheckoutViewModel vm = new CartCheckoutViewModel();

            vm.Addresses = new SelectList(addList, "AddressId", "HouseAddress");

            return View(vm);
        }

        [HttpPost]
        public IActionResult Checkout(CartCheckoutViewModel vm)
        {
            if (ModelState.IsValid)
            {
                List<OrderItem> cart = HttpContext.Session.GetObjectFromJson<List<OrderItem>>("cart");
                

                Order ord = new Order
                {
                    Items = vm.Items,
                    OrderId = vm.OrderId,
                    AddressId = vm.AddressId,
                };

                List<OrderItem> items = new List<OrderItem>();

                foreach (var item in cart)
                {
                    var cartItem = new OrderItem()
                    {
                        OrderId = item.OrderId,
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        SubTotal = item.SubTotal,
                    };
                    items.Add(cartItem);
                }

                ord.Items = items;

                _orderService.Create(ord);

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
    }
}