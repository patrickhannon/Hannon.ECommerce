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

namespace ECommerce.Controllers
{
    public class CatalogController : BaseController
    {
        // GET: Catalog
        private readonly CatalogSettings _catalogSettings;
        //private readonly IAclService _aclService;
        //private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly ICategoryService _categoryService;
        //private readonly ICustomerActivityService _customerActivityService;
        //private readonly IGenericAttributeService _genericAttributeService;
        //private readonly ILocalizationService _localizationService;
        //private readonly IManufacturerService _manufacturerService;
        //private readonly IPermissionService _permissionService;
        //private readonly IProductModelFactory _productModelFactory;
        //private readonly IProductService _productService;
        //private readonly IProductTagService _productTagService;
        //private readonly IStoreContext _storeContext;
        //private readonly IStoreMappingService _storeMappingService;
        //private readonly IVendorService _vendorService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        //private readonly MediaSettings _mediaSettings;
        private readonly VendorSettings _vendorSettings;

        public virtual ActionResult Category(int categoryId, CatalogPagingFilteringModel command)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null || category.Deleted)
                return InvokeHttp404();

            //model
            var model = _catalogModelFactory.PrepareCategoryModel(category, command);

            //template
            var templateViewPath = _catalogModelFactory.PrepareCategoryTemplateViewPath(category.CategoryTemplateId);
            return View(templateViewPath, model);
        }

        public virtual ActionResult Search(SearchModel model, CatalogPagingFilteringModel command)
        {
            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                HanCustomerDefaults.LastContinueShoppingPageAttribute,
                _webHelper.GetThisPageUrl(false),
                _storeContext.CurrentStore.Id);

            if (model == null)
                model = new SearchModel();

            model = _catalogModelFactory.PrepareSearchModel(model, command);
            return View(model);
        }
    }
}