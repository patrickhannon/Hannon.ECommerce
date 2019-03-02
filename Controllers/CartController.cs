using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data.Entities;
using ECommerce.Models;
using ECommerce.Filters;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        // GET: ShoppingCart
        [ECommerceAuthorize(AccessLevel = "Customer")]
        public ActionResult Index()
        {
            var items =  (List<Item>)Session["cart"];
            var cartModel = new CartViewModel();
            if (items != null && items.Any())
            {
                cartModel.Items = items;
            }
            else
            {
                cartModel.Items = new List<Item>();
            }
            return View(cartModel);
        }

        public ActionResult Buy(string id)
        {
            ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>
                {
                    new Item { Product = productModel.find(id), Quantity = 1 }
                };
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id.Equals(id))
                    return i;
            return -1;
        }

    }
}
