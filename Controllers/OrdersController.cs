using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Filters;

namespace ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        [ECommerceAuthorize(AccessLevel = "Customer")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Orders()
        {
            return View();
        }
    }
}