using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities.Orders;
using ECommerce.Models;

namespace ECommerce.Services.Catalog
{
    public interface ICartService
    {
        ResponseMessage AddProductToCart(ShoppingCartItem item);
        IList<ShoppingCartItem> GetCart(int customerId);
        ResponseMessage RemoveProductFromCart(ShoppingCartItem item);
    }
}