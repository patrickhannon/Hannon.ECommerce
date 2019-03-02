using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities;

namespace ECommerce.Models
{
    public class CartViewModel
    {
        public List<Item> Items { set; get; }
    }
}