using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Core;
using ECommerce.Data.Entities;
using ECommerce.Data.Entities.Catalog;

namespace ECommerce.Services
{
    public interface ICategoryService
    {
        IList<Product> GetAllProductsBelongingToCategory(int categoryId);
        IList<Product> GetFeaturedProducts(bool isFeatured);

    }
}
