using System.Web.Mvc;
using ECommerce.Data.Entities.Customers;

namespace ECommerce.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            var customer = new Customer();
            return View(customer);
        }
    }
}