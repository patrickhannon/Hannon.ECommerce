using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Models;

namespace ECommerce.Services.Menu
{
    public interface IMenuService
    {
        Models.HanMenu LoadCategories();

    }
}