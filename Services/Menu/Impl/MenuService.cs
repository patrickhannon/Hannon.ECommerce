using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Models;
using Hannon.Utils;
namespace ECommerce.Services.Menu.Impl
{
    public class MenuService : IMenuService
    {
        private ICategoryService _categoryService;
        private readonly IList<HanMenuItem> _collection;
        public MenuService(ICategoryService categoryService)
        {
            ArgumentValidator.ThrowOnNull("categoryService", categoryService);
            _categoryService = categoryService;
        }

        public Models.HanMenu LoadCategories()
        {
            IList<HanMenuItem> collection;
            var categories = _categoryService.GetAllCategories();
            collection = ConvertCategoriesToMenuItems(categories);
            var menu = new Models.HanMenu()
            {
                MenuItems = collection
            };
            return new HanMenu()
            {
                MenuItems = collection
            };
        }

        private IList<HanMenuItem> ConvertCategoriesToMenuItems(IList<Category> categories)
        {
            IList<HanMenuItem> collection = new List<HanMenuItem>();
            var order = 0;
            foreach (var category in categories)
            {
                collection.Add(new HanMenuItem()
                {
                    MenuId = category.Id,
                    DisplayOrder = order,
                    MenuAction = "/Catalog/Category",
                    MenuTitle = category.Name
                });
                order++;
            }
            return collection;
        }
    }
}