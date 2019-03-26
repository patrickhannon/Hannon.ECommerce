using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities.Catalog;

namespace ECommerce.Services.Catalog
{
    public interface IProductService
    {
        Product GetProductById(int productId);

    }
}