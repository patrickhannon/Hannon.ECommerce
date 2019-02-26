using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Models;


namespace ECommerce.Mocks
{
    public static class Mocks
    {
        public static RegisterViewModel GetRegisterViewModelMocks()
        {
            return new RegisterViewModel()
            {
                CellPhone = "18174120313",
                Email = "patrick_hannon@hotmail.com",
                UserName = "phannon"
            };
        }
    }
}