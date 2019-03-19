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

namespace ECommerce.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        #region Fields
            private readonly CatalogSettings _catalogSettings;
            private readonly CommonSettings _commonSettings;
            private readonly IRepository<Category> _categoryRepository;
            private readonly IRepository<Product> _productRepository;
            private readonly IRepository<ProductCategory> _productCategoryMappingRepository;

        #endregion
        #region Ctor

        public CategoryService(CatalogSettings catalogSettings,
                CommonSettings commonSettings,
                IRepository<Category> categoryRepository,
                IRepository<Product> productRepository,
                IRepository<ProductCategory> productCategoryMappingRepository
            )
            {
                _catalogSettings = catalogSettings;
                _commonSettings = commonSettings;
                _categoryRepository = categoryRepository;
                _productRepository = productRepository;
                _productCategoryMappingRepository = productCategoryMappingRepository;
            }
            #endregion

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <param name="loadCacheableCopy">A value indicating whether to load a copy that could be cached (workaround until Entity Framework supports 2-level caching)</param>
        /// <returns>Categories</returns>
        public virtual IList<Category> GetAllCategories(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true)
        {
            IList<Category> categories = _categoryRepository.Get();
            foreach (var c in categories)
            {
                
            }
            return categories;
        }

        public IPagedList<Category> GetAllCategories(string categoryName, int storeId = 0, int pageIndex = 0, int pageSize = Int32.MaxValue,
            bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetChildCategoryIds(int parentCategoryId, int storeId = 0, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void InsertCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductCategory(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId, int pageIndex = 0, int pageSize = Int32.MaxValue,
            bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<ProductCategory> GetProductCategoriesByProductId(int productId, int storeId, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public ProductCategory GetProductCategoryById(int productCategoryId)
        {
            throw new NotImplementedException();
        }

        public void InsertProductCategory(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public string[] GetNotExistingCategories(string[] categoryIdsNames)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, int[]> GetProductCategoryIds(int[] productIds)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesByIds(int[] categoryIds)
        {
            throw new NotImplementedException();
        }

        public IList<Category> SortCategoriesForTree(IList<Category> source, int parentId = 0, bool ignoreCategoriesWithoutExistingParent = false)
        {
            throw new NotImplementedException();
        }

        public ProductCategory FindProductCategory(IList<ProductCategory> source, int productId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public string GetFormattedBreadCrumb(Category category, IList<Category> allCategories = null, string separator = ">>",
            int languageId = 0)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetCategoryBreadCrumb(Category category, IList<Category> allCategories = null, bool showHidden = false)
        {
            throw new NotImplementedException();
        }
    }
    }
