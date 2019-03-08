using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Services.Menu;
using Hannon.Utils;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;

        public HomeController(IMenuService menuService)
        {
            ArgumentValidator.ThrowOnNull("menuService", menuService);
            _menuService = menuService;
        }
        public ActionResult Index()
        {
            var storeModel = new StoreViewModel()
            {
                HanMenu = _menuService.LoadCategories()
            };
            return View(storeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult _GetCategoryMenu()
        {
            var model = _menuService.LoadCategories();
            return PartialView("_GetCategoryMenu", model);
        }
    }
}