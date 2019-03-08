using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class HanMenuItem
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public int DisplayOrder { get; set; }
        public string MenuAction { get; set; }
        public override string ToString()
        {
            return MenuTitle;
        }
    }
}