using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Data.Entities;
using ECommerce.Services.Catalog;
using ECommerce.Services.Catalog.Impl;
using ECommerce.Data.Repository;
namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        // GET: Product
        public ProductController(
            IProductService productService
            )
        {
            _productService = productService;
        }
        public ActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            productModel.Products = productModel.findAll();
            return View(productModel);
        }

        public ActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }
    }
}