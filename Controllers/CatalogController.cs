using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data.Core;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Customers;
using ECommerce.Data.Entities.Vendors;
using ECommerce.Models;
using ECommerce.Services;
using ECommerce.Services.Menu;
using Hannon.Utils;

namespace ECommerce.Controllers
{
    public class CatalogController : BaseController
    {
        // GET: Catalog
        private readonly CatalogSettings _catalogSettings;
        private readonly ICategoryService _categoryService;
        private readonly IMenuService _menuService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;

        //private readonly MediaSettings _mediaSettings;
        private readonly VendorSettings _vendorSettings;

        public CatalogController(ICategoryService categoryService, IMenuService menuService)
        {
           ArgumentValidator.ThrowOnNull("categoryService", categoryService);
           ArgumentValidator.ThrowOnNull("menuService", menuService);
            _categoryService = categoryService;
            _menuService = menuService;

        }

        public virtual ActionResult Category(int categoryId) //, CatalogPagingFilteringModel command)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null || category.Deleted)
                return InvokeHttp404();

            //model
            //var model = _catalogModelFactory.PrepareCategoryModel(category, command);

            //template
            //var templateViewPath = _catalogModelFactory.PrepareCategoryTemplateViewPath(category.CategoryTemplateId);
            //return View(templateViewPath, model);
            return View();
        }

        public virtual ActionResult Search(int parentCategoryId) //, CatalogPagingFilteringModel command)
        {
            var category = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId);

            //if (category == null || category.Deleted)
            //    return InvokeHttp404();

            //model
            //var model = _catalogModelFactory.PrepareCategoryModel(category, command);

            //template
            //var templateViewPath = _catalogModelFactory.PrepareCategoryTemplateViewPath(category.CategoryTemplateId);
            //return View(templateViewPath, model);
            return View();
        }


        [AllowAnonymous]
        public ActionResult _GetCategoryMenu()
        {
            var model = _menuService.LoadCategories();
            return PartialView("_GetCategoryMenu", model);
        }

        [AllowAnonymous]
        public virtual ActionResult Search(string searchTerm)
        {
            //Lets see what's being searched for, this is for SEO optimization

            //Look in Category; returns true get all the products belonging to that Category...
            //Cherry pick what is shown on the page, same product model..

            //Look in Product; returns true gets that product...



            return View();
        }

        public virtual ActionResult Following()
        {
            return View();
        }


        //public virtual ActionResult Search(SearchModel model, CatalogPagingFilteringModel command)
        //{
        //    //'Continue shopping' URL
        //    _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
        //        HanCustomerDefaults.LastContinueShoppingPageAttribute,
        //        _webHelper.GetThisPageUrl(false),
        //        _storeContext.CurrentStore.Id);

        //    if (model == null)
        //        model = new SearchModel();

        //    model = _catalogModelFactory.PrepareSearchModel(model, command);
        //    return View(model);
        //}
    }
}