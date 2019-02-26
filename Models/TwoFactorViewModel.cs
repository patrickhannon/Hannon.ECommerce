using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class TwoFactorViewModel
    {
        public string UserValue { get; internal set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }

    }
}