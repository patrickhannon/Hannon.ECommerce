using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            TwoFactorViewModel model = new TwoFactorViewModel();

            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View(model);
        }
    }
}