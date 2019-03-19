using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Models;

namespace ECommerce.Services.Customer
{
    public interface ICustomerService
    {
        ResponseMessage Create(Data.Entities.Customers.Customer model);
        Data.Entities.Customers.Customer GetCustomer(int customerId);
    }
}
