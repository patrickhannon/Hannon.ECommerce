using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Core.Data;
using ECommerce.Data.Repository;
using ECommerce.Models;

namespace ECommerce.Services.Customer.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Data.Entities.Customers.Customer> _customerRepository;
        public CustomerService(IRepository<Data.Entities.Customers.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public ResponseMessage Create(Data.Entities.Customers.Customer model)
        {
            _customerRepository.Insert(model);
            return new ResponseMessage()
            {
                Message = "Customer created",
                Status = true
            };
        }

        public Data.Entities.Customers.Customer GetCustomer(int id)
        {
            return _customerRepository.GetById(id);
        }
    }
}