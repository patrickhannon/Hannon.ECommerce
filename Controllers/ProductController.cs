using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Data.Entities;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            productModel.Products = productModel.findAll();
            return View(productModel);
        }
    }
}