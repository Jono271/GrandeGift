using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//.......................
using Microsoft.AspNetCore.Identity;
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TAFE2018GrandeGiftFinal.Controllers
{
    public class ProductController : Controller
    {
        private IDataService<Product> _productService;
        private IDataService<Category> _categoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public int PageSize = 4;

        public ProductController(IDataService<Product> productService,
                                 IDataService<Category> categoryService,
                                 IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateViewModel();

            var categories = _categoryService.GetAll();

            model.Categories = new SelectList(categories, "CategoryId", "Name");

            return View(model);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public  async Task<IActionResult> Create(ProductCreateViewModel vm, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Product pro = new Product
                {
                    Name = vm.Name,
                    Details = vm.Details,
                    Price = vm.Price,
                    IsDiscontinued = vm.IsDiscontinued,
                    CategoryId = vm.CategoryId
                };
                //Checking for image
                if (image != null)
                {
                    //Create a path including the filename where we want to save the file 
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath, "images", Path.GetFileName(image.FileName));
                    //copy the file from temp memory to a parmanement memory
                    var fileStream = new FileStream(fileName, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                    //Whenever you use any System.IO interface or classes you makesure you close the process;
                    fileStream.Close();
                    pro.Image = Path.GetFileName(image.FileName);
                }

                _productService.Create(pro);

                return RedirectToAction("List", "Product");
            }

            var categories = _categoryService.GetAll();

            vm.Categories = new SelectList(categories, "CategoryId", "Name", vm.CategoryId);

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            Product pro = _productService.GetSingle(p => p.ProductId == id);

            var categories = _categoryService.GetAll();

            ProductUpdateViewModel vm = new ProductUpdateViewModel
            {
                ProductId = pro.ProductId,
                Name = pro.Name,
                Details = pro.Details,
                Price = pro.Price,
                IsDiscontinued = pro.IsDiscontinued,
                CategoryId = pro.CategoryId,
                Categories = new SelectList(categories, "CategoryId", "Name", pro.CategoryId)
            };
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(int id, ProductUpdateViewModel vm)
        {
            Product pro = _productService.GetSingle(p => p.ProductId == vm.ProductId);

            if (ModelState.IsValid && pro != null)
            {
                pro.Name = vm.Name;
                pro.Details = vm.Details;
                pro.Price = vm.Price;
                pro.IsDiscontinued = vm.IsDiscontinued;
                pro.CategoryId = vm.CategoryId;

                _productService.Update(pro);

                return RedirectToAction("List", "Product");
            }
            var categories = _categoryService.GetAll();

            vm.Categories = new SelectList(categories, "CategoryId", "Name", vm.CategoryId);

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Product> proList = _productService.GetAll();

            ProductListViewModel vm = new ProductListViewModel
            {
                Total = proList.Count(),
                Products = proList,
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult CustList(string CategoryName, string price, decimal number)
        {
            IEnumerable<Product> list = _productService.GetAll();
            IEnumerable<Category> catList = _categoryService.GetAll();

            ProductCustListViewModel vm = new ProductCustListViewModel
            {
                Products = list,
                //Products = HttpContext.Session.GetObjectFromJson<List<Product>>("customerList"),
            };

            vm.Categories = new SelectList(catList, "CategoryId", "Name");

            //if (!string.IsNullOrEmpty(CategoryName)) 
            //{
            //    list = list.Where(l => l.Name == CategoryName);
            //}
            //if (!string.IsNullOrEmpty(price))
            //{
            //    if (price == "max" && number != 0)
            //    {
            //        list = list.Where(l => l.Price <= number);
            //    }
            //    else if (price == "min" && number != 0)
            //    {
            //        list = list.Where(l => l.Price >= 0);
            //    }
            //}
            return View(vm);
        }

        [HttpPost]
        public IActionResult CustList(ProductCustListViewModel vm)
        {
            IQueryable<Product> query = _productService.GetAll().AsQueryable();
            IEnumerable<Category> catList = _categoryService.GetAll();

            if (vm.MinPrice == 0 && vm.MaxPrice > 0)
            {
                query = query.Where(l => l.Price <= vm.MaxPrice);
            }

            if (vm.MinPrice > 0 && vm.MaxPrice == 0)
            {
                query = query.Where(l => l.Price >= vm.MinPrice);
            }

            if (vm.MinPrice > 0 && vm.MaxPrice > 0)
            {
                query = query.Where(l => l.Price > vm.MinPrice && l.Price <= vm.MaxPrice);
            }

            if (vm.CategoryId != 0)
            {
                query = query.Where(l => l.CategoryId == vm.CategoryId);
            }

            vm.Products = query.ToList();

            vm.Categories = new SelectList(catList, "CategoryId", "Name", vm.CategoryId);

            //if (vm.CategoryId == vm.CategoryId)
            //{
            // 
            //}

            return View(vm);
        }
        //public ViewResult CustList(int productPage = 1)
        //    => View(_productService.GetAll()
        //        .OrderBy(p => p.ProductId)
        //        .Skip((productPage - 1) * PageSize)
        //        .Take(PageSize));
    }
}