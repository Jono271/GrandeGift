using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//..................
using TAFE2018GrandeGiftFinal.Models;
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.ViewModels;

namespace TAFE2018GrandeGiftFinal.Controllers.ApiController
{
    [Route("api/products")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IDataService<Product> _productService;
        private IDataService<Category> _categoryService;
        public int PageSize = 4;

        public ApiController(IDataService<Product> productService,
                                 IDataService<Category> categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> proList = _productService.GetAll();

            ProductListViewModel vm = new ProductListViewModel
            {
                Total = proList.Count(),
                Products = proList,
            };

            return proList;
        }

        //api/products/name
        [HttpGet("{id}")]
        public IEnumerable<Product> Get(int id)
        {
            IEnumerable<Product> proSingle = _productService.GetAll().Where(c => c.CategoryId == id); //Possible Problem?

            return proSingle;
        }
    }
}