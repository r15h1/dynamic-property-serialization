using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DynamicSerialization.Models;
using DynamicSerialization.Filters;


namespace DynamicSerialization.Controllers
{
    [DynamicFieldsFilter(typeof(Product))]
    [Route("products")]
    public class ProductsController : Controller
    {        
        private readonly List<Product> products;

        public ProductsController()
        {
            products = Repository.GetProducts();
        }
        
        [HttpGet]        
        public IActionResult Index() => Json(products);

        [HttpGet("{id}")]
        public IActionResult Details(int id) => Json(products.FirstOrDefault(p => p.Id == id));
    }
}