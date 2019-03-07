using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Filters;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        // GET: Orders
        [ECommerceAuthorize(AccessLevel = "Customer")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CustomerOrders()
        {
            var searchModel = new SearchViewModel();
            return View(searchModel);
        }
    }
}