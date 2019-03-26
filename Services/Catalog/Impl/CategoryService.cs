using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Core;
using ECommerce.Data.Core.Data;
using ECommerce.Data.Entities;
using ECommerce.Data.Entities.Caching;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Customers;
using ECommerce.Data.Entities.Security;
using ECommerce.Data.Entities.Stores;
using Nop.Core.Domain.Common;
using ECommerce.Data.Repository;
namespace ECommerce.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly CatalogSettings _catalogSettings;
        private readonly CommonSettings _commonSettings;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryMappingRepository;

        #endregion
        #region Ctor

        public CategoryService(CatalogSettings catalogSettings,
                CommonSettings commonSettings,
                IRepository<Category> categoryRepository,
                IRepository<Product> productRepository,
                IRepository<ProductCategory> productCategoryMappingRepository,
                ISearchRepository searchRepository
            )
            {
                _catalogSettings = catalogSettings;
                _commonSettings = commonSettings;
                _categoryRepository = categoryRepository;
                _productRepository = productRepository;
                _searchRepository = searchRepository;
                _productCategoryMappingRepository = productCategoryMappingRepository;
            }
            #endregion


        public IList<Product> GetAllProductsBelongingToCategory(int categoryId)
        {
            return _searchRepository.GetProductsByCategory(categoryId);
        }

        public IList<Product> GetFeaturedProducts(bool isFeatured)
        {
            return _searchRepository.GetFeaturedProducts(isFeatured);
        }
    }
}
