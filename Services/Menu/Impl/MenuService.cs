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
            var allCategories = _categoryService.GetAllCategories();
            //build out main menu first 
            
            collection = ConvertCategoriesToMenuItems(allCategories);

            //associate menu with submenu items 
            foreach (var menuItem in collection)
            {
                if (menuItem.ParentCategoryId > 0)
                {
                    AssociateMenuWithSubMenu(menuItem, collection);
                }
            }

            return new Models.HanMenu()
            {
                MenuItems = collection.Where(x => x.ParentCategoryId == 0).ToList()
            };
        }

        private IList<HanMenuItem> AssociateMenuWithSubMenu(HanMenuItem item, 
            IList<HanMenuItem> mainMenu)
        {
            var mainMenuItem = mainMenu.FirstOrDefault(x => x.MenuId == item.ParentCategoryId);
            //Then populate 
            if (mainMenuItem != null)
            {
                var subMenuItem = new HanMenuItem()
                {
                    DisplayOrder = 1,
                    MenuId = item.MenuId,
                    MenuAction = item.MenuTitle,
                };

                if (mainMenuItem.SubMenus == null)
                {
                    mainMenuItem.SubMenus = new List<HanMenuItem>();
                    mainMenuItem.SubMenus.Add(subMenuItem);
                }
                else
                {
                    mainMenuItem.SubMenus.Add(subMenuItem);
                }
            }
            return mainMenu;
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
                    MenuAction = "/Catalog/Search/"+ category.ParentCategoryId,
                    MenuTitle = category.Name,
                    ParentCategoryId = category.ParentCategoryId
                });
                order++;
            }
            return collection;
        }
    }
}