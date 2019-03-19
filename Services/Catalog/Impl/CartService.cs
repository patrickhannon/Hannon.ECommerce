using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Core.Data;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Orders;
using ECommerce.Data.Repository;
using ECommerce.Models;
using Nop.Core.Domain.Common;

namespace ECommerce.Services.Catalog.Impl
{
    public class CartService : ICartService
    {
        #region Fields
        private readonly CommonSettings _commonSettings;
        private readonly IRepository<ShoppingCartItem> 
            _shoppingCartItemRepository;
        private readonly ICustomerRepository
            _customerRepository;
        #endregion
        public CartService(
            CommonSettings commonSettings,
            IRepository<ShoppingCartItem> shoppingCartItemRepository
        )
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _commonSettings = commonSettings;
        }

        public ResponseMessage AddProductToCart(ShoppingCartItem item)
        {
            _shoppingCartItemRepository.Insert(item);
            return new ResponseMessage()
            {
                Status = true,
                Message = "Item added to Cart."
            };
        }

        public IList<ShoppingCartItem> GetCart(int customerId)
        {
            return _customerRepository.GetByCustomerId(customerId);
        }

        public ResponseMessage RemoveProductFromCart(ShoppingCartItem item)
        {
            _shoppingCartItemRepository.Delete(item);
            return new ResponseMessage()
            {
                Status = true,
                Message = "Item deleted from Cart."
            };
        }
    }
}