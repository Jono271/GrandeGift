using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//....................
using Microsoft.AspNetCore.Mvc;
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TAFE2018GrandeGiftFinal.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        //Injection
        private IDataService<Category> _categoryService;

        public CategoryController(IDataService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<Category> catList = _categoryService.GetAll();

            CategoryListViewModel vm = new CategoryListViewModel
            {
                Total = catList.Count(),
                Categories = catList
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = vm.Name,
                    Details = vm.Details
                };
                _categoryService.Create(category);

                return RedirectToAction("List", "Category");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Category cat = _categoryService.GetSingle(c => c.CategoryId == id);

            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryId = cat.CategoryId,
                Name = cat.Name,
                Details = cat.Details
            };

            return View(vm);
        }

        [HttpPost]  
        public IActionResult Update(int id, CategoryUpdateViewModel vm)
        {
            Category cat = _categoryService.GetSingle(c => c.CategoryId == vm.CategoryId);

            if (ModelState.IsValid && cat != null)
            {
                cat.Name = vm.Name;
                cat.Details = vm.Details;

                //Call "Update" service
                _categoryService.Update(cat);

                return RedirectToAction("List", "Category");
            }
            //If invalid
            return View(vm);
        }
    }
}
